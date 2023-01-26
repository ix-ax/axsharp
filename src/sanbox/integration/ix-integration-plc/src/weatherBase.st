
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

        {#ix-attr:[RenderIgnore()]} 
        {#ix-set:AttributeName = "THIS HAS [RenderIgnore()] must not be displayed!"} 
        RenderIgnoreAllToghether : STRING;
        {#ix-set:AttributeName = "THIS HAS [RenderIgnore(''Control'')] must not be displayed in 'Control' presentation type!"} 
        {#ix-attr:[RenderIgnore("Control")]} 
        RenderIgnoreWhenControl : STRING;
        {#ix-set:AttributeName = "THIS HAS [RenderIgnore(''Display'')] must not be displayed in 'Display' presentation type!"} 
        {#ix-attr:[RenderIgnore("Display")]} 
        RenderIgnoreWhenDisplay : STRING;
        {#ix-set:AttributeName = "THIS HAS [RenderIgnore(''Control'', ''ShadowControl'')] must not be displayed in 'Control' and 'ShadowControl' presentation type!"} 
        {#ix-attr:[RenderIgnore("Control", "ShadowControl")]} 
        RenderIgnoreWhenControlAndShadow : STRING;
        {#ix-set:AttributeName = "THIS HAS [RenderIgnore(''Display'', ''ShadowDisplay'')] must not be displayed in 'Display' and 'ShadowDisplay' presentation type!"} 
        {#ix-attr:[RenderIgnore("Display", "ShadowDisplay")]} 
        RenderIgnoreWhenDisplayAndShadow : STRING;
      END_VAR  
    END_CLASS    
