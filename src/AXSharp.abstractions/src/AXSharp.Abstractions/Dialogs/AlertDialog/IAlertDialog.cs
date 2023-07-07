using System;

namespace AXSharp.Abstractions.Dialogs.AlertDialog
{
    public interface IAlertDialog
    {
        public Guid Id { get; set; }
        public eDialogType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTimeOffset TimeToBurn { get; set; }
        public DateTimeOffset Posted { get; set; }
    }
}