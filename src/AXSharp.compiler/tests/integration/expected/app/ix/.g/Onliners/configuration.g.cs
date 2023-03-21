using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class appTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public lib1.MyClass lib1_MyClass { get; }

    public lib2.MyClass lib2_MyClass { get; }

    public appTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        lib1_MyClass = new lib1.MyClass(this.Connector, "", "lib1_MyClass");
        lib2_MyClass = new lib2.MyClass(this.Connector, "", "lib2_MyClass");
    }

    public appTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        lib1_MyClass = new lib1.MyClass(this.Connector, "", "lib1_MyClass");
        lib2_MyClass = new lib2.MyClass(this.Connector, "", "lib2_MyClass");
    }
}