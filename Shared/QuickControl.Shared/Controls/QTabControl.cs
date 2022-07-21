using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace QuickControl.Controls;

public class QTabControl : TabControl
{
    private const string PointerBorderTemplateName = "PART_PointerBorder";
    private const string PaneContentGridTemplateName = "PART_PointerGrid";
    private static readonly PropertyPath MarginXPath = new("Margin");
    private static readonly Point Frame1Point1 = new(0.9, 0.1);
    private static readonly Point Frame1Point2 = new(1.0, 0.2);
    private static readonly Point Frame2Point1 = new(0.1, 0.9);
    private static readonly Point Frame2Point2 = new(0.2, 1.0);

    private static readonly object DefaultLineLeftRight = 3.0;

    public static readonly DependencyProperty LineLeftRightProperty = DependencyProperty.Register(
        "LineLeftRight", typeof(double), typeof(QTabControl),
        new PropertyMetadata(DefaultLineLeftRight, OnLineLeftRightChanged));

    private double? _lastItemWidth;

    private Point _lastPoint;
    private Grid m_pointerGrid;
    private Border m_pointerBorder;

    static QTabControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(QTabControl),
            new FrameworkPropertyMetadata(typeof(QTabControl)));
    }

    public QTabControl()
    {
        //SizeChanged += QTabControl_SizeChanged;
    }

    public double LineLeftRight
    {
        get => (double)GetValue(LineLeftRightProperty);
        set => SetValue(LineLeftRightProperty, value);
    }

    private TabItem? _lastSelectedItem;
    private void QTabControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        //if (SelectedItem is TabItem tabItem)
        //{
        //    UpdateRepeaterItemsSource(tabItem);
        //}
        //else if (ItemContainerGenerator.ContainerFromItem(SelectedItem) is TabItem tabItem2)
        //{
        //    UpdateRepeaterItemsSource(tabItem2);
        //}
        //else
        //    UpdateRepeaterItemsSource(null);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        m_pointerBorder = GetTemplateChild(PointerBorderTemplateName) as Border;
        m_pointerGrid = GetTemplateChild(PaneContentGridTemplateName) as Grid;
        m_pointerGrid.SizeChanged += QTabControl_SizeChanged;
    }

    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
        base.OnSelectionChanged(e);
        if (e.AddedItems?.Count > 0)
        {
            if (e.AddedItems[0] is TabItem tabItem)
                UpdateRepeaterItemsSource(tabItem);
            else if (ItemContainerGenerator.ContainerFromItem(e.AddedItems[0]) is TabItem tabItem2)
                UpdateRepeaterItemsSource(tabItem2);
            else
                UpdateRepeaterItemsSource(null);
        }
        else
        {
            UpdateRepeaterItemsSource(null);
        }
    }

    private static void OnLineLeftRightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is QTabControl tabControl) tabControl.UpdateRepeaterItemsSource(null);
    }

    private void UpdateRepeaterItemsSource(TabItem? selectedUiElement)
    {
        if (selectedUiElement?.IsLoaded == false)
        {
            //selectedUiElement.Arrange(new Rect(0, 0, m_pointerGrid.ActualWidth, m_pointerGrid.ActualHeight));
            //selectedUiElement.Measure(new Size(m_pointerGrid.ActualWidth, m_pointerGrid.ActualHeight));
            m_pointerBorder.Visibility = Visibility.Collapsed;
            //return;
        }

        IList? items = null;
        if (ItemsSource is IList list)
            items = list;
        else if (Items is IList list2)
            items = list2;
        if (items == null) return;
        if (selectedUiElement == null)
        {
            m_pointerBorder.Visibility = Visibility.Collapsed;
            return;
        }

        m_pointerBorder.Visibility = Visibility.Visible;

        var point = selectedUiElement.TranslatePoint(new Point(), m_pointerGrid);
        if (point.X < 0)
        {
            point = new Point(0, point.Y);
        }
        var currentItemWidth = selectedUiElement.RenderSize.Width;
        var storyboard = new Storyboard();
        var da = new ThicknessAnimationUsingKeyFrames();
        if (point.X > _lastPoint.X)
        {
            da.KeyFrames = new ThicknessKeyFrameCollection
            {
                new SplineThicknessKeyFrame(
                    new Thickness(_lastPoint.X + LineLeftRight, 0,
                        m_pointerGrid.ActualWidth - point.X - currentItemWidth + LineLeftRight, 0),
                    KeyTime.FromPercent(0.333), new KeySpline(Frame1Point1, Frame1Point2)),
                new SplineThicknessKeyFrame(
                    new Thickness(point.X + LineLeftRight, 0,
                        m_pointerGrid.ActualWidth - point.X - currentItemWidth + LineLeftRight, 0),
                    KeyTime.FromPercent(1.0), new KeySpline(Frame2Point1, Frame2Point2))
            };
            da.Duration = TimeSpan.FromMilliseconds(600);
        }
        else
        {
            da.KeyFrames = new ThicknessKeyFrameCollection
            {
                new SplineThicknessKeyFrame(
                    new Thickness(point.X + LineLeftRight, 0,
                        (m_pointerGrid.ActualWidth - _lastPoint.X - _lastItemWidth ?? currentItemWidth) +
                        LineLeftRight, 0), KeyTime.FromPercent(0.333), new KeySpline(Frame1Point1, Frame1Point2)),
                new SplineThicknessKeyFrame(
                    new Thickness(point.X + LineLeftRight, 0,
                        m_pointerGrid.ActualWidth - point.X - currentItemWidth + LineLeftRight, 0),
                    KeyTime.FromPercent(1.0), new KeySpline(Frame2Point1, Frame2Point2))
            };
            da.Duration = TimeSpan.FromMilliseconds(600);
        }

        Storyboard.SetTarget(da, m_pointerBorder);
        Storyboard.SetTargetProperty(da, MarginXPath);
        storyboard.Children.Add(da);
   
        storyboard.Begin();
        _lastPoint = point;
        _lastItemWidth = currentItemWidth;
    }
}