using Ix.Connector;
using Ix.Connector.ValueTypes;

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

        [Fact]
        public async Task OnlineToShadow_RealMonster_should_copy()
        {
            var monster = Entry.Plc.OnlineToPlain_should_copy;
            var today = DateTime.UtcNow;
            var date = new DateOnly(1999, 2, 13);
            var timespan = new TimeSpan(13, 13, 13);

            monster.TestDateTime.Cyclic = today;
            monster.TestDate.Cyclic = date;
            monster.TestTimeSpan.Cyclic = timespan;
            monster.Description.Cyclic = "from plain to online";

            monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic = 123;
            await monster.WriteAsync();

            await monster.OnlineToShadowAsync();
        


            Assert.Equal(monster.Description.Cyclic, monster.Description.Shadow);
            Assert.Equal(monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic, monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Shadow);
            Assert.Equal(monster.TestDate.Cyclic, monster.TestDate.Shadow);
            Assert.Equal(monster.TestDateTime.Cyclic, monster.TestDateTime.Shadow);
            Assert.Equal(monster.TestTimeSpan.Cyclic, monster.TestTimeSpan.Shadow);
        }

        [Fact]
        public async Task ShadowToOnline_RealMonster_should_copy()
        {
            var monster = Entry.Plc.OnlineToPlain_should_copy;
            var today = DateTime.UtcNow;
            var date = new DateOnly(1999, 2, 13);
            var timespan = new TimeSpan(13, 13, 13);

            monster.TestDateTime.Shadow = today;
            monster.TestDate.Shadow = date;
            monster.TestTimeSpan.Shadow = timespan;
            monster.Description.Shadow = "from plain to online";

            monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Shadow = 123;

            await monster.ShadowToOnlineAsync();



            Assert.Equal(monster.Description.Shadow, monster.Description.Cyclic);
            Assert.Equal(monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Shadow, monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic);
            Assert.Equal(monster.TestDate.Shadow, monster.TestDate.Cyclic);
            Assert.Equal(monster.TestDateTime.Shadow, monster.TestDateTime.Cyclic);
            Assert.Equal(monster.TestTimeSpan.Shadow, monster.TestTimeSpan.Cyclic);
        }

        [Fact]
        public async Task ShadowToOnline_primitives_should_copy()
        {
            var monster = Entry.Plc.OnlineToShadow_should_copy;
            var today = DateTime.UtcNow;
            var date = new DateOnly(1999, 2, 13);

            monster.primitives.mySTRING.Shadow = "from plain to online";
            monster.primitives.myBOOL.Shadow = true;
            monster.primitives.mySINT.Shadow = 13;
            monster.primitives.myTIME.Shadow = new TimeSpan(13, 13, 13);
            monster.primitives.myTIME_OF_DAY.Shadow = new TimeSpan(13, 13, 13);
            monster.primitives.myDATE.Shadow = date;
            monster.primitives.myDATE_AND_TIME.Shadow = today;
           

            await monster.ShadowToOnlineAsync();


            Assert.Equal(monster.primitives.mySTRING.Shadow, monster.primitives.mySTRING.Cyclic);
            Assert.Equal(monster.primitives.myBOOL.Shadow, monster.primitives.myBOOL.Cyclic);
            Assert.Equal(monster.primitives.mySINT.Shadow, monster.primitives.mySINT.Cyclic);
            Assert.Equal(monster.primitives.myTIME.Shadow, monster.primitives.myTIME.Cyclic);
            Assert.Equal(monster.primitives.myTIME_OF_DAY.Shadow, monster.primitives.myTIME_OF_DAY.Cyclic);
            Assert.Equal(monster.primitives.myDATE.Shadow, monster.primitives.myDATE.Cyclic);
            Assert.Equal(monster.primitives.myDATE_AND_TIME.Shadow, monster.primitives.myDATE_AND_TIME.Cyclic);
           
        }
    }
}