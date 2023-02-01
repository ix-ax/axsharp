
CLASS example
    VAR PUBLIC
        {#ix-attr:[Container(Layout.Stack)]}
        primitives_stack: test_primitive;
        {#ix-attr:[Container(Layout.Wrap)]}
        primitives_wrap: test_primitive;
        {#ix-attr:[Container(Layout.Tabs)]}
        primitives_tabs: test_primitive;
        {#ix-attr:[Container(Layout.UniformGrid)]}
        primitives_uniform: test_primitive;

        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}    
        test_groupbox: test_primitive;

        {#ix-attr:[Container(Layout.Stack)]}
        {#ix-attr:[Group(GroupLayout.Border)]}    
        test_border: test_primitive;

        {#ix-attr:[Container(Layout.Tabs)]}
        {#ix-attr:[Group(GroupLayout.GroupBox)]}   
        testgroupbox: groupbox;
        testborder: border;
    END_VAR    
END_CLASS