namespace Assertly.Core;

internal static class AssertionHelper
{
    public static object EnsureType<T>(T value)
    {
        return value != null ? value : AssertionConstants.Null;
    }
}
