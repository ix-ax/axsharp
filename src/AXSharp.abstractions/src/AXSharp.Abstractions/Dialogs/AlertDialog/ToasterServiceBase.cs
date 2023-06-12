using AXSharp.Abstractions.Dialogs.AlertDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace AXSharp.Abstractions.Dialogs.AlertDialog
{
    public class ToasterServiceBase : IDialogService
    {
        public event EventHandler? ToasterChanged;

        public void AddToast(string type, string title, string message, int time)
        {
            throw new NotImplementedException();
        }

        public void AddToast(IToast toast)
        {
            throw new NotImplementedException();
        }

        public List<IToast> GetToasts()
        {
            throw new NotImplementedException();
        }

        public void RemoveToast(IToast toast)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllToast()
        {
            throw new NotImplementedException();
        }
    }
}
