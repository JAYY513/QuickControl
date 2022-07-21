using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using QuickControl.Data;

namespace QuickControl.Controls;

[DefaultProperty("Content")]
[ContentProperty("Content")]
[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
public class SimpleButton : Control
{
    [Category("Behavior")]
    public event RoutedEventHandler Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    #region static

    public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(SimpleButton));

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command),
        typeof(ICommand), typeof(SimpleButton), new FrameworkPropertyMetadata(null));

    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(SimpleButton),
            new FrameworkPropertyMetadata(null));

    public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register(nameof(CommandTarget),
        typeof(IInputElement), typeof(SimpleButton), new FrameworkPropertyMetadata(null));

    internal static readonly DependencyPropertyKey IsPressedPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(IsPressed), typeof(bool), typeof(SimpleButton),
            new FrameworkPropertyMetadata(ValueBoxes.FalseBox));

    public static readonly DependencyProperty IsPressedProperty = IsPressedPropertyKey.DependencyProperty;

    public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content),
        typeof(object), typeof(SimpleButton), new FrameworkPropertyMetadata(null, OnContentChanged));

    static SimpleButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleButton),
            new FrameworkPropertyMetadata(typeof(SimpleButton)));
    }

    #endregion

    #region prop

    [Bindable(true)]
    [Category("Action")]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    [Bindable(true)]
    [Category("Action")]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    [Bindable(true)]
    [Category("Action")]
    public IInputElement CommandTarget
    {
        get => (IInputElement)GetValue(CommandTargetProperty);
        set => SetValue(CommandTargetProperty, value);
    }

    [Browsable(false)]
    [Category("Appearance")]
    [ReadOnly(true)]
    public bool IsPressed
    {
        get => (bool)GetValue(IsPressedProperty);
        protected set => SetValue(IsPressedPropertyKey, ValueBoxes.BooleanBox(value));
    }

    [Bindable(true)]
    [Category("Content")]
    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    #endregion

    #region event

    protected virtual void OnClick()
    {
        RaiseEvent(new RoutedEventArgs(ClickEvent, this));
        Command?.Execute(CommandParameter);
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        e.Handled = true;
        Focus();
        if (e.ButtonState == MouseButtonState.Pressed)
        {
            CaptureMouse();
            if (IsMouseCaptured)
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    if (!IsPressed)
                        SetIsPressed(true);
                }
                else
                {
                    ReleaseMouseCapture();
                }
            }
        }

        base.OnMouseLeftButtonDown(e);
    }

    protected override void OnLostMouseCapture(MouseEventArgs e)
    {
        base.OnLostMouseCapture(e);
        if (e.OriginalSource != this )
            return;
        if (this.IsKeyboardFocused)
            Keyboard.Focus(null);
        this.SetIsPressed(false);
    }
    private void SetIsPressed(bool pressed)
    {
        if (pressed)
            SetValue(IsPressedPropertyKey, ValueBoxes.BooleanBox(pressed));
        else
            ClearValue(IsPressedPropertyKey);
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        e.Handled = true;
        var flag = IsPressed;
        if (IsMouseCaptured)
            ReleaseMouseCapture();
        if (flag)
            OnClick();
        base.OnMouseLeftButtonUp(e);
    }

    private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var contentControl = (SimpleButton)d;
        contentControl.OnContentChanged(e.OldValue, e.NewValue);
    }

    protected virtual void OnContentChanged(object oldContent, object newContent)
    {
        //this.RemoveLogicalChild(oldContent);
        //if (this.ContentIsNotLogical)
        //    return;
        //if (newContent is DependencyObject current)
        //{
        //    DependencyObject parent = LogicalTreeHelper.GetParent(current);
        //    if (parent != null)
        //    {
        //        if (this.TemplatedParent != null && FrameworkObject.IsEffectiveAncestor(parent, (DependencyObject)this))
        //            return;
        //        LogicalTreeHelper.RemoveLogicalChild(parent, newContent);
        //    }
        //}
        //this.AddLogicalChild(newContent);
    }

    #endregion
}