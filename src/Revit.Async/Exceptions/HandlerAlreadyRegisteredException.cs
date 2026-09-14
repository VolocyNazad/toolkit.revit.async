namespace Revit.Async.Exceptions;

/// <summary>
/// Thrown when a handler type is registered more than once in the same registry.
/// </summary>
public sealed class HandlerAlreadyRegisteredException : RevitAsyncException
{
    /// <summary>
    /// Initializes a new instance for the handler type that is already registered.
    /// </summary>
    /// <param name="handlerType">The handler type that caused the registration conflict.</param>
    public HandlerAlreadyRegisteredException(Type handlerType)
        : base($"External-event handler '{(handlerType ?? throw new ArgumentNullException(nameof(handlerType))).FullName}' is already registered.")
    {
        HandlerType = handlerType;
    }

    /// <summary>
    /// Gets the handler type that was already registered.
    /// </summary>
    public Type HandlerType { get; }
}
