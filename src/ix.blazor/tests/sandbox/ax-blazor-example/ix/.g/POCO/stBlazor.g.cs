using System;

namespace Pocos
{
    public partial class stBlazor
    {
        public Int16 testInteger { get; set; }

        public stTestEnum testEnum { get; set; }

        public string testString { get; set; } = string.Empty;
        public Single testReal { get; set; }

        public Double testLReal { get; set; }

        public Boolean testBool { get; set; }

        public stComplex complexInstance { get; set; } = new stComplex();
        public stTest3 testInstance { get; set; } = new stTest3();
        public stTest3 testInstance2 { get; set; } = new stTest3();
        public stTest testInstance3 { get; set; } = new stTest();
    }
}