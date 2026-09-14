namespace Revit.Async;

internal static class Guard
{
    public static T ThrowIfNull<T>(T? argument, string parameterName) where T : class
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(argument, parameterName);
#else
        if (argument is null) throw new ArgumentNullException(parameterName);
#endif
        return argument;
    }
}