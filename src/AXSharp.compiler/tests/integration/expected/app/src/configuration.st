CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1000ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
        lib1_MyClass : lib1.MyClass;
        lib2_MyClass : lib2.MyClass;
    END_VAR
END_CONFIGURATION
