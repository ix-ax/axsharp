// See https://aka.ms/new-console-template for more information
using AX.ST.Semantic;
using AX.ST.Syntax.Parser;
using AX.ST.Syntax.Tree;
using AX.Text;
using AX.Text.Diagnostics;
using Ix.Compiler;
using Ix.ixc_doc;
using System;
using CommandLine;
using System.Reflection;
using System.Text;
using CliWrap;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using AX.ST.Semantic.Model;
using Ix.ixr_doc;
using Microsoft.CodeAnalysis;
using Serilog.Parsing;

const string Logo =
@"| \ / 
=  =  
| / \
";


Console.WriteLine(Logo);
LegalAcrobatics.LegalComplianceAcrobatics().Wait();
Console.WriteLine("Ixr compiler - compile plc localized strings to resx dictonaries");
Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                var recoverCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                   Generate(o);
                   Console.WriteLine("Done.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    Environment.CurrentDirectory = recoverCurrentDirectory;
                }
            });



void Generate(Options o)
{

    var axProject = new AxProject(o.AxSourceProjectFolder);
    Console.WriteLine($"Compiling project {axProject.ProjectInfo.Name}...");
    var projectSources = axProject.Sources.Select(p => (parseTree: STParser.ParseTextAsync(p).Result, source: p));

    var syntaxTrees = projectSources.Select(p => p.parseTree);

    //var syntaxTree = toCompile.First(); // all_primitives.st

    var lw = new LocalizedStringWrapper();

    //iterate all syntax trees from project
    foreach (var syntaxTree in syntaxTrees)
    {
        Console.WriteLine(syntaxTree.Filename);
        IterateSyntaxTree(syntaxTree.GetRoot(),lw, syntaxTree.Filename);

        //IterateSyntaxTree(syntaxTree.GetRoot(),lw);
    }


    //print dictonary with localized strings and their ids
    foreach (var item in lw.LocalizedStringsDictionary)
    {
        Console.WriteLine($"{item.Key}: {item.Value.RawValue}, {item.Value.FileName},{item.Value.Line}");
    }

}

void IterateSyntaxTree(ISyntaxNode root, LocalizedStringWrapper lw, string fileName)
{
    foreach (var literalSyntax in GetChildNodesRecursive(root).OfType<ILiteralSyntax>())
    {
        var token = literalSyntax.Tokens.First();
        //literalSyntax.Location
        AddToDictionaryIfLocalizedString(token,lw,fileName);
    }
}

void AddToDictionaryIfLocalizedString(ISyntaxToken token, LocalizedStringWrapper lw, string fileName)
{
    // if is valid string token
    if(IsStringToken(token))
    {
        
        // try to acquire localized string
        var localizedString = lw.TryToGetLocalizedString(token.Text);
        if(localizedString == null) 
        {
            return;
        }
        //get raw text from localized string
        var rawText = lw.GetRawTextFromLocalizedString(localizedString);

        //create id
        var id = lw.CreateId(rawText);

        //check if identifier is valid
        if(lw.IsValidId(id))
        { 
            var pos = token.Location.GetLineSpan().StartLinePosition;
            var wrapper = new StringValueWrapper(rawText, fileName, pos.Line);
            // add id and wrapper to dictionary
            lw.LocalizedStringsDictionary.Add(id, wrapper);
        }
            
    }
}

bool IsStringToken(ISyntaxToken token)
{ 
    if(token.SyntaxKind == SyntaxKind.TypedStringDToken ||
        token.SyntaxKind == SyntaxKind.TypedStringSToken ||
        token.SyntaxKind == SyntaxKind.UntypedStringDToken ||
        token.SyntaxKind == SyntaxKind.UntypedStringSToken) 
    { 
        return true;
    }

    return false; 
}

IEnumerable<ISyntaxNode> GetChildNodesRecursive(ISyntaxNode syntaxNode)
{
    yield return syntaxNode;

    foreach (ISyntaxNode node in syntaxNode.ChildNodes)
    {
        foreach (ISyntaxNode childNode in GetChildNodesRecursive(node))
        {
            yield return childNode;
        }
    }
}

public static class LegalAcrobatics
{ 
    public static async Task LegalComplianceAcrobatics()
    {
        /*
         We need this to comply with the Siemens legal requirement to allow only users that have access
         to simatic-ax to some APIs.
        */
        
        var entryAssemblyLocation = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
        var stcapipath = Path.GetFullPath(Path.Combine(entryAssemblyLocation, $".apax//.apax//packages//@ax//{GetStcNameByPlatform()}//bin//"));

        if(!Directory.Exists(stcapipath))
        {
            await ApaxInstallLegalAcrobatics(entryAssemblyLocation);
        }
        
        SetupAssemblyResolverLegalAcrobatics(entryAssemblyLocation);
    }

    static IEnumerable<Assembly> AXAssemblies { get; set; }


    static void SetupAssemblyResolverLegalAcrobatics(string entryAssemblyLocation)
    {
        var axAssemblies = new List<Assembly>();

        foreach (var assemblyFile in Directory.EnumerateFiles(
                     Path.GetFullPath(Path.Combine(entryAssemblyLocation, $".apax//.apax//packages//@ax//{GetStcNameByPlatform()}//bin//")), "*.dll"))
        {
            try
            {
                axAssemblies.Add(Assembly.LoadFile(assemblyFile));
            }
            catch (System.BadImageFormatException e)
            {
                // We just ignore this...there might be some libraries that we just cannot load, but we do not need them.
            }
        }

        AXAssemblies = axAssemblies;

        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
    }

    static async Task ApaxInstallLegalAcrobatics(string entryAssemblyLocation)
    {
        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();
        try
        {
            Directory.CreateDirectory(Path.Combine(entryAssemblyLocation, ".apax"));
            Log.Logger.Information("Restoring apax packages.\n" +
                                   "You need have access to apax registry.\n" +
                                   "use 'apax login' if you did not do it yet.");
            var result = await Cli.Wrap("apax")
                .WithArguments("install")
                .WithWorkingDirectory(Path.Combine(entryAssemblyLocation, ".apax"))
                .WithValidation(CommandResultValidation.None)
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
                .WithStandardErrorPipe(PipeTarget.ToStringBuilder(stdErrBuffer))
                .ExecuteAsync();
        }
        catch(Exception ex)
        {
            Log.Logger.Error("There was a problem restoring apax packages.", ex);
        }
        finally
        {
            Log.Logger.Information(stdOutBuffer.ToString());
            Log.Logger.Information(stdErrBuffer.ToString());
        }
    }

    static Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
    {
        return AXAssemblies.FirstOrDefault(p => p.FullName == args.Name);
    }

  static string GetStcNameByPlatform()
       => RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "stc-linux-x64" : "stc-win-x64";
}


