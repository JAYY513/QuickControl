using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace QuickControl.Converters;

public class CornerRadiusToRadiusConverter : MarkupExtension, IValueConverter
{
    private static CornerRadiusToRadiusConverter? _converter;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CornerRadius cornerRadius) return cornerRadius.TopLeft;
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return _converter ??= new CornerRadiusToRadiusConverter();
    }
}