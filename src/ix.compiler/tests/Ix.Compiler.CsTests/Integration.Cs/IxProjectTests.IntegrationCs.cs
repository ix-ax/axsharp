// Ix.Compiler.CsTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Xunit;
// Ix.CompilerTests
// Copyright (c)2022 Peter Kurhajec and Contributors All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using System.Reflection;
using Ix.Compiler;
using Ix.Compiler.Cs.Onliner;
using Ix.Compiler.Cs.Plain;
using Polly;
using Xunit.Abstractions;

namespace Ix.CompilerTests.Integration.Cs;

public class IxProjectTests
{
    private readonly ITestOutputHelper output;

    private readonly string testFolder;

    public IxProjectTests(ITestOutputHelper output)
    {
        this.output = output;

#pragma warning disable CS8604 // Possible null reference argument.
        var executingAssemblyFileInfo
            = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.

        testFolder = executingAssemblyFileInfo.Directory!.FullName;
    }

    [Fact]
    public void should_get_project_name()
    {
        var project = new IxProject(new AxProject(Path.Combine(testFolder, @"samples\units\")), new Type[] { },
            typeof(CsProject));
        var expected = Path.Combine(testFolder, @"samples\units\ix");
        var actual = project.OutputFolder;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void should_create_files_from_source_to_generated_output_folder()
    {
        var project = new IxProject(new AxProject(Path.Combine(testFolder, @"samples\units\")),
            new[] { typeof(CsPlainSourceBuilder) }, typeof(CsProject));


        Policy
            .Handle<Exception>()
            .WaitAndRetry(10, a => TimeSpan.FromSeconds(2))
            .Execute(() =>
            {
                if (Directory.Exists(project.OutputFolder)) Directory.Delete(project.OutputFolder, true);
            });


        var rootSourceFolder = Path.Combine(testFolder, @"samples\units\src");
        var expected = Directory.EnumerateFiles(
            rootSourceFolder, "*.st", SearchOption.AllDirectories).Select(p => p.Remove(0, rootSourceFolder.Length));

        project.Generate();


        var rootOutputFolder = Path.Combine(project.OutputFolder, ".g", "POCO");
        var actual = Directory.EnumerateFiles(
            rootOutputFolder, "*.*", SearchOption.AllDirectories).Select(p => p.Remove(0, rootOutputFolder.Length));


        Assert.Equal(expected.Count(), actual.Count());

        var actualList = actual.ToList();
        var index = 0;
        foreach (var exp in expected) Assert.Equal(exp.Replace(".st", ".g.cs"), actualList[index++]);
    }

    [Fact]
    public void should_clean_output_folder()
    {
        should_create_files_from_source_to_generated_output_folder();
        var project = new IxProject(new AxProject(Path.Combine(testFolder, @"samples\units\")),
            new[] { typeof(CsPlainSourceBuilder) }, typeof(CsProject));

        Assert.True(Directory.EnumerateFiles(project.OutputFolder, "*.g.cs", SearchOption.AllDirectories).Count() > 0);

        project.CleanOutput(project.OutputFolder);

        Assert.Equal(0, Directory.EnumerateFiles(project.OutputFolder, "*.g.cs", SearchOption.AllDirectories).Count());
    }

    [Fact]
    public void should_match_expected_and_generated_whole_project()
    {
        var project = new IxProject(new AxProject(Path.Combine(testFolder, @"samples\units\")),
            new[] { typeof(CsPlainSourceBuilder), typeof(CsOnlinerSourceBuilder) }, typeof(CsProject));

        if (Directory.Exists(project.OutputFolder)) Directory.Delete(project.OutputFolder, true);

        project.Generate();

        var rootSourceFolder = Path.Combine(testFolder, @"samples\units\expected\.g\");
        var expected = Directory.EnumerateFiles(
            rootSourceFolder, "*.g.cs", SearchOption.AllDirectories).Select(p => p);


        var rootOutputFolder = Path.Combine(project.OutputFolder, ".g");
        var actual = Directory.EnumerateFiles(
            rootOutputFolder, "*.*", SearchOption.AllDirectories).Select(p => p);


        Assert.Equal(expected.Count(), actual.Count());

        var actualList = actual.ToList();
        var index = 0;
        foreach (var exp in expected)
        {
            var currentIndex = index++;
            var expectedFileContent = File.ReadAllText(exp);
            var actualFileContent = File.ReadAllText(actualList[currentIndex]);
            try
            {
                Assert.Equal(expectedFileContent, actualFileContent);
            }
            catch (Exception)
            {
                output.WriteLine($"-- Case: {new FileInfo(exp).Name} vs {new FileInfo(actualList[currentIndex]).Name}");
                output.WriteLine($"-- expected\n{expectedFileContent}");
                output.WriteLine($"-- actual\n{actualFileContent}");
                throw;
            }
        }
    }

    [Fact]
    public void should_generate_all_even_when_fails_somewhere()
    {
        var project = new IxProject(new AxProject(Path.Combine(testFolder, @"samples\units\")),
            new[] { typeof(CsPlainSourceBuilder), typeof(CsOnlinerSourceBuilder) }, typeof(CsProject));

        if (Directory.Exists(project.OutputFolder)) Directory.Delete(project.OutputFolder, true);


        project.Generate();

        var rootSourceFolder = Path.Combine(testFolder, @"samples\units\expected\.g\");
        var expected = Directory.EnumerateFiles(
            rootSourceFolder, "*.g.cs", SearchOption.AllDirectories).Select(p => p);


        var rootOutputFolder = Path.Combine(project.OutputFolder, ".g");
        var actual = Directory.EnumerateFiles(
            rootOutputFolder, "*.*", SearchOption.AllDirectories).Select(p => p);


        //Assert.Equal(expected.Count(), actual.Count());

        var actualList = actual.ToList();
        var index = 0;
        foreach (var exp in expected)
        {
            var currentIndex = index++;
            var expectedFileContent = File.ReadAllText(exp);
            var actualFileContent = File.ReadAllText(actualList[currentIndex]);
            try
            {
                Assert.Equal(expectedFileContent, actualFileContent);
            }
            catch (Exception)
            {
                output.WriteLine($"-- Case: {new FileInfo(exp).Name} vs {new FileInfo(actualList[currentIndex]).Name}");
                output.WriteLine($"-- expected\n{expectedFileContent}");
                output.WriteLine($"-- actual\n{actualFileContent}");
            }
        }
    }

    [Fact]
    public void should_retrieve_dependencies_and_use_types_from_referenced_project()
    {
        var integrationProjectsPaths = new string[]
        {
            Path.GetFullPath(Path.Combine(testFolder, @"..\..\..\..\integration\actual\lib1")),
            Path.GetFullPath(Path.Combine(testFolder, @"..\..\..\..\integration\actual\lib2")),
            Path.GetFullPath(Path.Combine(testFolder, @"..\..\..\..\integration\actual\app"))
        };

        var projects = integrationProjectsPaths.Select(p => new IxProject(new AxProject(p),
            new[] { typeof(CsPlainSourceBuilder), typeof(CsOnlinerSourceBuilder) }, typeof(CsProject)));

        foreach (var project in projects)
        {
            project.Generate();
        }


        

        //if (Directory.Exists(project.OutputFolder))
        //{
        //    Directory.Delete(project.OutputFolder, true);
        //}

        

        


        var rootSourceFolder = Path.Combine(testFolder, @"..\..\..\..\integration\expected\app\ix\.g");
        var expected = Directory.EnumerateFiles(
            rootSourceFolder, "*.g.cs", SearchOption.AllDirectories).Select(p => p);


        var rootOutputFolder = Path.Combine(projects.Last().OutputFolder, ".g");
        var actual = Directory.EnumerateFiles(
            rootOutputFolder, "*.*", SearchOption.AllDirectories).Select(p => p);


        //Assert.Equal(expected.Count(), actual.Count());

        var actualList = actual.ToList();
        var index = 0;
        foreach (var exp in expected)
        {
            var currentIndex = index++;
            var expectedFileContent = File.ReadAllText(exp);
            var actualFileContent = File.ReadAllText(actualList[currentIndex]);
            try
            {
                Assert.Equal(expectedFileContent, actualFileContent);
            }
            catch (Exception)
            {
                output.WriteLine($"-- Case: {new FileInfo(exp).Name} vs {new FileInfo(actualList[currentIndex]).Name}");
                output.WriteLine($"-- expected\n{expectedFileContent}");
                output.WriteLine($"-- actual\n{actualFileContent}");
                throw;
            }
        }
    }
}