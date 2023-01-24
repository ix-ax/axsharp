CONFIGURATION MyConfiguration
    TASK Main(Interval := T#10ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL      
        oop_counter : counter_oop;
    END_VAR
END_CONFIGURATION
