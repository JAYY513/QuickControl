﻿using System;

namespace QuickControl.Expression.Media;

[Flags]
internal enum DrawingPropertyMetadataOptions
{
    AffectsMeasure = 1,
    AffectsRender = 0x10,
    None = 0
}
