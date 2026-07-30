namespace Revit.Async.Exceptions;

/// <summary>
/// Base exception for errors reported by Revit.Async.
/// </summary>
public class RevitAsyncException : Exception
{
    public RevitAsyncException(string message) : base(message)
    {
    }
}
