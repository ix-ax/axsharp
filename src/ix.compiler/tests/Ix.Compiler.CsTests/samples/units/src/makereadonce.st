NAMESPACE makereadonce
    CLASS PUBLIC MembersWithMakeReadOnce
        VAR PUBLIC
            {#ix-attr:[ReadOnce()]}
            makeReadOnceMember : STRING;
            someOtherMember : STRING;

            {#ix-attr:[ReadOnce()]}
            makeReadComplexMember : ComplexMember;
            someotherComplexMember : ComplexMember;
        END_VAR    
    END_CLASS    

    CLASS PUBLIC ComplexMember
        VAR PUBLIC            
            someMember : STRING;
            someOtherMember : STRING;
        END_VAR    
    END_CLASS    
END_NAMESPACE
