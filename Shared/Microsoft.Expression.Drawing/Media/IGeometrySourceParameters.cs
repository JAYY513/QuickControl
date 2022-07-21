﻿using System.Windows.Media;

namespace QuickControl.Expression.Media;

public interface IGeometrySourceParameters
{
    Stretch Stretch { get; }

    Brush Stroke { get; }

    double StrokeThickness { get; }
}
