namespace Revit.Async.Exceptions;

/// <summary>
/// Thrown when work is scheduled before RevitTask is initialized.
/// </summary>
public sealed class RevitTaskNotInitializedException : RevitAsyncException
{
    public RevitTaskNotInitializedException()
        : base("RevitTask is not initialized. Call RevitTask.Initialize from a valid Revit API context first.")
    {
    }
}
