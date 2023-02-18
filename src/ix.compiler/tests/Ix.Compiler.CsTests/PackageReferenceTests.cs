using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using YamlDotNet.Core;

namespace Ix.Compiler.CsTests
{
    public class PackageReferenceTests
    {
        private readonly string testFolder;

        private ITestOutputHelper output;

        protected IEnumerable<Type> builders;

        protected string OutputSubFolder;

        public PackageReferenceTests(ITestOutputHelper output)
        {
            this.output = output;

#pragma warning disable CS8604 // Possible null reference argument.
            var executingAssemblyFileInfo
                = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.

            testFolder = executingAssemblyFileInfo.Directory!.FullName;
        }

        [Fact]
        public void retrieve_metadata_from_package_success()
        {
            var packageFolder = Path.Combine(testFolder, $@"samples\packaging\");
            var packageReference = new PackageReference(packageFolder, "ix.framework.core", "0.0.0");

            Assert.True(File.Exists(packageReference.MetadataPath));
            Assert.True(packageReference.IsIxDependency);
        }
    }
}
