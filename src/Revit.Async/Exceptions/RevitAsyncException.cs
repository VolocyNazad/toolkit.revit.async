namespace Revit.Async.Exceptions;

/// <summary>
/// Base exception for errors reported by Revit.Async.
/// </summary>
public class RevitAsyncException : Exception
{
    /// <summary>
    /// Initializes a new instance with the specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public RevitAsyncException(string message) : base(message)
    {
    }
}
