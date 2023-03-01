using Ix.ixr_doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.ixr.Tests
{
    public class GetOnlyLocalizedStringTests
    {
        private readonly LocalizedStringWrapper _lw;
        public GetOnlyLocalizedStringTests()
        {
            _lw= new LocalizedStringWrapper();
        }

         [Fact]
        public void get_base_localized_string()
        {
            //arrange
            var localizedString = "<#localized#>";
            var expected = "<#localized#>";
            //act

            var result = _lw.TryToGetLocalizedStrings(localizedString);
            //assert

            Assert.Equal(expected, result.First());

        }

        [Fact]
        public void get_complex_localized_string()
        {0
            //arrange
            var localizedString = "dafadsf432<#localized#>4343";
            var expected = "<#localized#>";
            //act

            var result = _lw.TryToGetLocalizedStrings(localizedString);
            //assert

            Assert.Equal(expected, result.First());

        }

         [Fact]
        public void get_empty_localized_string()
        {
            //arrange
            var localizedString = "<##>";
            var expected = "<##>";
            //act

            var result = _lw.TryToGetLocalizedStrings(localizedString);
            //assert

            Assert.Equal(expected, result.First());

        }


        [Fact]
        public void get_more_localized_strings()
        {
            //arrange
            var localizedStrings = "<#test1#> fdasf 32 <#test2#>";
            var expected1 = "<#test1#>";
            var expected2 = "<#test2#>";
            //act

            var result = _lw.TryToGetLocalizedStrings(localizedStrings);
            //assert
            
            Assert.Equal(expected1, result.First());
            Assert.Equal(expected2, result.Last());
            

        }
    }
}
