using System;

namespace Pocos
{
    public partial class groupbox
    {
        public Int16 testInteger { get; set; }

        public Int16 testUInteger { get; set; }

        public string testString { get; set; } = string.Empty;
        public UInt16 testWord { get; set; }

        public Byte testByte { get; set; }

        public Single testReal { get; set; }

        public Double testLReal { get; set; }

        public Boolean testBool { get; set; }

        public DateOnly TestDate { get; set; } = default(DateOnly);
        public DateTime TestDateTime { get; set; } = default(DateTime);
        public TimeOnly TestTimeOfDay { get; set; } = default(TimeOnly);
        public enumStationStatus Status { get; set; }
    }
}