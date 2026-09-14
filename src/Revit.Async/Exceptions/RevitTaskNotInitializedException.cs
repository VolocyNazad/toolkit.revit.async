namespace Revit.Async.Exceptions;

/// <summary>
/// Thrown when work is scheduled before RevitTask is initialized.
/// </summary>
public sealed class RevitTaskNotInitializedException : RevitAsyncException
{
    /// <summary>
    /// Initializes a new instance indicating that <see cref="RevitTask"/> has not been initialized.
    /// </summary>
    public RevitTaskNotInitializedException()
        : base("RevitTask is not initialized. Call RevitTask.Initialize from a valid Revit API context first.")
    {
    }
}
