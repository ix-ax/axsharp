// See https://aka.ms/new-console-template for more information
using AX.ST.Semantic;
using AX.ST.Syntax.Parser;
using AX.ST.Syntax.Tree;
using AX.Text;
using AX.Text.Diagnostics;
using Ix.Compiler;
using Ix.ixc_doc;
using Ix.ixc_doc.Interfaces;
using Ix.ixc_doc.Visitors;
using System;
using CommandLine;
using System.CommandLine;
using Ix.ixc_doc.Schemas;




//var fileOption = new Option<FileInfo?>(
//    name: "--file",
//    description: "The ST file to use.")
//{ IsRequired = false };

//var fileOptionFolder = new Option<FileInfo?>(
//    name: "--source-project-folder",
//    description: "Apax source folder.")
//{ IsRequired = false };

////setup cmd command
//var rootCommand = new RootCommand("ixc-doc - st to yml compiler for Docfx documentation");

//var generate = new Command("ixc-doc");
//generate.AddOption(fileOption);
//generate.AddOption(fileOptionFolder);
//generate.SetHandler(async file => { await Generate(file!); }, fileOption);
////generate.SetHandler(async () => { await GenerateProject(); });
////rootCommand.Add(generate);

//await rootCommand.InvokeAsync(args);

const string Logo =
@"| \ / 
=  =  
| / \
";


Console.WriteLine(Logo);
Console.WriteLine("ixc-doc compiler - st to yml compiler for Docfx");
Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                var recoverCurrentDirectory = Environment.CurrentDirectory;
                try
                {
                   GenerateYamls(o);
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


void GenerateYamls(Options o)
{
    //var axProject = new AxProject("D:\\Inxton\\ix-ax\\ix\\src\\ix.compiler\\src\\Ix.ixc-doc\\samples\\ax\\");
    // check for null ox axsourceprojectfolder
    var axProject = new AxProject(o.AxSourceProjectFolder);

    var projectSources = axProject.Sources.Select(p => (parseTree: STParser.ParseTextAsync(p).Result, source: p));

    var toCompile = projectSources.Select(p => p.parseTree);

    var compilation = Compilation.Create(toCompile, Compilation.Settings.Default).Result;

    var semanticTree = compilation.GetSemanticTree();

    //visit
    var myNodeVisitor = new MyNodeVisitor();
    var yamlSerializer = new YamlSerializer(o);
    var treeWalker = new YamlBuilder(yamlSerializer);

    semanticTree.GetRoot().Accept(myNodeVisitor, treeWalker);

    //serialize
    var x = 
    myNodeVisitor.TocSchema.Items = TocItemListToTocItem(myNodeVisitor.TocSchemaList.Items);
    yamlSerializer.TocToYaml(myNodeVisitor.TocSchema);
}

TocSchema.Item[] TocItemListToTocItem(List<TocSchemaList.ItemList> itemLists)
{
    List<TocSchema.Item> items = new List<TocSchema.Item>();
    foreach (var item in itemLists)
    {
        items.Add(new TocSchema.Item() { Uid = item.Uid, Name = item.Name, Items = TocItemListToTocItem(item.Items) });
    }
    return items.ToArray();
}

//for one file
async Task GenerateYamlFromFile(FileInfo file, Options o)
{

    var syntaxTree = await ParseST(file);

    if (!CheckSyntaxErrors(syntaxTree))
        return;

    var syntaxTrees = new List<ISyntaxTree>{syntaxTree};

    var compilation = Compilation.Create(syntaxTrees, Compilation.Settings.Default).Result;
    var semanticTree = compilation.GetSemanticTree();


    //visit
    var myNodeVisitor = new MyNodeVisitor();
    var yamlSerializer = new YamlSerializer(o);
    var treeWalker = new YamlBuilder(yamlSerializer);
    semanticTree.GetRoot().Accept(myNodeVisitor, treeWalker);


    //dump yaml schema
    yamlSerializer.SchemaToYaml(myNodeVisitor.Schema, file.Name.Split('.').First());

    

    Task<ISyntaxTree> ParseST(FileInfo file)
    {
        var sourceText = new SourceFileText(file.FullName);
        return STParser.ParseTextAsync(sourceText);
    }

    bool CheckSyntaxErrors(ISyntaxTree syntaxTree)
    {
        if (!syntaxTree.HasDiagnosticsOfSeverity(DiagnosticSeverity.Error)) return true;

        foreach (Diagnostic diagnostic in syntaxTree.GetDiagnostics(DiagnosticSeverity.Error))
        {
            Console.WriteLine($"Syntax error: {diagnostic}");
        }

        return false;
    }
}


