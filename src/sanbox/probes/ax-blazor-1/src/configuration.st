CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1000ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
        myINT : INT;
        weather : weather;
        weathers : weathers;
    END_VAR
END_CONFIGURATION
