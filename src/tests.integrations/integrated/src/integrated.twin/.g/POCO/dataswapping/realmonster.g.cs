using System;

namespace Pocos
{
    namespace RealMonsterData
    {
        public partial class RealMonsterBase
        {
            public string Description { get; set; } = string.Empty;
            public UInt64 Id { get; set; }

            public DateOnly TestDate { get; set; } = default(DateOnly);
            public DateTime TestDateTime { get; set; } = default(DateTime);
            public TimeSpan TestTimeSpan { get; set; } = default(TimeSpan);
            public Byte[] ArrayOfBytes { get; set; } = new Byte[4];
            public RealMonsterData.DriveBaseNested[] ArrayOfDrives { get; set; } = new RealMonsterData.DriveBaseNested[4];
        }

        public partial class RealMonster : RealMonsterBase
        {
            public RealMonsterData.DriveBaseNested DriveA { get; set; } = new RealMonsterData.DriveBaseNested();
            public all_primitives primitives { get; set; } = new all_primitives();
        }

        public partial class DriveBaseNested
        {
            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }

            public RealMonsterData.NestedLevelOne NestedLevelOne { get; set; } = new RealMonsterData.NestedLevelOne();
        }

        public partial class NestedLevelOne
        {
            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }

            public RealMonsterData.NestedLevelTwo NestedLevelTwo { get; set; } = new RealMonsterData.NestedLevelTwo();
        }

        public partial class NestedLevelTwo
        {
            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }

            public RealMonsterData.NestedLevelThree NestedLevelThree { get; set; } = new RealMonsterData.NestedLevelThree();
        }

        public partial class NestedLevelThree
        {
            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }
        }
    }
}