NAMESPACE RefToSimple
    {S7.extern=ReadWrite}
    CLASS ref_to_simple
        VAR PUBLIC
            a : REF_TO INT;
            b : REF_TO referenced;
        END_VAR        
    END_CLASS
    
    {S7.extern=ReadWrite}
    CLASS referenced
        VAR PUBLIC
            b : INT;
        END_VAR        
    END_CLASS
END_NAMESPACE