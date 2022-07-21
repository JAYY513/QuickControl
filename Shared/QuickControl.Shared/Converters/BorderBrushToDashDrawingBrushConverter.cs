using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace QuickControl.Converters;

public class BorderBrushToDashDrawingBrushConverter : MarkupExtension, IValueConverter
{
    private static BorderBrushToDashDrawingBrushConverter? _converter;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Brush brush)
        {
            DrawingBrush drawing = new DrawingBrush()
            {
                TileMode = TileMode.Tile,
                Viewport = new Rect(0,0,6,6) ,
                ViewportUnits =  BrushMappingMode.Absolute,
            };
            var drawingGroup = new DrawingGroup();
            var geometryDrawing = new GeometryDrawing();
            var geometryGroup = new GeometryGroup();
            geometryGroup.Children.Add(new RectangleGeometry(new Rect(0,0,50,50)));
            geometryGroup.Children.Add(new RectangleGeometry(new Rect(50, 50, 50, 50)));
            geometryDrawing.Geometry = geometryGroup;
            geometryDrawing.Brush = brush;
            drawingGroup.Children.Add(geometryDrawing);
            drawing.Drawing = drawingGroup;
            return drawing;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => _converter ??= new BorderBrushToDashDrawingBrushConverter();
}