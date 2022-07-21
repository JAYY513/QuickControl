using System;
using System.Collections.Generic;
using System.Text;
using QuickControl.Expression.Drawing;
using QuickControl.Interop;
using QuickControl.Shared.Interop;

namespace QuickControl.Dpi
{
    internal static partial class DpiHelper
    {
        public static double DeviceDpiX { get; }

        public static double DeviceDpiY { get; }

        static DpiHelper()
        {
            var dC = User32.GetDC(IntPtr.Zero);
            if (dC != IntPtr.Zero)
            {
                // 沿着屏幕宽度每逻辑英寸的像素数。在具有多个显示器的系统中，这个值对所有显示器都是相同的
                const int logicPixelsX = 88;
                // 沿着屏幕高度每逻辑英寸的像素数
                const int logicPixelsY = 90;
                DeviceDpiX = InteropMethods.GetDeviceCaps(dC, logicPixelsX);
                DeviceDpiY = InteropMethods.GetDeviceCaps(dC, logicPixelsY);
                User32.ReleaseDC(IntPtr.Zero, dC);
            }
            else
            {
                DeviceDpiX = DefaultDpi;
                DeviceDpiY = DefaultDpi;
            }
        }
        public static double RoundLayoutValue(double value, double dpiScale)
        {
            double newValue;

            if (!MathHelper.AreClose(dpiScale, 1.0))
            {
                newValue = Math.Round(value * dpiScale) / dpiScale;
                if (double.IsNaN(newValue) || double.IsInfinity(newValue) || MathHelper.AreClose(newValue, double.MaxValue))
                {
                    newValue = value;
                }
            }
            else
            {
                newValue = Math.Round(value);
            }

            return newValue;
        }
    }
}
