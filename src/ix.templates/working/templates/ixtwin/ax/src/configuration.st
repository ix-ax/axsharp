CONFIGURATION MyConfiguration
    TASK Main(Interval := T#10ms, Priority := 1);
    PROGRAM P1 WITH Main: MainProgram;

    VAR_GLOBAL

    END_VAR
END_CONFIGURATION
