PROGRAM MyProgram
    VAR_EXTERNAL          
        StartPolling_should_update_cyclic_property : RealMonsterData.RealMonster;         
        GH_ISSUE_183 : GH_ISSUE_183.GH_ISSUE_183_1;
    END_VAR        
    StartPolling_should_update_cyclic_property.Id := StartPolling_should_update_cyclic_property.Id + ULINT#1;

    StartPolling_should_update_cyclic_property.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position := StartPolling_should_update_cyclic_property.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position  + REAL#1.0;
    
END_PROGRAM
