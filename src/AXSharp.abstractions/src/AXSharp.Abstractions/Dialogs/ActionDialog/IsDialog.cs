using AXSharp.Connector;
using System;

namespace AXSharp.Abstractions.Dialogs.ActionDialog
{
    public interface IsDialog : ITwinObject
    {
        string DialogId { get; set; }
        void Initialize(Action dialogAction);

    }
}