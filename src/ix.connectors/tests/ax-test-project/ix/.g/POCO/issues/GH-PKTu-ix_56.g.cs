using System;

namespace Pocos
{
    namespace GH.PKTu.ix_56
    {
        public partial class ComplexMember
        {
            public Int16 Counter { get; set; }
        }

        public partial class Base
        {
            public string baseMember { get; set; } = string.Empty;
            public GH.PKTu.ix_56.ComplexMember baseComplexMember { get; set; } = new GH.PKTu.ix_56.ComplexMember();
            public GH.PKTu.ix_56.DavidBase BaseDavid { get; set; } = new GH.PKTu.ix_56.DavidBase();
        }

        public partial class FirstInheritance : Base
        {
            public string FirstInheritanceMember { get; set; } = string.Empty;
            public GH.PKTu.ix_56.ComplexMember FirstInheritanceComplexMember { get; set; } = new GH.PKTu.ix_56.ComplexMember();
            public GH.PKTu.ix_56.DavidBase FirstDavid { get; set; } = new GH.PKTu.ix_56.DavidBase();
        }

        public partial class SecondInheritance : FirstInheritance
        {
            public string SecondInheritanceMember { get; set; } = string.Empty;
            public GH.PKTu.ix_56.ComplexMember SecondInheritanceComplexMember { get; set; } = new GH.PKTu.ix_56.ComplexMember();
            public GH.PKTu.ix_56.DavidBase SecodnDavid { get; set; } = new GH.PKTu.ix_56.DavidBase();
        }

        public partial class PedroBase
        {
            public string p { get; set; } = string.Empty;
        }

        public partial class DavidBase : PedroBase
        {
            public string d { get; set; } = string.Empty;
        }
    }
}