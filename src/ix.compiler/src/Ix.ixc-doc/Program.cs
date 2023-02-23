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


const string Logo =
@"| \ / 
=  =  
| / \
";


Console.WriteLine(Logo);
Console.WriteLine("Ixd compiler - st to yml compiler for Docfx");
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
    myNodeVisitor.YamlHelper.TocSchema.Items = TocItemListToTocItem(myNodeVisitor.YamlHelper.TocSchemaList.Items);
    yamlSerializer.TocToYaml(myNodeVisitor.YamlHelper.TocSchema);
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


