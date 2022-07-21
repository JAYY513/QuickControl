using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using QuickControl.Interop;

namespace QuickControl.Shared.Interop
{
    internal class InteropMethods
    {
        [DllImport(Libraries.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
    }
}
