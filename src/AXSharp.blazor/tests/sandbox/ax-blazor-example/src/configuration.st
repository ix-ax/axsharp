CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1000ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
        testingProgram : testingProgram; 
        prgWeatherStations : prgWeatherStations;
        MAINC : MAIN;       
    END_VAR
END_CONFIGURATION
