namespace Revit.Async.Exceptions;

/// <summary>
/// Thrown when a requested external-event handler is not registered.
/// </summary>
public sealed class HandlerNotRegisteredException : RevitAsyncException
{
    /// <summary>
    /// Initializes a new instance for the handler type that is not registered.
    /// </summary>
    /// <param name="handlerType">The requested handler type.</param>
    public HandlerNotRegisteredException(Type handlerType)
        : base($"External-event handler '{(handlerType ?? throw new ArgumentNullException(nameof(handlerType))).FullName}' is not registered.")
    {
        HandlerType = handlerType;
    }

    /// <summary>
    /// Gets the handler type that was not registered.
    /// </summary>
    public Type HandlerType { get; }
}
