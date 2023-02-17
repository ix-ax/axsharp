using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;

namespace Ix.Compiler.CsTests
{
    public class PackageReferenceTests
    {
        [Fact]
        public void Test()
        {
            var packageFolder = "";
            var packageReference = new PackageReference(packageFolder, "ix.package", "0.0.0");
        }
    }
}
