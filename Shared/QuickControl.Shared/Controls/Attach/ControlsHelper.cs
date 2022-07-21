using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using QuickControl.Appearance;
using QuickControl.Data;
using QuickControl.PackIcon;
using QuickControl.Tools.Converter;

namespace QuickControl.Controls;

public class ControlsHelper
{
    public static readonly DependencyProperty ThemeTypeProperty = DependencyProperty.RegisterAttached(
        "ThemeType", typeof(ThemeType), typeof(ControlsHelper), new PropertyMetadata(ThemeType.Dark));

    public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
        "Icon", typeof(PackIconKind), typeof(ControlsHelper), new PropertyMetadata(default));

    public static readonly DependencyProperty BackgroundNormalProperty =
        DependencyProperty.RegisterAttached("BackgroundNormal", typeof(Brush), typeof(ControlsHelper),
            new PropertyMetadata(Brushes.Transparent));

    public static readonly DependencyProperty BackgroundHoverProperty =
        DependencyProperty.RegisterAttached("BackgroundHover", typeof(Brush), typeof(ControlsHelper),
            new PropertyMetadata(Brushes.Transparent));

    public static readonly DependencyProperty BackgroundPressedProperty =
        DependencyProperty.RegisterAttached("BackgroundPressed", typeof(Brush), typeof(ControlsHelper),
            new PropertyMetadata(Brushes.Transparent));

    public static readonly DependencyProperty BackgroundCheckedProperty =
        DependencyProperty.RegisterAttached("BackgroundChecked", typeof(Brush), typeof(ControlsHelper),
            new PropertyMetadata(Brushes.Transparent));

    public static readonly DependencyProperty BorderBrushNormalProperty =
        DependencyProperty.RegisterAttached("BorderBrushNormal", typeof(Brush), typeof(ControlsHelper),
            new PropertyMetadata(Brushes.Black));

    public static readonly DependencyProperty BorderBrushHoverProperty =
        DependencyProperty.RegisterAttached("BorderBrushHover", typeof(Brush), typeof(ControlsHelper),
            new PropertyMetadata(Brushes.Black));

    public static readonly DependencyProperty BorderBrushPressedProperty =
        DependencyProperty.RegisterAttached("BorderBrushPressed", typeof(Brush), typeof(ControlsHelper),
            new PropertyMetadata(Brushes.Black));

    public static readonly DependencyProperty BorderBrushCheckedProperty =
        DependencyProperty.RegisterAttached("BorderBrushChecked", typeof(Brush), typeof(ControlsHelper),
            new PropertyMetadata(Brushes.Black));

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
        "CornerRadius", typeof(CornerRadius), typeof(ControlsHelper),
        new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits));

    public static readonly DependencyProperty CircularProperty = DependencyProperty.RegisterAttached(
        "Circular", typeof(bool), typeof(ControlsHelper), new PropertyMetadata(ValueBoxes.FalseBox, OnCircularChanged));

    public static readonly DependencyProperty GeometryProperty = DependencyProperty.RegisterAttached(
        "Geometry", typeof(Geometry), typeof(ControlsHelper), new PropertyMetadata(default(Geometry)));

    public static readonly DependencyProperty WidthProperty = DependencyProperty.RegisterAttached(
        "Width", typeof(double), typeof(ControlsHelper), new PropertyMetadata(double.NaN));

    public static readonly DependencyProperty HeightProperty = DependencyProperty.RegisterAttached(
        "Height", typeof(double), typeof(ControlsHelper), new PropertyMetadata(double.NaN));

    public static void SetThemeType(DependencyObject element, ThemeType value)
    {
        element.SetValue(ThemeTypeProperty, value);
    }

    public static ThemeType GetThemeType(DependencyObject element)
    {
        return (ThemeType)element.GetValue(ThemeTypeProperty);
    }

    /// <summary>
    ///     设置控件的Icon图标
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value"></param>
    public static void SetIcon(DependencyObject element, PackIconKind value)
    {
        element.SetValue(IconProperty, value);
    }


    public static PackIconKind GetIcon(DependencyObject element)
    {
        return (PackIconKind)element.GetValue(IconProperty);
    }

    public static void SetBackgroundNormal(DependencyObject element, Brush value)
    {
        element.SetValue(BackgroundNormalProperty, value);
    }

    public static Brush GetBackgroundNormal(DependencyObject element)
    {
        return (Brush)element.GetValue(BackgroundNormalProperty);
    }

    public static void SetBackgroundHover(DependencyObject element, Brush value)
    {
        element.SetValue(BackgroundHoverProperty, value);
    }

    public static Brush GetBackgroundHover(DependencyObject element)
    {
        return (Brush)element.GetValue(BackgroundHoverProperty);
    }

    public static void SetBackgroundPressed(DependencyObject element, Brush value)
    {
        element.SetValue(BackgroundPressedProperty, value);
    }

    public static Brush GetBackgroundPressed(DependencyObject element)
    {
        return (Brush)element.GetValue(BackgroundPressedProperty);
    }

    public static void SetBackgroundChecked(DependencyObject element, Brush value)
    {
        element.SetValue(BackgroundCheckedProperty, value);
    }

    public static Brush GetBackgroundChecked(DependencyObject element)
    {
        return (Brush)element.GetValue(BackgroundCheckedProperty);
    }

    public static void SetBorderBrushNormal(DependencyObject element, Brush value)
    {
        element.SetValue(BorderBrushNormalProperty, value);
    }

    public static Brush GetBorderBrushNormal(DependencyObject element)
    {
        return (Brush)element.GetValue(BorderBrushNormalProperty);
    }

    public static void SetBorderBrushHover(DependencyObject element, Brush value)
    {
        element.SetValue(BorderBrushHoverProperty, value);
    }

    public static Brush GetBorderBrushHover(DependencyObject element)
    {
        return (Brush)element.GetValue(BorderBrushHoverProperty);
    }

    public static void SetBorderBrushPressed(DependencyObject element, Brush value)
    {
        element.SetValue(BorderBrushPressedProperty, value);
    }

    public static Brush GetBorderBrushPressed(DependencyObject element)
    {
        return (Brush)element.GetValue(BorderBrushPressedProperty);
    }

    public static void SetBorderBrushChecked(DependencyObject element, Brush value)
    {
        element.SetValue(BorderBrushCheckedProperty, value);
    }

    public static Brush GetBorderBrushChecked(DependencyObject element)
    {
        return (Brush)element.GetValue(BorderBrushCheckedProperty);
    }

    public static void SetCornerRadius(DependencyObject element, CornerRadius value)
    {
        element.SetValue(CornerRadiusProperty, value);
    }

    public static CornerRadius GetCornerRadius(DependencyObject element)
    {
        return (CornerRadius)element.GetValue(CornerRadiusProperty);
    }

    private static void OnCircularChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Border border)
        {
            if ((bool)e.NewValue)
            {
                var binding = new MultiBinding
                {
                    Converter = new BorderCircularConverter()
                };
                binding.Bindings.Add(new Binding(FrameworkElement.ActualWidthProperty.Name) { Source = border });
                binding.Bindings.Add(new Binding(FrameworkElement.ActualHeightProperty.Name) { Source = border });
                border.SetBinding(Border.CornerRadiusProperty, binding);
            }
            else
            {
                BindingOperations.ClearBinding(border, FrameworkElement.ActualWidthProperty);
                BindingOperations.ClearBinding(border, FrameworkElement.ActualHeightProperty);
                BindingOperations.ClearBinding(border, Border.CornerRadiusProperty);
            }
        }
        else if (d is Button button)
        {
            if ((bool)e.NewValue)
            {
                var binding = new MultiBinding
                {
                    Converter = new BorderCircularConverter()
                };
                binding.Bindings.Add(new Binding(FrameworkElement.ActualWidthProperty.Name) { Source = button });
                binding.Bindings.Add(new Binding(FrameworkElement.ActualHeightProperty.Name) { Source = button });
                button.SetBinding(CornerRadiusProperty, binding);
            }
            else
            {
                BindingOperations.ClearBinding(button, FrameworkElement.ActualWidthProperty);
                BindingOperations.ClearBinding(button, FrameworkElement.ActualHeightProperty);
                BindingOperations.ClearBinding(button, CornerRadiusProperty);
            }
        }
        else if (d is SimpleButton simpleButton)
        {
            if ((bool)e.NewValue)
            {
                var binding = new MultiBinding
                {
                    Converter = new BorderCircularConverter()
                };
                binding.Bindings.Add(new Binding(FrameworkElement.ActualWidthProperty.Name) { Source = simpleButton });
                binding.Bindings.Add(new Binding(FrameworkElement.ActualHeightProperty.Name) { Source = simpleButton });
                simpleButton.SetBinding(CornerRadiusProperty, binding);
            }
            else
            {
                BindingOperations.ClearBinding(simpleButton, FrameworkElement.ActualWidthProperty);
                BindingOperations.ClearBinding(simpleButton, FrameworkElement.ActualHeightProperty);
                BindingOperations.ClearBinding(simpleButton, CornerRadiusProperty);
            }
        }
    }

    public static void SetCircular(DependencyObject element, bool value)
    {
        element.SetValue(CircularProperty, ValueBoxes.BooleanBox(value));
    }

    public static bool GetCircular(DependencyObject element)
    {
        return (bool)element.GetValue(CircularProperty);
    }

    public static void SetGeometry(DependencyObject element, Geometry value)
    {
        element.SetValue(GeometryProperty, value);
    }

    public static Geometry GetGeometry(DependencyObject element)
    {
        return (Geometry)element.GetValue(GeometryProperty);
    }

    public static void SetWidth(DependencyObject element, double value)
    {
        element.SetValue(WidthProperty, value);
    }

    public static double GetWidth(DependencyObject element)
    {
        return (double)element.GetValue(WidthProperty);
    }

    public static void SetHeight(DependencyObject element, double value)
    {
        element.SetValue(HeightProperty, value);
    }

    public static double GetHeight(DependencyObject element)
    {
        return (double)element.GetValue(HeightProperty);
    }
}