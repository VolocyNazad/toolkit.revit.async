using Revit.Async.Extensions;
using Revit.Async.Interfaces;
using Xunit;

namespace Revit.Async.Tests;

public sealed class ExternalEventResultHandlerExtensionsTests
{
    [Fact]
    public async Task Wait_sets_result()
    {
        var handler = new TestResultHandler<int>();

        handler.Wait(() => 42);

        Assert.Equal(42, await handler.Result);
    }

    [Fact]
    public async Task Wait_propagates_exception()
    {
        var handler = new TestResultHandler<int>();
        var expected = new InvalidOperationException("Failure");

        handler.Wait<int>(() => throw expected);

        var actual = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Result);
        Assert.Same(expected, actual);
    }

    [Fact]
    public async Task Await_sets_result()
    {
        var handler = new TestResultHandler<int>();

        await handler.Await(Task.FromResult(42));

        Assert.Equal(42, await handler.Result);
    }

    [Fact]
    public async Task Await_propagates_exception()
    {
        var handler = new TestResultHandler<int>();
        var expected = new InvalidOperationException("Failure");

        await handler.Await(Task.FromException<int>(expected));

        var actual = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Result);
        Assert.Same(expected, actual);
    }

    [Fact]
    public async Task Await_propagates_cancellation()
    {
        var handler = new TestResultHandler<int>();

        await handler.Await(Task.FromCanceled<int>(new CancellationToken(true)));

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => handler.Result);
    }

    private sealed class TestResultHandler<TResult> : IExternalEventResultHandler<TResult>
    {
        private readonly TaskCompletionSource<TResult> _completionSource =
            new(TaskCreationOptions.RunContinuationsAsynchronously);

        public Task<TResult> Result => _completionSource.Task;

        public void Cancel()
        {
            _completionSource.TrySetCanceled();
        }

        public void SetResult(TResult result)
        {
            _completionSource.TrySetResult(result);
        }

        public void ThrowException(Exception exception)
        {
            _completionSource.TrySetException(exception);
        }
    }
}
