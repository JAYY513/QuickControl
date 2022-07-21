using System;
using System.Reflection;
using System.Windows;

namespace QuickControl.Tools;

/// <summary>
///     资源帮助类
/// </summary>
public class ResourceHelper
{
    private static ResourceDictionary _theme;

    /// <summary>
    ///     获取资源
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T GetResource<T>(string key)
    {
        if (Application.Current.TryFindResource(key) is T resource)
        {
            return resource;
        }

        return default;
    }

    internal static T GetResourceInternal<T>(string key)
    {
        if (GetTheme()[key] is T resource)
        {
            return resource;
        }

        return default;
    }
    /// <summary>
    ///     get HandyControl theme
    /// </summary>
    public static ResourceDictionary GetTheme() => _theme ??= GetStandaloneTheme();

    public static ResourceDictionary GetStandaloneTheme()
    {
        return new()
        {
            Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
        };
    }
}
