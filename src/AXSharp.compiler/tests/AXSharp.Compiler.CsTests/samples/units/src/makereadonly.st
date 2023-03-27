NAMESPACE makereadonly
    CLASS PUBLIC MembersWithMakeReadOnly
        VAR PUBLIC
            {#ix-attr:[ReadOnly()]}
            makeReadOnceMember : STRING;
            someOtherMember : STRING;

            {#ix-attr:[ReadOnly()]}
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
