using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace QuickControl.Converters;

public class BorderThicknessToStrokeThicknessConverter : MarkupExtension, IValueConverter
{
    private static BorderThicknessToStrokeThicknessConverter? _converter;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Thickness b) return b.Left;
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => _converter ??= new BorderThicknessToStrokeThicknessConverter();
}