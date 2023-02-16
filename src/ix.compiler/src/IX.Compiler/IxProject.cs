﻿// Ix.Compiler
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Text;
using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Parser;
using AX.ST.Syntax.Tree;
using AX.Text;
using Ix.Compiler.Core;
using IX.Compiler.Core;
using Ix.Compiler.Exceptions;
using Newtonsoft.Json;
using Polly;

namespace Ix.Compiler;

/// <summary>
///     Provides entry point for compilation of AX project sources.
/// </summary>
public class IxProject : IIxProject
{
    /// <summary>
    ///     Creates new instance of the <see cref="IxProject" />
    /// </summary>
    /// <param name="axProject">Instance of source AX project.</param>
    /// <param name="builderTypes">
    ///     List of output builders. The builders must implement <see cref="ISourceBuilder" /> and
    ///     <see cref="ICombinedThreeVisitor" />
    /// </param>
    /// <param name="targetProjectType">
    ///     Target project type. The target project type must implement
    ///     <see cref="ITargetProject" />
    /// </param>
    /// <param name="cliCompilerOptions">
    ///     Compiler options from CLI.
    /// </param>
    public IxProject(AxProject axProject, IEnumerable<Type> builderTypes, Type targetProjectType, ICompilerOptions? cliCompilerOptions = null)
    {
        AxProject = axProject;
        CompilerOptions = IxConfig.UpdateAndGetIxConfig(axProject.ProjectFolder, cliCompilerOptions);
        OutputFolder = Path.GetFullPath(Path.Combine(AxProject.ProjectFolder, CompilerOptions.OutputProjectFolder));
        BuilderTypes = builderTypes;
        TargetProject = Activator.CreateInstance(targetProjectType, this) as ITargetProject ?? throw new
            InvalidOperationException("Target project type must implement ITargetProject interface.");
    }

    
    
    /// <summary>
    ///     Get AX project.
    /// </summary>
    public AxProject AxProject { get; }

    private IEnumerable<Type> BuilderTypes { get; }
    public ICompilerOptions? CompilerOptions { get; }

    /// <summary>
    ///     Gets target project.
    /// </summary>
    public ITargetProject TargetProject { get; }

    /// <summary>
    ///     Gets root output folder where the generated sources will be emitted.
    /// </summary>
    public string OutputFolder { get; }

    /// <summary>
    ///     Generates outputs from the builders and emits the files into output folder.
    /// </summary>
    public void Generate()
    {
        Log.Logger.Information($"Compilation of project '{AxProject.SrcFolder}' started");

        var projectSources = AxProject.Sources.Select(p => (parseTree: STParser.ParseTextAsync(p).Result, source: p));

        var refParseTrees = GetReferences();

        var toCompile = refParseTrees.Concat(projectSources.Select(p => p.parseTree));

        var compilation = Compilation.Create(toCompile, Compilation.Settings.Default).Result;

        this.CleanOutput(this.OutputFolder);

        foreach (var origin in projectSources)
        {
            Log.Logger.Verbose($"Compiling '{origin.source.Filename}'.");

            foreach (var sourceBuilderType in BuilderTypes)
            {
                var builder = Activator.CreateInstance(sourceBuilderType, this, compilation);
                var treeWalker = builder as ICombinedThreeVisitor;
                var sourceBuilder = builder as ISourceBuilder;
                

                if (treeWalker == null)
                    throw new FailedToCreateCombineThreeVisitorException(
                        $"Could not create {sourceBuilderType.Name} as ICombinedThreeVisitor");
                if (sourceBuilder == null)
                    throw new FailedToCreateSourceBuilderException(
                        $"Could not create {sourceBuilderType.Name} as ISourceBuilder");


                origin.parseTree.GetRoot().Visit(new IxNodeVisitor(compilation), treeWalker);

                
                
                Policy
                    .Handle<IOException>()
                    .WaitAndRetry(5, a => TimeSpan.FromMilliseconds(500))
                    .Execute(() =>
                    {
                        using (var swr = new StreamWriter(Path.Combine(
                                   EnsureFolder(GetFilesOutputFolder(origin.source, sourceBuilder)),
                                   GetOutputFileName(origin.source, sourceBuilder))))
                        {
                            swr.Write(sourceBuilder.Output);
                        }
                    });
            }
        }

        TargetProject.ProvisionProjectStructure();
        GenerateMetadata(compilation);

        Log.Logger.Information($"Compilation of project '{AxProject.SrcFolder}' done.");
    }

    /// <summary>
    /// Cleans all output files from the output directory
    /// </summary>
    public void CleanOutput(string folderToClean)
    {
        if (!Directory.Exists(folderToClean))
            return;

        foreach (var sourceBuilder in BuilderTypes.Select(p => Activator.CreateInstance(p, this, null) as ISourceBuilder).Where(p => !(p is null)))
        {
            foreach (var outputFile in Directory.GetFiles(folderToClean, $"*{sourceBuilder.OutputFileSuffix}", SearchOption.AllDirectories))
            {
                Policy
                    .Handle<IOException>()
                    .WaitAndRetry(5, a => TimeSpan.FromMilliseconds(500))
                    .Execute(() =>
                    {
                        File.Delete(outputFile);
                    });
            }
        }
      
    }

    private static string GetOutputFileName(SourceFileText source, ISourceBuilder compilerData)
    {
        var fileInfo = new FileInfo(source.Filename);
        var fileNameWithoutSuffix = fileInfo.Name[..^fileInfo.Extension.Length];
        var fileNameWithNewExtension = $"{fileNameWithoutSuffix}{compilerData.OutputFileSuffix}";
        return fileNameWithNewExtension;
    }

