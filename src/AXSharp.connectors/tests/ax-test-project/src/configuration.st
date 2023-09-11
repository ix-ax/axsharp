CONFIGURATION MyConfiguration
    TASK Main(Interval := T#1ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL 

        myArrayString: ARRAY[0..10] OF STRING; 

        myArrayComplex: ARRAY[0..10] OF complexArrayItem; 

        mins : full_of_primitives;
        maxs : full_of_primitives;

        minsmatch : full_of_primitives_match;
        maxsmatch : full_of_primitives_match;

        myBOOL	        :       BOOL	 := TRUE     ;
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
        myDATE_AND_TIME  :      DATE_AND_TIME ;
        myLDATE_AND_TIME :      LDATE_AND_TIME;
        // myCHAR	        :       CHAR	      ;
        // myWCHAR	        :       WCHAR	      ;
        mySTRING	        :   STRING	      ;
        myWSTRING	    :       WSTRING	      ;


        
        
        myColors : Colors;

        //Hierarchy : hierarchy;
        // Issues  

        GH_PKTu_ix_56_Base: GH.PKTu.ix_56.Base;

        GH_PKTu_ix_56_FirstInheritance : GH.PKTu.ix_56.FirstInheritance;

        // GH_PKTu_ix_56_SecondInheritance : GH.PKTu.ix_56.SecondInheritance;
    END_VAR
END_CONFIGURATION
