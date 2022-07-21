using System.Windows;

namespace QuickControl.Interactivity;

public interface IAttachedObject
{
    void Attach(DependencyObject dependencyObject);
    void Detach();

    DependencyObject AssociatedObject { get; }
}
