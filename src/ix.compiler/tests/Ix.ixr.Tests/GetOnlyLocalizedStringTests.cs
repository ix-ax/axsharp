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

            var result = _lw.TryToGetLocalizedString(localizedString);
            //assert

            Assert.Equal(expected, result);

        }

        [Fact]
        public void get_complex_localized_string()
        {
            //arrange
            var localizedString = "dafadsf432<#localized#>4343";
            var expected = "<#localized#>";
            //act

            var result = _lw.TryToGetLocalizedString(localizedString);
            //assert

            Assert.Equal(expected, result);

        }

         [Fact]
        public void get_empty_localized_string()
        {
            //arrange
            var localizedString = "<##>";
            var expected = "<##>";
            //act

            var result = _lw.TryToGetLocalizedString(localizedString);
            //assert

            Assert.Equal(expected, result);

        }
    }
}
