using Ix.Connector;
namespace integrated.tests
{
    public class PlainersSwappingTests
    {
        [Fact]
        public async  Task OnlineToPlain_should_copy_entire_structure()
        {
            var monster = Entry.Plc.OnlineToPlain_should_copy_entire_structure;

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

            await monster.WriteAsync();

            var p = await monster.OnlineToPlainAsync();

            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.Id.Cyclic, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Cyclic          , p.ArrayOfBytes[0])          ;
            Assert.Equal(monster.ArrayOfBytes[1].Cyclic          , p.ArrayOfBytes[1])          ;
            Assert.Equal(monster.ArrayOfBytes[2].Cyclic          , p.ArrayOfBytes[2])          ;
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Cyclic    , p.ArrayOfDrives[0].Velo)    ;
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Cyclic     , p.ArrayOfDrives[0].Acc)     ;
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Cyclic     , p.ArrayOfDrives[0].Dcc)     ;
            Assert.Equal(monster.ArrayOfDrives[0].Position.Cyclic, p.ArrayOfDrives[0].Position);  
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Cyclic    , p.ArrayOfDrives[1].Velo)    ;
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Cyclic     , p.ArrayOfDrives[1].Acc)     ;
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Cyclic     , p.ArrayOfDrives[1].Dcc)     ;
            Assert.Equal(monster.ArrayOfDrives[1].Position.Cyclic, p.ArrayOfDrives[1].Position);  
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Cyclic    , p.ArrayOfDrives[2].Velo)    ;
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Cyclic     , p.ArrayOfDrives[2].Acc)     ;
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Cyclic     , p.ArrayOfDrives[2].Dcc)     ;
            Assert.Equal(monster.ArrayOfDrives[2].Position.Cyclic, p.ArrayOfDrives[2].Position);
        }

        [Fact]
        public async Task PlainToOnline_should_copy_entire_structure()
        {
            var monster = Entry.Plc.PlainToOnline_should_copy_entire_structure;

            var p = new Pocos.MonsterData.Monster();

            p.Description = "from plain to online";
            p.Id = 111222;
            p.ArrayOfBytes[0] = 11;
            p.ArrayOfBytes[1] = 22;
            p.ArrayOfBytes[2] = 33;

            for (int i = 0; i < p.ArrayOfDrives.Length; i++)
            {
                p.ArrayOfDrives[i] = new();
            }


            p.ArrayOfDrives[0].Velo = 110;
            p.ArrayOfDrives[0].Acc = 120;
            p.ArrayOfDrives[0].Dcc = 130;
            p.ArrayOfDrives[0].Position = 140;
            
            p.ArrayOfDrives[1].Velo = 210;
            p.ArrayOfDrives[1].Acc = 220;
            p.ArrayOfDrives[1].Dcc = 230;
            p.ArrayOfDrives[1].Position = 240;
            
            p.ArrayOfDrives[2].Velo = 310;
            p.ArrayOfDrives[2].Acc = 320;
            p.ArrayOfDrives[2].Dcc = 330;
            p.ArrayOfDrives[2].Position = 340;

            await monster.PlainToOnlineAsync(p);

            await monster.WriteAsync();
            await monster.ReadAsync();


            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.Id.Cyclic, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Cyclic, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Cyclic, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Cyclic, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Cyclic, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Cyclic, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Cyclic, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Cyclic, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Cyclic, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Cyclic, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Cyclic, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Cyclic, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Cyclic, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Cyclic, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Cyclic, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Cyclic, p.ArrayOfDrives[2].Position);
        }




        [Fact]
        public async Task OnlineToPlain_RealMonster_should_copy()
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


            var p = await monster.OnlineToPlainAsync();


            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic, p.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc);
            Assert.Equal(monster.TestDate.Cyclic, p.TestDate);
            Assert.Equal(monster.TestDateTime.Cyclic, p.TestDateTime);
            Assert.Equal(monster.TestTimeSpan.Cyclic, p.TestTimeSpan);
        }
    }
}