﻿using System;
using System.Windows.Media;

namespace QuickControl.Tools
{
    public class ColorHelper
    {
        public static Color ColorWithLuminance(Color color, double luminance)
        {
            var result = color;
            var partWithLuminance = Math.Clamp(result.R + result.R * luminance, 0, 255);
            var roundValue = (int)Math.Round(partWithLuminance);
            result.R = (byte)roundValue;

            partWithLuminance = Math.Clamp(result.G + result.G * luminance, 0, 255);
            roundValue = (int)Math.Round(partWithLuminance);
            result.G = (byte)roundValue;

            partWithLuminance = Math.Clamp(result.B + result.B * luminance, 0, 255);
            roundValue = (int)Math.Round(partWithLuminance);
            result.B = (byte)roundValue;
            return result;
        }
    }
}