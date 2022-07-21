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
    private static readonly Point c_frame1point1 = new(0.9, 0.1);
    private static readonly Point c_frame1point2 = new(1.0, 0.2);
    private static readonly Point c_frame2point1 = new(0.1, 0.9);
    private static readonly Point c_frame2point2 = new(0.2, 1.0);

    private static readonly object DefaultLineLeftRight = 3.0;

    public static readonly DependencyProperty LineLeftRightProperty = DependencyProperty.Register(
        "LineLeftRight", typeof(double), typeof(QTabControl),
        new PropertyMetadata(DefaultLineLeftRight, OnLineLeftRightChanged));

    private double? _lastItemWidth;

    private Point _lastPoint;
    private Grid m_paneContentGrid;
    private Border m_pointerRectangle;

    static QTabControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(QTabControl),
            new FrameworkPropertyMetadata(typeof(QTabControl)));
    }

    public QTabControl()
    {
        SizeChanged += QTabControl_SizeChanged;
    }

    public double LineLeftRight
    {
        get => (double)GetValue(LineLeftRightProperty);
        set => SetValue(LineLeftRightProperty, value);
    }

    private void QTabControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (SelectedItem is TabItem tabItem)
            UpdateRepeaterItemsSource(tabItem);
        else if (ItemContainerGenerator.ContainerFromItem(SelectedItem) is TabItem tabItem2)
            UpdateRepeaterItemsSource(tabItem2);
        else
            UpdateRepeaterItemsSource(null);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        m_pointerRectangle = GetTemplateChild(PointerBorderTemplateName) as Border;
        m_paneContentGrid = GetTemplateChild(PaneContentGridTemplateName) as Grid;
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
            selectedUiElement.Arrange(new Rect(0, 0, m_paneContentGrid.ActualWidth, m_paneContentGrid.ActualHeight));
            selectedUiElement.Measure(new Size(m_paneContentGrid.ActualWidth, m_paneContentGrid.ActualHeight));
            m_pointerRectangle.Visibility = Visibility.Collapsed;
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
            m_pointerRectangle.Visibility = Visibility.Collapsed;
            return;
        }

        m_pointerRectangle.Visibility = Visibility.Visible;

        var point = selectedUiElement.TranslatePoint(new Point(), m_paneContentGrid);
        var currentItemWidth = selectedUiElement.RenderSize.Width;
        var storyboard = new Storyboard();
        var da = new ThicknessAnimationUsingKeyFrames();
        if (point.X > _lastPoint.X)
        {
            da.KeyFrames = new ThicknessKeyFrameCollection
            {
                new SplineThicknessKeyFrame(
                    new Thickness(_lastPoint.X + LineLeftRight, 0,
                        m_paneContentGrid.ActualWidth - point.X - currentItemWidth + LineLeftRight, 0),
                    KeyTime.FromPercent(0.333), new KeySpline(c_frame1point1, c_frame1point2)),
                new SplineThicknessKeyFrame(
                    new Thickness(point.X + LineLeftRight, 0,
                        m_paneContentGrid.ActualWidth - point.X - currentItemWidth + LineLeftRight, 0),
                    KeyTime.FromPercent(1.0), new KeySpline(c_frame2point1, c_frame2point2))
            };
            da.Duration = TimeSpan.FromMilliseconds(600);
        }
        else
        {
            da.KeyFrames = new ThicknessKeyFrameCollection
            {
                new SplineThicknessKeyFrame(
                    new Thickness(point.X + LineLeftRight, 0,
                        (m_paneContentGrid.ActualWidth - _lastPoint.X - _lastItemWidth ?? currentItemWidth) +
                        LineLeftRight, 0), KeyTime.FromPercent(0.333), new KeySpline(c_frame1point1, c_frame1point2)),
                new SplineThicknessKeyFrame(
                    new Thickness(point.X + LineLeftRight, 0,
                        m_paneContentGrid.ActualWidth - point.X - currentItemWidth + LineLeftRight, 0),
                    KeyTime.FromPercent(1.0), new KeySpline(c_frame2point1, c_frame2point2))
            };
            da.Duration = TimeSpan.FromMilliseconds(600);
        }

        Storyboard.SetTarget(da, m_pointerRectangle);
        Storyboard.SetTargetProperty(da, MarginXPath);
        storyboard.Children.Add(da);
        storyboard.Begin();
        storyboard.Completed += (s, e) => { };
        _lastPoint = point;
        _lastItemWidth = currentItemWidth;
    }
}