using System;
using System.Collections.Generic;


namespace AXSharp.Abstractions.Dialogs.AlertDialog
{
    public interface IAlertDialogService
    {
        public event EventHandler? AlertDialogChanged;
        public void AddAlertDialog(eDialogType type, string title, string message, int time);
        public void AddAlertDialog(IAlertDialog toast);
        public List<IAlertDialog> GetAlertDialogs();
        public void RemoveAlertDialog(IAlertDialog toast);
        public void RemoveAllAlertDialogs();
    }
}
