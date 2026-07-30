using Autodesk.Revit.UI;
using Revit.Async.Exceptions;
using Revit.Async.ExternalEvents;
using Revit.Async.Interfaces;

namespace Revit.Async.Entities;

internal sealed class FutureExternalEvent : ICloneable, IDisposable
{
    private static readonly object InitializationLock = new();
    private static readonly SemaphoreSlim CreationLock = new(1, 1);
    private static FutureExternalEvent? _externalEventCreator;

    private readonly SemaphoreSlim _executionLock = new(1, 1);
    private readonly Func<Task<ExternalEvent>> _externalEventFactory;
    private ExternalEvent? _externalEvent;
    private bool _disposed;

    private FutureExternalEvent(IExternalEventHandler handler, ExternalEvent externalEvent)
    {
        Handler = handler;
        _externalEvent = externalEvent;
        _externalEventFactory = () => Task.FromResult(externalEvent);
    }

    public FutureExternalEvent(IExternalEventHandler handler)
    {
        Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        _externalEventFactory = CreateAndCacheExternalEventAsync;
    }

    public IExternalEventHandler Handler { get; }

    internal static bool IsInitialized
    {
        get
        {
            lock (InitializationLock)
            {
                return _externalEventCreator is not null;
            }
        }
    }

    public object Clone()
    {
        ThrowIfDisposed();
        var handler = Handler is ICloneable cloneable
            ? cloneable.Clone() as IExternalEventHandler
            : Handler;

        return new FutureExternalEvent(handler ??
            throw new InvalidOperationException($"Handler '{Handler.GetType().FullName}' returned an invalid clone."));
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _externalEvent?.Dispose();
        _executionLock.Dispose();
    }

    internal static void Initialize(UIControlledApplication application)
    {
        if (application is null) throw new ArgumentNullException(nameof(application));
        InitializeCore(() =>
        {
            var handler = new ExternalEventHandlerCreator();
            return new FutureExternalEvent(handler, ExternalEvent.Create(handler));
        });
    }

    internal static void Initialize(UIApplication application)
    {
        if (application is null) throw new ArgumentNullException(nameof(application));
        InitializeCore(() =>
        {
            var handler = new ExternalEventHandlerCreator();
            return new FutureExternalEvent(handler, ExternalEvent.Create(handler));
        });
    }

    internal static void Shutdown()
    {
        lock (InitializationLock)
        {
            _externalEventCreator?.Dispose();
            _externalEventCreator = null;
        }
    }

    internal async Task<TResult> RunAsync<TParameter, TResult>(TParameter parameter)
    {
        ThrowIfDisposed();
        await _executionLock.WaitAsync().ConfigureAwait(false);
        try
        {
            ThrowIfDisposed();
            var handler = (IGenericExternalEventHandler<TParameter, TResult>)Handler;
            var result = handler.Prepare(parameter);
            var externalEvent = await GetExternalEventAsync().ConfigureAwait(false);

            if (externalEvent.Raise() != ExternalEventRequest.Accepted)
            {
                throw new ExternalEventRejectedException();
            }

            return await result.ConfigureAwait(false);
        }
        finally
        {
            _executionLock.Release();
        }
    }

    private static void InitializeCore(Func<FutureExternalEvent> factory)
    {
        lock (InitializationLock)
        {
            _externalEventCreator ??= factory();
        }
    }

    private static async Task<ExternalEvent> CreateExternalEventAsync(IExternalEventHandler handler)
    {
        await CreationLock.WaitAsync().ConfigureAwait(false);
        try
        {
            FutureExternalEvent creator;
            lock (InitializationLock)
            {
                creator = _externalEventCreator ?? throw new RevitTaskNotInitializedException();
            }

            return await creator.RunAsync<IExternalEventHandler, ExternalEvent>(handler).ConfigureAwait(false);
        }
        finally
        {
            CreationLock.Release();
        }
    }

    private async Task<ExternalEvent> CreateAndCacheExternalEventAsync()
    {
        var created = await CreateExternalEventAsync(Handler).ConfigureAwait(false);
        _externalEvent = created;
        return created;
    }

    private Task<ExternalEvent> GetExternalEventAsync()
    {
        return _externalEvent is not null ? Task.FromResult(_externalEvent) : _externalEventFactory();
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FutureExternalEvent));
        }
    }
}
