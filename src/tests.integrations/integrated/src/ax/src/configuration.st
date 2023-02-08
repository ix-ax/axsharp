CONFIGURATION MyConfiguration
    TASK Main(Interval := T#10ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL      
       Monster : MonsterData.Monster;
    END_VAR
END_CONFIGURATION
