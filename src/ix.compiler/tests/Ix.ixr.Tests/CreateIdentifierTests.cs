using Ix.ixr_doc;
using static System.Net.Mime.MediaTypeNames;

namespace Ix.ixr.Tests
{
    public class CreateIdentifierTests
    {
        private readonly LocalizedStringWrapper _lw;

        public CreateIdentifierTests()
        {
            _lw = new LocalizedStringWrapper();
        }

        [Fact]
        public void create_base_id()
        {
            //arrange
            var localizedString = "<#localized#>";
            var expected = "localized";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void create_empty_id()
        {
            //arrange
            var localizedString = "<##>";
            var expected = "";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void create_id_with_numbers()
        {
            //arrange
            var localizedString = "<#03951#>";
            var expected = "_ZERO__THREE__NINE__FIVE__ONE_";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void create_id_with_whitespaces()
        {
            //arrange
            var localizedString = "<#hello I am localized string #>";
            var expected = "hello_I_am_localized_string_";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void create_id_with_supported_characters()
        {
            //arrange
            var localizedString = "<#!,.: ;-? #>";
            var expected = "_EXCLAMATION__COMMA__DOT__COLON___SEMICOLON__DASH__QMARK__";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void create_id_with_not_supported_characters()
        {
            //arrange
            var localizedString = "<#'\\\"|@&#[]<>{}()$#>";
            var expected = "________________";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void create_complex_id_with_numbers()
        {
            //arrange
            var localizedString = "<#4loc8 aliz2ed7 #>";
            var expected = "_FOUR_loc_EIGHT__aliz_TWO_ed_SEVEN__";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void create_complex_id_with_supported_characters()
        {
            //arrange
            var localizedString = "<#.loc;ali!zed- #>";
            var expected = "_DOT_loc_SEMICOLON_ali_EXCLAMATION_zed_DASH__";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void create_complex_id_with_not_supported_characters()
        {
            //arrange
            var localizedString = "<#\\loc#ali'zed\"#>";
            var expected = "_loc_ali_zed_";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void create_complex_id()
        {
            //arrange
            var localizedString = "<# ?\"loc3ali-zed 6str.in#g!#>";
            var expected = "__QMARK__loc_THREE_ali_DASH_zed__SIX_str_DOT_in_g_EXCLAMATION_";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert
            Assert.Equal(expected, result);
        }
    }
}