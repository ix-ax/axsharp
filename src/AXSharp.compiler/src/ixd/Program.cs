﻿// See https://aka.ms/new-console-template for more information
using AX.ST.Semantic;
using AX.ST.Syntax.Parser;
using AX.ST.Syntax.Tree;
using AX.Text;
using AX.Text.Diagnostics;
using AXSharp.Compiler;
using AXSharp.ixc_doc;
using AXSharp.ixc_doc.Interfaces;
using AXSharp.ixc_doc.Visitors;
using System;
using CommandLine;
using AXSharp.ixc_doc.Schemas;
using System.Reflection;
using System.Text;
using CliWrap;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

const string Logo =
@"| \ / 
=  =  
| / \
";


Console.WriteLine(Logo);
LegalAcrobatics.LegalComplianceAcrobatics(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName).Wait();
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
    
    DeleteYamlFilesIfExists(o.OutputProjectFolder);

    var axProject = new AxProject(o.AxSourceProjectFolder);
    Console.WriteLine($"Compiling project {axProject.ProjectInfo.Name}...");
    var projectSources = axProject.Sources.Select(p => (parseTree: STParser.ParseTextAsync(p).Result, source: p));

    var toCompile = projectSources.Select(p => p.parseTree);

    var compilation = Compilation.Create(toCompile, Compilation.Settings.Default).Result;

    var semanticTree = compilation.GetSemanticTree();

    //visit
    var myNodeVisitor = new MyNodeVisitor(axProject);
    var yamlSerializer = new YamlSerializer(o);
    var treeWalker = new YamlBuilder(yamlSerializer, axProject.ProjectFile);

    semanticTree.GetRoot().Accept(myNodeVisitor, treeWalker);

    //serialize
    yamlSerializer.TocToYaml(myNodeVisitor.YamlHelper.TocSchema);
}


void DeleteYamlFilesIfExists(string outputPath)
{
    if (!Directory.Exists(outputPath)) return;

    DirectoryInfo di = new DirectoryInfo(outputPath);

    var ymlFiles = di.GetFiles("*.yml");
    if(ymlFiles != null)
    { 
        foreach (FileInfo file in ymlFiles)
        {
            file.Delete(); 
        }
    }
 }


