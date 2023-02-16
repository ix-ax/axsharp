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