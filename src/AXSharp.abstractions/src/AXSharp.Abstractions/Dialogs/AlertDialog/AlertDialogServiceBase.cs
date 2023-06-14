using AXSharp.Abstractions.Dialogs.AlertDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace AXSharp.Abstractions.Dialogs.AlertDialog
{
    public class AlertDialogServiceBase : IAlertDialogService
    {
        public event EventHandler AlertDialogChanged;

        public void AddToast(string type, string title, string message, int time)
        {
            throw new NotImplementedException();
        }

        public void AddAlertDialog(string type, string title, string message, int time)
        {
            throw new NotImplementedException();
        }

        public void AddAlertDialog(IAlertDialog toast)
        {
            throw new NotImplementedException();
        }

        public List<IAlertDialog> GetAlertDialogs()
        {
            throw new NotImplementedException();
        }

        public void RemoveAlertDialog(IAlertDialog toast)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllAlertDialogs()
        {
            throw new NotImplementedException();
        }
    }
}
