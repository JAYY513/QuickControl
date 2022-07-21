namespace QuickControl.Data;

public static class ValueBoxes
{
    public static readonly object TrueBox = true;
    public static readonly object FalseBox = false;
    internal static readonly object Double1Box = 1.0;
    internal static readonly object Double0Box = .0;
    public static object BooleanBox(bool value)
    {
        return value ? TrueBox : FalseBox;
    }
}