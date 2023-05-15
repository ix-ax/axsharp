NAMESPACE Generics
    {#ix-generic:<TOnline, TPlain> where TOnline : ITwinObject}
    CLASS PUBLIC Extender
    
    END_CLASS
    
    
    CLASS PUBLIC Extendee EXTENDS Extender
        VAR PUBLIC
            {#ix-generic:TOnline}
            SomeType : SomeType;
            {#ix-generic:TPlain as POCO}
            SomeTypeAsPoco : SomeType;
        END_VAR
    END_CLASS
    
    CLASS PUBLIC Extendee2 EXTENDS Extender
        VAR PUBLIC
            {#ix-generic:TOnline}
            {#ix-generic:TPlain as POCO}
            SomeType : SomeType;          
        END_VAR
    END_CLASS

    CLASS SomeType 
    END_IF;
END_NAMESPACE

