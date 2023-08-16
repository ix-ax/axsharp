using System.Reflection;

namespace AXSharp.LocalizablesToResx.Tests
{
    public class LocalizablesToResxTests
    {
        
        public LocalizablesToResxTests()
        {
                
        }

        [Fact]
        public void resxgen_sample_test()
        {
            Options args = new LocalizablesToResx.Options();
            args.SourceFile = @"./input/SimpleTests.razor";

            ResXGen.CreateRegex(null);
            ResXGen.CreateResxDictionary(args);

            Assert.Equal(3.ToString(), ResXGen.count.ToString());
            

        }

        [Fact]
        public void resxgen_different_identifier()
        {
            Options args = new LocalizablesToResx.Options();
            args.SourceFile = @"./input/DifferentIdentifier.razor";
            args.Identifier = "LocalizerDiff";

            ResXGen.CreateRegex(args.Identifier);
            ResXGen.CreateResxDictionary(args);

            Assert.Equal(2.ToString(), ResXGen.count.ToString());


        }
    }
}