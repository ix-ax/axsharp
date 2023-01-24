
    {#ix-attr:[Container(Layout.Stack)]}
    {#ix-attr:[Group(Layout.GroupBox)]}    
    CLASS PUBLIC weatherBase
      VAR PUBLIC
        Latitude : REAL;       
        Longitude : REAL;       
        Altitude : REAL;                
        Description : STRING;    
        {#ix-attr:[ReadOnly()]}   
        LongDescription : STRING;
        {#ix-attr:[ReadOnce()]} 
        {#ix-set:AttributeName = "this has [ReadOnce()] attribute will be readon only once..."} 
        StartCounter : INT;
      END_VAR  
    END_CLASS    
