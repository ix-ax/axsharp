USING RealMonsterData;
CONFIGURATION MyConfiguration
    TASK Main(Interval := T#10ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL      
       Monster : MonsterData.Monster;
       OnlineToPlain_should_copy_entire_structure : MonsterData.Monster;
       PlainToOnline_should_copy_entire_structure : MonsterData.Monster;
       OnlineToShadowAsync_should_copy_entire_structure : MonsterData.Monster;
       ShadowToOnlineAsync_should_copy_entire_structure : MonsterData.Monster;
       
       ShadowToPlainAsync_should_copy_entire_structure : MonsterData.Monster;
       PlainToShadowAsync_should_copy_entire_structure : MonsterData.Monster;
       Pokus : Pokus;

       RealMonster : RealMonsterData.RealMonster;
       OnlineToShadow_should_copy : RealMonsterData.RealMonster;
       ShadowToOnline_should_copy : RealMonsterData.RealMonster;
       OnlineToPlain_should_copy: RealMonsterData.RealMonster;
       PlainToOnline_should_copy: RealMonsterData.RealMonster;

    END_VAR
END_CONFIGURATION

CLASS Pokus
    VAR PUBLIC
        Nested : Nested;
    END_VAR
    
END_CLASS

CLASS Nested
    VAR PUBLIC
        SomeString : STRING;
        SomeInt : INT;
        SomeByte : BYTE;
    END_VAR    
END_CLASS    
