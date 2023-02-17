NAMESPACE misc
    CLASS PUBLIC VariousMembers
        VAR PUBLIC
            _SomeClass : SomeClass;  
            _Motor : Motor;  
        END_VAR    
    END_CLASS    
    
      
    CLASS SomeClass
        VAR PUBLIC            
            SomeClassVariable : STRING;
        END_VAR
    END_CLASS

    TYPE
        Motor : STRUCT
            isRunning : BOOL;
        END_STRUCT;
        Vehicle : STRUCT
            m : Motor;
            displacement : INT;
        END_STRUCT;
    END_TYPE
END_NAMESPACE

NAMESPACE UnknownArraysShouldNotBeTraspiled
    CLASS ClassWithArrays
        VAR PUBLIC
            _complexKnown : ARRAY[0..10] OF Complex;
            _complexUnknown : ARRAY [0..10] OF ComplexUnknown;
            _primitive  : ARRAY[0..10] OF BYTE;
        END_VAR
        
    END_CLASS


    CLASS PUBLIC Complex 
        VAR PUBLIC
            HelloString : STRING;
            Id : ULINT;
        END_VAR        
    END_CLASS

    
END_NAMESPACE