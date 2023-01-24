// Ix.CompilerTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Xunit;
using Ix.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Ix.Compiler.Exceptions;

namespace Ix.CompilerTests
{
    public class IxConfigTests
    {

        public IxConfigTests()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            var executingAssemblyFileInfo
                = new FileInfo(Assembly.GetExecutingAssembly().FullName);
#pragma warning restore CS8604 // Possible null reference argument.

            testFolder = executingAssemblyFileInfo.Directory!.FullName;
        }

        private readonly string testFolder;

        [Fact()]
        public void UpdateAndGetIxConfig_should_create_config_file_if_does_not_exist()
        {
           var apaxFolder = Path.Combine(testFolder, "samples", "plt","app");
           var ixConfigFile = Path.Combine(apaxFolder, "ix.config.json");
           if (File.Exists(ixConfigFile))
           {
               File.Delete(ixConfigFile);
           }

           var result = IxConfig.UpdateAndGetIxConfig(apaxFolder);
           
           Assert.True(File.Exists(ixConfigFile));
           
           Assert.Equal("ix", result.OutputProjectFolder);
           Assert.Equal(apaxFolder, result.AxProjectFolder);
        }

        [Fact]
        public void UpdateAndGetIxConfig_shoud_retrieve_existing_config_file()
        {
            var apaxFolder = Path.Combine(testFolder, "samples", "plt","lib");
            var ixConfigFile = Path.Combine(apaxFolder, "ix.config.json");
            Assert.True(File.Exists(ixConfigFile));
            var result = IxConfig.UpdateAndGetIxConfig(apaxFolder);
            Assert.True(File.Exists(ixConfigFile));
            Assert.Equal($"..{Path.DirectorySeparatorChar}ix", result.OutputProjectFolder);
            Assert.Equal(apaxFolder, result.AxProjectFolder);
        }

        [Fact]
        public void UpdateAndGetIxConfig_should_retrieve_existing_config_and_update_from_cli()
        {
            var apaxFolder = Path.Combine(testFolder, "samples", "plt","lib");
            var ixConfigFile = Path.Combine(apaxFolder, "ix.config.json");
            Assert.True(File.Exists(ixConfigFile));
            var result = IxConfig.UpdateAndGetIxConfig(apaxFolder, new IxConfig() 
                { AxProjectFolder = "hoho", OutputProjectFolder = "hehe"});
            Assert.True(File.Exists(ixConfigFile));
            Assert.Equal("hehe", result.OutputProjectFolder);
            Assert.Equal(apaxFolder, result.AxProjectFolder);
        }

        [Fact]
        public void RetrieveIxConfig_should_read_existing_config()
        {
            var apaxFolder = Path.Combine(testFolder, "samples", "plt","lib3");
            var ixConfigFile = Path.Combine(apaxFolder, "ix.config.json");
            Assert.True(File.Exists(ixConfigFile));
            var result = IxConfig.RetrieveIxConfig(ixConfigFile);
            Assert.True(File.Exists(ixConfigFile));
            Assert.Equal($"..{Path.DirectorySeparatorChar}ix", result.OutputProjectFolder);
            Assert.Equal(Path.Combine("samples","plt","lib3"), result.AxProjectFolder);
        }

        [Fact]
        public void RetrieveIxConfig_should_throw_exception_when_unable_to_process_config()
        {
            var apaxFolder = Path.Combine(testFolder, "samples", "plt","lib3");
            var ixConfigFile = Path.Combine(apaxFolder, "ix.config.json1");
            Assert.Throws<FailedToReadIxConfigurationFileException>(() =>IxConfig.RetrieveIxConfig(ixConfigFile));
        }
    }
}