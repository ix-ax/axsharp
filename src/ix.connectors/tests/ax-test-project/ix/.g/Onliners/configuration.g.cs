using System;
using Ix.Connector;
using Ix.Connector.ValueTypes;
using System.Collections.Generic;

public partial class ax_test_projectTwinController : ITwinController
{
    public Ix.Connector.Connector Connector { get; }

    public full_of_primitives mins { get; }

    public full_of_primitives maxs { get; }

    public full_of_primitives_match minsmatch { get; }

    public full_of_primitives_match maxsmatch { get; }

    public OnlinerBool myBOOL { get; }

    public OnlinerByte myBYTE { get; }

    public OnlinerWord myWORD { get; }

    public OnlinerDWord myDWORD { get; }

    public OnlinerLWord myLWORD { get; }

    public OnlinerSInt mySINT { get; }

    public OnlinerInt myINT { get; }

    public OnlinerDInt myDINT { get; }

    public OnlinerLInt myLINT { get; }

    public OnlinerUSInt myUSINT { get; }

    public OnlinerUInt myUINT { get; }

    public OnlinerUDInt myUDINT { get; }

    public OnlinerULInt myULINT { get; }

    public OnlinerReal myREAL { get; }

    public OnlinerLReal myLREAL { get; }

    public OnlinerTime myTIME { get; }

    public OnlinerLTime myLTIME { get; }

    public OnlinerDate myDATE { get; }

    public OnlinerDate myLDATE { get; }

    public OnlinerTimeOfDay myTIME_OF_DAY { get; }

    public OnlinerLTimeOfDay myLTIME_OF_DAY { get; }

    public OnlinerDateTime myDATE_AND_TIME { get; }

    public OnlinerLDateTime myLDATE_AND_TIME { get; }

    public OnlinerChar myCHAR { get; }

    public OnlinerWChar myWCHAR { get; }

    public OnlinerString mySTRING { get; }

    public OnlinerWString myWSTRING { get; }

    [Ix.Connector.EnumeratorDiscriminatorAttribute(typeof(Colors))]
    public OnlinerInt myColors { get; }

    public GH.PKTu.ix_56.SecondInheritance GH_PKTu_ix_56_SecondInheritance { get; }

    public ax_test_projectTwinController(Ix.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        mins = new full_of_primitives(this.Connector, "", "mins");
        maxs = new full_of_primitives(this.Connector, "", "maxs");
        minsmatch = new full_of_primitives_match(this.Connector, "", "minsmatch");
        maxsmatch = new full_of_primitives_match(this.Connector, "", "maxsmatch");
        myBOOL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "myBOOL");
        myBYTE = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this.Connector, "", "myBYTE");
        myWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this.Connector, "", "myWORD");
        myDWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateDWORD(this.Connector, "", "myDWORD");
        myLWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateLWORD(this.Connector, "", "myLWORD");
        mySINT = @Connector.ConnectorAdapter.AdapterFactory.CreateSINT(this.Connector, "", "mySINT");
        myINT = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "myINT");
        myDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateDINT(this.Connector, "", "myDINT");
        myLINT = @Connector.ConnectorAdapter.AdapterFactory.CreateLINT(this.Connector, "", "myLINT");
        myUSINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUSINT(this.Connector, "", "myUSINT");
        myUINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUINT(this.Connector, "", "myUINT");
        myUDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUDINT(this.Connector, "", "myUDINT");
        myULINT = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this.Connector, "", "myULINT");
        myREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this.Connector, "", "myREAL");
        myLREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this.Connector, "", "myLREAL");
        myTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME(this.Connector, "", "myTIME");
        myLTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME(this.Connector, "", "myLTIME");
        myDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this.Connector, "", "myDATE");
        myLDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE(this.Connector, "", "myLDATE");
        myTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this.Connector, "", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME_OF_DAY(this.Connector, "", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this.Connector, "", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE_AND_TIME(this.Connector, "", "myLDATE_AND_TIME");
        myCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateCHAR(this.Connector, "", "myCHAR");
        myWCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateWCHAR(this.Connector, "", "myWCHAR");
        mySTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "mySTRING");
        myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING");
        myColors = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "myColors");
        GH_PKTu_ix_56_SecondInheritance = new GH.PKTu.ix_56.SecondInheritance(this.Connector, "", "GH_PKTu_ix_56_SecondInheritance");
    }

    public ax_test_projectTwinController(Ix.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        mins = new full_of_primitives(this.Connector, "", "mins");
        maxs = new full_of_primitives(this.Connector, "", "maxs");
        minsmatch = new full_of_primitives_match(this.Connector, "", "minsmatch");
        maxsmatch = new full_of_primitives_match(this.Connector, "", "maxsmatch");
        myBOOL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "myBOOL");
        myBYTE = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this.Connector, "", "myBYTE");
        myWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this.Connector, "", "myWORD");
        myDWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateDWORD(this.Connector, "", "myDWORD");
        myLWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateLWORD(this.Connector, "", "myLWORD");
        mySINT = @Connector.ConnectorAdapter.AdapterFactory.CreateSINT(this.Connector, "", "mySINT");
        myINT = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "myINT");
        myDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateDINT(this.Connector, "", "myDINT");
        myLINT = @Connector.ConnectorAdapter.AdapterFactory.CreateLINT(this.Connector, "", "myLINT");
        myUSINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUSINT(this.Connector, "", "myUSINT");
        myUINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUINT(this.Connector, "", "myUINT");
        myUDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUDINT(this.Connector, "", "myUDINT");
        myULINT = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this.Connector, "", "myULINT");
        myREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this.Connector, "", "myREAL");
        myLREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this.Connector, "", "myLREAL");
        myTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME(this.Connector, "", "myTIME");
        myLTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME(this.Connector, "", "myLTIME");
        myDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this.Connector, "", "myDATE");
        myLDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE(this.Connector, "", "myLDATE");
        myTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this.Connector, "", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME_OF_DAY(this.Connector, "", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this.Connector, "", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE_AND_TIME(this.Connector, "", "myLDATE_AND_TIME");
        myCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateCHAR(this.Connector, "", "myCHAR");
        myWCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateWCHAR(this.Connector, "", "myWCHAR");
        mySTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "mySTRING");
        myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING");
        myColors = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "myColors");
        GH_PKTu_ix_56_SecondInheritance = new GH.PKTu.ix_56.SecondInheritance(this.Connector, "", "GH_PKTu_ix_56_SecondInheritance");
    }
}