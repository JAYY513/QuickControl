using System;
using System.Windows;

namespace QuickControl.Interactivity;

public abstract class TriggerAction<T> : TriggerAction where T : DependencyObject
{
    protected TriggerAction() : base(typeof(T))
    {
    }

#pragma warning disable 108,114
    protected T AssociatedObject => (T) base.AssociatedObject;
#pragma warning restore 108,114

    protected sealed override Type AssociatedObjectTypeConstraint =>
        base.AssociatedObjectTypeConstraint;
}
