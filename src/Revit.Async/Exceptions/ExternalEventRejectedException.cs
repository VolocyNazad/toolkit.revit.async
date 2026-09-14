namespace Revit.Async.Exceptions;

/// <summary>
/// Thrown when Revit rejects an external-event request.
/// </summary>
public sealed class ExternalEventRejectedException : RevitAsyncException
{
    /// <summary>
    /// Initializes a new instance for an external-event request rejected by Revit.
    /// </summary>
    public ExternalEventRejectedException() : base("Revit did not accept the external-event request.")
    {
    }
}
