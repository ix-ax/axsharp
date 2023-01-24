
{#ix-attr:[Container(Layout.Wrap)]}
CLASS PUBLIC counter_oop
    VAR PUBLIC
        Counter : ULINT;
        AllowCounter : BOOL;
    END_VAR

    METHOD PUBLIC RunCounter        
        IF (AllowCounter) THEN
            Counter := Counter + ULINT#1;    
        END_IF;           
    END_METHOD
    
END_CLASS