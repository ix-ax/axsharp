CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1000ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
        Complex : ComplexForConfig;
        myBOOL	        :       BOOL	      ;
        myBYTE	        :       BYTE	      ;
        myWORD	        :       WORD	      ;
        myDWORD	        :       DWORD	      ;
        myLWORD	        :       LWORD	      ;
        mySINT	        :       SINT	      ;
        myINT	        :       INT	          ;
        myDINT	        :       DINT	      ;
        myLINT	        :       LINT	      ;
        myUSINT	        :       USINT	      ;
        myUINT	        :       UINT	      ;
        myUDINT	        :       UDINT	      ;
        myULINT	        :       ULINT	      ;
        myREAL	        :       REAL	      ;
        myLREAL	        :       LREAL	      ;
        myTIME	        :       TIME	      ;
        myLTIME	        :       LTIME	      ;
        myDATE	        :       DATE	      ;
        myLDATE	        :       LDATE	      ;
        myTIME_OF_DAY	:       TIME_OF_DAY	  ;
        myLTIME_OF_DAY	:       LTIME_OF_DAY  ;
        myDATE_AND_TIME  :       DATE_AND_TIME ;
        myLDATE_AND_TIME :       LDATE_AND_TIME;
        myCHAR	        :       CHAR	      ;
        myWCHAR	        :       WCHAR	      ;
        mySTRING	        :       STRING	      ;
        myWSTRING	    :       WSTRING	      ;

        {#ix-attr:[ReadOnce()]}
        myWSTRING_readOnce	    :       WSTRING	      ;
        {#ix-attr:[ReadOnly()]}
        myWSTRING_readOnly	    :       WSTRING	      ;
        {#ix-attr:[ReadOnce()]}
        cReadOnce           : ComplexForConfig;
        {#ix-attr:[ReadOnly()]}
        cReadOnly           : ComplexForConfig;
        Colorss : Colorss;
        Colorsss : Colorsss;

        {#ix-attr:[CompilerOmitsAttribute()]}
        _must_be_omitted_everywhere : BOOL;
        {#ix-attr:[CompilerOmitsAttribute("Onliner")]}
        _must_be_omitted_in_onliner : BOOL;
        {#ix-attr:[CompilerOmitsAttribute("POCO")]}
         _must_be_omitted_in_poco : BOOL;
    END_VAR
END_CONFIGURATION

CLASS PUBLIC ComplexForConfig
    VAR PUBLIC
        myBOOL	        :       BOOL	      ;
        myBYTE	        :       BYTE	      ;
        myWORD	        :       WORD	      ;
        myDWORD	        :       DWORD	      ;
        myLWORD	        :       LWORD	      ;
        mySINT	        :       SINT	      ;
        myINT	        :       INT	          ;
        myDINT	        :       DINT	      ;
        myLINT	        :       LINT	      ;
        myUSINT	        :       USINT	      ;
        myUINT	        :       UINT	      ;
        myUDINT	        :       UDINT	      ;
        myULINT	        :       ULINT	      ;
        myREAL	        :       REAL	      ;
        myLREAL	        :       LREAL	      ;
        myTIME	        :       TIME	      ;
        myLTIME	        :       LTIME	      ;
        myDATE	        :       DATE	      ;
        myLDATE	        :       LDATE	      ;
        myTIME_OF_DAY	:       TIME_OF_DAY	  ;
        myLTIME_OF_DAY	:       LTIME_OF_DAY  ;
        myDATE_AND_TIME  :       DATE_AND_TIME ;
        myLDATE_AND_TIME :       LDATE_AND_TIME;
        myCHAR	        :       CHAR	      ;
        myWCHAR	        :       WCHAR	      ;
        mySTRING	        :       STRING	      ;
        myWSTRING	    :       WSTRING	      ;
        myMotor         : Motor;
    END_VAR    
END_CLASS   

TYPE
    Colorss : (Red, Green, Blue) := Red;
END_TYPE

TYPE
    Colorsss : ULINT (Red := ULINT#1, Green := ULINT#2, Blue := ULINT#3) := Red;
END_TYPE

 TYPE
        Motor : STRUCT
            isRunning : BOOL;
        END_STRUCT;
        Vehicle : STRUCT
            m : Motor;
            displacement : INT;
        END_STRUCT;
    END_TYPE