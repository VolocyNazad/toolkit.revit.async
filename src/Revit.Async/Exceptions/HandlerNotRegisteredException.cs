namespace Revit.Async.Exceptions;

/// <summary>
/// Thrown when a requested external-event handler is not registered.
/// </summary>
public sealed class HandlerNotRegisteredException : RevitAsyncException
{
    public HandlerNotRegisteredException(Type handlerType)
        : base($"External-event handler '{handlerType.FullName}' is not registered.")
    {
        HandlerType = handlerType;
    }

    public Type HandlerType { get; }
}
