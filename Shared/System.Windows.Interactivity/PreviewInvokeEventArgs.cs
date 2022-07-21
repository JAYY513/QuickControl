using System;

namespace QuickControl.Interactivity;

public class PreviewInvokeEventArgs : EventArgs
{
    public bool Cancelling { get; set; }
}