    private string GetFilesOutputFolder(SourceFileText source, ISourceBuilder sourceBuilder)
    {
        var fileInfo = new FileInfo(source.Filename);
        var relativePathToSrc = fileInfo.DirectoryName!.Replace($"{AxProject.SrcFolder}", string.Empty);
        if (relativePathToSrc.StartsWith(Path.DirectorySeparatorChar))
            relativePathToSrc = relativePathToSrc.Substring(1, relativePathToSrc.Length - 1);
        return Path.Combine(OutputFolder, ".g", sourceBuilder.Group, relativePathToSrc);
    }

    private static string EnsureFolder(string folder)
    {
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        return folder;
    }

    private IEnumerable<ISyntaxTree> GetReferences()
    {
        var referencedDependencies = TargetProject.LoadReferences();

        var dependencyMetadata = referencedDependencies
            .Where(p => p.IsIxDependency)
            .Select(p => p.MetadataPath)
            .Select(p => JsonConvert.DeserializeObject<IEnumerable<string>>(File.ReadAllText(p)));


        CompileProjectReferences(referencedDependencies);


        var refParseTrees = dependencyMetadata.SelectMany(p => p)
            .Select(s => STParser.ParseTextAsync(new StringText(s)).Result);

        return refParseTrees;
    }

    private void CompileProjectReferences(IEnumerable<IReference> referencedDependencies)
    {
        foreach (var ixProjectReference in AxProject.IxReferences)
        {
            string apaxFolder = ixProjectReference.AxProjectFolder == null
                ? referencedDependencies
                    .Where(p => p.IsIxDependency)
                    .Select(p => GetApaxFolderFromProjectReference(p))
                    .FirstOrDefault()
                : ixProjectReference.AxProjectFolder;
            

            if(!Directory.Exists(apaxFolder))
                continue;

            var ax = new AxProject(apaxFolder);
                var targetProject = Activator.CreateInstance(TargetProject.GetType(), this) as ITargetProject;

                if (targetProject == null)
                    throw new FailedToCreateTargetProjectException(
                        "Target project is not a valid ITargetProject");

                var project = new IxProject(ax, BuilderTypes, targetProject.GetType());

                project.Generate();
        }
    }

    private string GetApaxFolderFromProjectReference(IReference projectReference)
    {
        try
        {
            try
            {
                using (var sr = new StreamReader(projectReference.ProjectInfo))
                {
                    var sourceinfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(sr.ReadToEnd());
                    return Path.Combine(projectReference.ReferencePath, $"..{Path.DirectorySeparatorChar}", sourceinfo["ax-source"]);
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                throw new SourceProjectInfoRetrievalException(
                    $"It looks like the file '{projectReference.ProjectInfo}' does not exist.", ex);
            }
            catch (Newtonsoft.Json.JsonSerializationException ex)
            {
                throw new SourceProjectInfoRetrievalException(
                    $"We were unable to get the info from '{projectReference.ProjectInfo}' we won't be able to compile the references." +
                    $"There seem to be a problem with '{projectReference.ProjectInfo}' consistency.", ex);
            }
            catch (Exception ex)
            {
                throw new SourceProjectInfoRetrievalException(
                    $"There was a problem getting source information about the ax project associated with " +
                    $"'{projectReference.ReferencePath}'." +
                    $"The source project may not exist or has been moved to another location." +
                    $"Check that the file '{projectReference.ProjectInfo}' exists and contains correct information.",
                    ex);
            }
        }
        catch (Exception e)
        {
            Log.Logger.Warning("Failed to retrieve apax from twin project", e);
            return string.Empty;
        }
       
    }

    private void GenerateMetadata(Compilation compilation)
    {
        var meta = compilation.GetSemanticTree().Types.Where(p => p.AccessModifier == AccessModifier.Public)
            .Select(p => CreateMetaType(p));

        using (var swr = new StreamWriter(Path.Combine(EnsureFolder(TargetProject.GetMetaDataFolder), "meta.json")))
        {
            swr.Write(JsonConvert.SerializeObject(meta));
        }

        using (var swr = new StreamWriter(Path.Combine(EnsureFolder(TargetProject.GetMetaDataFolder), "sourceinfo.json")))
        {
            var settings = new Dictionary<string, string>
            {
                ["ax-source"] = $"{GetRelativePath(this.OutputFolder, this.AxProject.ProjectFolder)}"
            };

            swr.Write(JsonConvert.SerializeObject(settings));
        }
    }

    public static string GetRelativePath(string fromPath, string toPath)
    {
        Uri fromUri = new Uri(fromPath);
        Uri toUri = new Uri(toPath);

        Uri relativeUri = fromUri.MakeRelativeUri(toUri);
        string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

        return relativePath;
    }

    private static string CreateMetaType(ITypeDeclaration type)
    {
        var kind = type.Kind;
        var sb = new StringBuilder();


        var hasNamespace = type.ContainingNamespace != null;
        if (hasNamespace) sb.Append($"NAMESPACE {type.ContainingNamespace!.FullyQualifiedName}\n");


        switch (kind)
        {
            case DeclarationKind.Struct:
                sb.Append($"TYPE {type.Name} : STRUCT ; END_STRUCT");
                break;
            case DeclarationKind.Class:
                sb.Append($"CLASS {type.Name} END_CLASS");
                break;
            case DeclarationKind.Enumeration:
                sb.Append($"TYPE {type.Name} : (item0); END_TYPE");
                break;
            case DeclarationKind.Interface:
                sb.Append($"INTERFACE {type.Name} END_INTERFACE");
                break;
            case DeclarationKind.NamedValueType:
                sb.Append($"TYPE {type.Name} : INT (item0 := 0); END_TYPE");
                break;
        }

        if (hasNamespace) sb.Append("END_NAMESPACE");

        return sb.ToString();
    }
}