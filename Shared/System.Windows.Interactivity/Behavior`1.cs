﻿using System.Windows;

namespace QuickControl.Interactivity;

public abstract class Behavior<T> : Behavior where T : DependencyObject
{
    protected Behavior() : base(typeof(T))
    {
    }

    protected new T AssociatedObject => (T) base.AssociatedObject;
}
