using System;

namespace Pocos
{
    using RealMonsterData;

    public partial class integrated
    {
        public MonsterData.Monster Monster { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster OnlineToPlain_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster PlainToOnline_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster OnlineToShadowAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster ShadowToOnlineAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster ShadowToPlainAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public MonsterData.Monster PlainToShadowAsync_should_copy_entire_structure { get; set; } = new MonsterData.Monster();
        public Pokus Pokus { get; set; } = new Pokus();
        public RealMonsterData.RealMonster RealMonster { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster OnlineToShadow_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster ShadowToOnline_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster OnlineToPlain_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public RealMonsterData.RealMonster PlainToOnline_should_copy { get; set; } = new RealMonsterData.RealMonster();
        public all_primitives p_online_shadow { get; set; } = new all_primitives();
        public all_primitives p_shadow_online { get; set; } = new all_primitives();
        public all_primitives p_online_plain { get; set; } = new all_primitives();
        public all_primitives p_plain_online { get; set; } = new all_primitives();
        public all_primitives p_shadow_plain { get; set; } = new all_primitives();
        public all_primitives p_plain_shadow { get; set; } = new all_primitives();
    }

    public partial class Pokus
    {
        public Nested Nested { get; set; } = new Nested();
    }

    public partial class Nested
    {
        public string SomeString { get; set; } = string.Empty;
        public Int16 SomeInt { get; set; }

        public Byte SomeByte { get; set; }
    }
}