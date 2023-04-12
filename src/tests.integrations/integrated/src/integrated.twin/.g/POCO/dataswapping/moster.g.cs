using System;

namespace Pocos
{
    namespace MonsterData
    {
        public partial class MonsterBase : AXSharp.Connector.IPlain
        {
            public string Description { get; set; } = string.Empty;
            public UInt64 Id { get; set; }

            public Byte[] ArrayOfBytes { get; set; } = new Byte[4];
            public MonsterData.DriveBase[] ArrayOfDrives { get; set; } = new MonsterData.DriveBase[4];
        }

        public partial class Monster : MonsterData.MonsterBase, AXSharp.Connector.IPlain
        {
            public MonsterData.DriveBase DriveA { get; set; } = new MonsterData.DriveBase();
        }

        public partial class DriveBase : AXSharp.Connector.IPlain
        {
            public Double Position { get; set; }

            public Double Velo { get; set; }

            public Double Acc { get; set; }

            public Double Dcc { get; set; }
        }
    }
}