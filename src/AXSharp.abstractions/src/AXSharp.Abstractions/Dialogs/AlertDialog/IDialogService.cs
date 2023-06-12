using System;
using System.Collections.Generic;


namespace AXSharp.Abstractions.Dialogs.AlertDialog
{
    public interface IDialogService
    {
        public event EventHandler? ToasterChanged;
        public void AddToast(string type, string title, string message, int time);
        public void AddToast(IToast toast);
        public List<IToast> GetToasts();
        public void RemoveToast(IToast toast);
        public void RemoveAllToast();
    }
}
