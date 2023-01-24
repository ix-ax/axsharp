CLASS weather
    VAR PUBLIC            
        Temperature : REAL;
        Humidity : REAL;
        Location : STRING; 
        HeyMan : STRING; 
        GeoLocation : GeoLocation; 
    END_VAR    
END_CLASS

CLASS weathers
    VAR PUBLIC
        i : ARRAY[0..10] OF weather;
    END_VAR    
END_CLASS