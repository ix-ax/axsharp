{#ix[Container(Layout.Tabs)]}
CLASS weather
    VAR PUBLIC                        
        GeoLocation : GeoLocation;               
        {#ix[Container(Layout.Wrap)]}
        Temperature : REAL;
        Humidity : REAL;
        Location : STRING;  
        ChillFactor : REAL;
        Feeling : Feeling;
    END_VAR    
END_CLASS

CLASS weathers
    VAR PUBLIC
        i : ARRAY[0..50] OF weatherBase;
    END_VAR    
END_CLASS

TYPE
    ///<summary>
    /// Aa
    ///</summary>
    Feeling : (
        ///<summary>LK</summary>
        Freezing, 
        ///<summary>LO</summary>
        Cold, 
        ///<summary>LP</summary>
        Lookworm, 
        ///<summary>LU</summary>
        Warm, 
        ///<summary>LZ</summary>
        Hot
        );
END_TYPE

TYPE
    ///<summary>
    /// UU
    ///</summary>
    Colors : INT (
        ///<summary>BB</summary>
        RED := 12,
        ///<summary>Ahoj</summary>
        GREEN := 14,
        ///<summary>POIU</summary>
        BLUE := 23
    );
END_TYPE