FUNCTION_BLOCK TestFunctionBlock
    VAR_INPUT
        x : INT;
    END_VAR
    VAR_OUTPUT
        y : INT;
    END_VAR
    VAR
        s : INT;
    END_VAR
    s := s + x;
    y := y + s;
END_FUNCTION_BLOCK