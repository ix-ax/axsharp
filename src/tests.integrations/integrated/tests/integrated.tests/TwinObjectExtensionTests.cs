using Ix.Connector;
namespace integrated.tests
{
    public class TwinObjectExtensionTests
    {
        [Fact]
        public async  Task OnlineToShadowAsync_should_copy_entire_structure()
        {
            var monster = Entry.Plc.OnlineToShadowAsync_should_copy_entire_structure;
            monster.Description.Cyclic = "from online to shadow";
            monster.Id.Cyclic = 111222;
            monster.ArrayOfBytes[0].Cyclic = 11;
            monster.ArrayOfBytes[1].Cyclic = 22;
            monster.ArrayOfBytes[2].Cyclic = 33;

            monster.ArrayOfDrives[0].Velo.Cyclic = 110;
            monster.ArrayOfDrives[0].Acc.Cyclic = 120;
            monster.ArrayOfDrives[0].Dcc.Cyclic = 130;
            monster.ArrayOfDrives[0].Position.Cyclic = 140;

            monster.ArrayOfDrives[1].Velo.Cyclic = 210;
            monster.ArrayOfDrives[1].Acc.Cyclic = 220;
            monster.ArrayOfDrives[1].Dcc.Cyclic = 230;
            monster.ArrayOfDrives[1].Position.Cyclic = 240;

            monster.ArrayOfDrives[2].Velo.Cyclic = 310;
            monster.ArrayOfDrives[2].Acc.Cyclic = 320;
            monster.ArrayOfDrives[2].Dcc.Cyclic = 330;
            monster.ArrayOfDrives[2].Position.Cyclic = 340;

            await monster.OnlineToShadowAsync();

            Assert.Equal(monster.Description.Cyclic, monster.Description.Shadow);
            Assert.Equal(monster.Id.Cyclic, monster.Id.Shadow);
            Assert.Equal(monster.ArrayOfBytes[0].Cyclic          , monster.ArrayOfBytes[0].Shadow)          ;
            Assert.Equal(monster.ArrayOfBytes[1].Cyclic          , monster.ArrayOfBytes[1].Shadow)          ;
            Assert.Equal(monster.ArrayOfBytes[2].Cyclic          , monster.ArrayOfBytes[2].Shadow)          ;
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Cyclic    , monster.ArrayOfDrives[0].Velo.Shadow)    ;
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Cyclic     , monster.ArrayOfDrives[0].Acc.Shadow)     ;
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Cyclic     , monster.ArrayOfDrives[0].Dcc.Shadow)     ;
            Assert.Equal(monster.ArrayOfDrives[0].Position.Cyclic, monster.ArrayOfDrives[0].Position.Shadow);  
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Cyclic    , monster.ArrayOfDrives[1].Velo.Shadow)    ;
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Cyclic     , monster.ArrayOfDrives[1].Acc.Shadow)     ;
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Cyclic     , monster.ArrayOfDrives[1].Dcc.Shadow)     ;
            Assert.Equal(monster.ArrayOfDrives[1].Position.Cyclic, monster.ArrayOfDrives[1].Position.Shadow);  
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Cyclic    , monster.ArrayOfDrives[2].Velo.Shadow)    ;
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Cyclic     , monster.ArrayOfDrives[2].Acc.Shadow)     ;
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Cyclic     , monster.ArrayOfDrives[2].Dcc.Shadow)     ;
            Assert.Equal(monster.ArrayOfDrives[2].Position.Cyclic, monster.ArrayOfDrives[2].Position.Shadow);
        }

        [Fact]
        public async Task ShadowToOnlineAsync_should_copy_entire_structure()
        {
            var monster = Entry.Plc.ShadowToOnlineAsync_should_copy_entire_structure;
            monster.Description.Cyclic = "from shadow to online";
            monster.Id.Cyclic = 333444;
            
            monster.ArrayOfBytes[0].Shadow = 41;
            monster.ArrayOfBytes[1].Shadow = 42;
            monster.ArrayOfBytes[2].Shadow = 43;
            
            monster.ArrayOfDrives[0].Velo.Shadow = 510;
            monster.ArrayOfDrives[0].Acc.Shadow = 520;
            monster.ArrayOfDrives[0].Dcc.Shadow = 530;
            monster.ArrayOfDrives[0].Position.Shadow = 540;
            
            monster.ArrayOfDrives[1].Velo.Shadow = 610;
            monster.ArrayOfDrives[1].Acc.Shadow = 620;
            monster.ArrayOfDrives[1].Dcc.Shadow = 630;
            monster.ArrayOfDrives[1].Position.Shadow = 640;
            
            monster.ArrayOfDrives[2].Velo.Shadow = 710;
            monster.ArrayOfDrives[2].Acc.Shadow = 720;
            monster.ArrayOfDrives[2].Dcc.Shadow = 730;
            monster.ArrayOfDrives[2].Position.Shadow = 740;

            await monster.ShadowToOnlineAsync();
            await monster.ReadAsync();

            Assert.Equal(monster.Description.Shadow, monster.Description.Cyclic);
            Assert.Equal(monster.Id.Shadow, monster.Id.Cyclic);
            Assert.Equal(monster.ArrayOfBytes[0].Shadow, monster.ArrayOfBytes[0].Cyclic);
            Assert.Equal(monster.ArrayOfBytes[1].Shadow, monster.ArrayOfBytes[1].Cyclic);
            Assert.Equal(monster.ArrayOfBytes[2].Shadow, monster.ArrayOfBytes[2].Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Shadow, monster.ArrayOfDrives[0].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Shadow, monster.ArrayOfDrives[0].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Shadow, monster.ArrayOfDrives[0].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Shadow, monster.ArrayOfDrives[0].Position.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Shadow, monster.ArrayOfDrives[1].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Shadow, monster.ArrayOfDrives[1].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Shadow, monster.ArrayOfDrives[1].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Shadow, monster.ArrayOfDrives[1].Position.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Shadow, monster.ArrayOfDrives[2].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Shadow, monster.ArrayOfDrives[2].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Shadow, monster.ArrayOfDrives[2].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Shadow, monster.ArrayOfDrives[2].Position.Cyclic);

            Assert.Equal(monster.ArrayOfBytes[0].Shadow, monster.ArrayOfBytes[0].Cyclic);
            Assert.Equal(monster.ArrayOfBytes[1].Shadow, monster.ArrayOfBytes[1].Cyclic);
            Assert.Equal(monster.ArrayOfBytes[2].Shadow, monster.ArrayOfBytes[2].Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Shadow, monster.ArrayOfDrives[0].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Shadow, monster.ArrayOfDrives[0].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Shadow, monster.ArrayOfDrives[0].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Shadow, monster.ArrayOfDrives[0].Position.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Shadow, monster.ArrayOfDrives[1].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Shadow, monster.ArrayOfDrives[1].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Shadow, monster.ArrayOfDrives[1].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Shadow, monster.ArrayOfDrives[1].Position.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Shadow, monster.ArrayOfDrives[2].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Shadow, monster.ArrayOfDrives[2].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Shadow, monster.ArrayOfDrives[2].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Shadow, monster.ArrayOfDrives[2].Position.Cyclic);
        }
    }
}