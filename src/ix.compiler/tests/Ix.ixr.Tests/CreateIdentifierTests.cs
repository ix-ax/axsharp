using Ix.ixr_doc;
using static System.Net.Mime.MediaTypeNames;

namespace Ix.ixr.Tests
{
    public class CreateIdentifierTests
    {
        private readonly LocalizedStringWrapper _lw;
        public CreateIdentifierTests()
        {
            _lw= new LocalizedStringWrapper();
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
        public void create_id_with_numbers()
        {
            //arrange
            var localizedString = "<#loca3lized1#>";
            var expected = "loca_THREE_lized_ONE_";
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
            var localizedString = "<#hello I am 1 localized string#>";
            var expected = "hello_I_am__ONE__localized_string";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert

            Assert.Equal(expected, result);

        }

        [Fact]
        public void create_id_with_other_characters()
        {
            //arrange
            var localizedString = "<#hello ! am I?#>";
            var expected = "hello__EXCLAMATION__am_I_QMARK_";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert

            Assert.Equal(expected, result);

        }

         [Fact]
        public void create_id_with_colon_semicolon_dash_dot()
        {
            //arrange
            var localizedString = "<#hello: this ; so- end.#>";
            var expected = "hello_COLON__this__SEMICOLON__so_DASH__end_DOT_";
            //act
            var rawText = _lw.GetRawTextFromLocalizedString(localizedString);
            var result = _lw.CreateId(rawText);
            //assert

            Assert.Equal(expected, result);

        }
    }
}