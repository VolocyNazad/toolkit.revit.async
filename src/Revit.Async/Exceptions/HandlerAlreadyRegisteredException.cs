namespace Revit.Async.Exceptions;

/// <summary>
/// Thrown when a handler type is registered more than once in the same registry.
/// </summary>
public sealed class HandlerAlreadyRegisteredException : RevitAsyncException
{
    public HandlerAlreadyRegisteredException(Type handlerType)
        : base($"External-event handler '{handlerType.FullName}' is already registered.")
    {
        HandlerType = handlerType;
    }

    public Type HandlerType { get; }
}
