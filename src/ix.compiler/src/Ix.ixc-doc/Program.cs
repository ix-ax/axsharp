// See https://aka.ms/new-console-template for more information
using AX.ST.Semantic;
using AX.ST.Syntax.Parser;
using AX.ST.Syntax.Tree;
using AX.Text;
using AX.Text.Diagnostics;
using Ix.Compiler;
using Ix.ixc_doc;
using Ix.ixc_doc.Interfaces;
using ix_doc_compiler;
using System;
using System.CommandLine;
using System.Linq.Expressions;




var fileOption = new Option<FileInfo?>(
    name: "--file",
    description: "The ST file to use.")
{ IsRequired = false };

var fileOptionFolder = new Option<FileInfo?>(
    name: "--source-project-folder",
    description: "Apax source folder.")
{ IsRequired = false };

//setup cmd command
var rootCommand = new RootCommand("ixc-doc - st to yml compiler for Docfx documentation");

var generate = new Command("ixc-doc");
//generate.AddOption(fileOption);
//generate.AddOption(fileOptionFolder);
//generate.SetHandler(async file => { await Generate(file!); }, fileOption);
generate.SetHandler(async () => { await GenerateProject(); });
rootCommand.Add(generate);




await rootCommand.InvokeAsync(args);



async Task GenerateProject()
{
    var axProject = new AxProject("D:\\Inxton\\ix-ax\\ix\\src\\ix.compiler\\src\\Ix.ixc-doc\\samples\\ax\\");

    var projectSources = axProject.Sources.Select(p => (parseTree: STParser.ParseTextAsync(p).Result, source: p));
    var toCompile = projectSources.Select(p => p.parseTree);

    var compilation = Compilation.Create(toCompile, Compilation.Settings.Default).Result;
    var semanticTree = compilation.GetSemanticTree();

    //visit
    var myNodeVisitor = new MyNodeVisitor();
    var treeWalker = new YamlBuilder();

    semanticTree.GetRoot().Accept(myNodeVisitor, treeWalker);
    var x = new YamlSerializer();
    myNodeVisitor.TocSchema.Items = myNodeVisitor.TocSchemaItems.ToArray();
    x.TocToYaml(myNodeVisitor.TocSchema);

}
    async Task Generate(FileInfo file)
{
    

    Console.WriteLine("ixc-doc Compiler");
    Console.WriteLine($"Using source ${file.FullName}");
    Console.WriteLine("Parsing...");
    var syntaxTree = await ParseST(file);

    if (!CheckSyntaxErrors(syntaxTree))
        return;

    var syntaxTrees = new List<ISyntaxTree>();
    syntaxTrees.Add(syntaxTree);
    var compilation = Compilation.Create(syntaxTrees, Compilation.Settings.Default).Result;
    var semanticTree = compilation.GetSemanticTree();
    Console.WriteLine("Compiling...");
    //visit
    var myNodeVisitor = new MyNodeVisitor();
    var treeWalker = new YamlBuilder();
    semanticTree.GetRoot().Accept(myNodeVisitor, treeWalker);


    //dump yaml schema
    var x = new YamlSerializer();
    x.SchemaToYaml(myNodeVisitor.Schema, file.Name.Split('.').First());

    Console.WriteLine("Done.");

}

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