
using Revit.Async.Interfaces;


namespace Revit.Async.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="IExternalEventResultHandler{TResult}"/>
    /// </summary>
    public static class ExternalEventResultHandlerExtensions
    {
        /// <summary>
        ///     Await a <see cref="Task{TSource}"/>
        /// </summary>
        /// <typeparam name="TSource">The type of the task result to wait</typeparam>
        /// <typeparam name="TResult">The type of the result to generate</typeparam>
        /// <param name="resultHandler">The instance of handler to handle the result</param>
        /// <param name="source">The task to wait</param>
        /// <param name="onComplete">Callback to invoke when the task is completed</param>
        /// <returns>The handler itself</returns>
        public static IExternalEventResultHandler<TResult> Await<TSource, TResult>(
            this IExternalEventResultHandler<TResult>             resultHandler,
            Task<TSource>                                         source,
            Action<TSource, IExternalEventResultHandler<TResult>> onComplete)
        {
            Guard.ThrowIfNull(resultHandler, nameof(resultHandler));
            Guard.ThrowIfNull(source, nameof(source));
            Guard.ThrowIfNull(onComplete, nameof(onComplete));

            source.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // A faulted task always carries the aggregate exception, so no placeholder is needed.
                    resultHandler.ThrowException(task.Exception!);
                }
                else if (task.IsCanceled)
                {
                    resultHandler.Cancel();
                }
                else
                {
                    onComplete(task.Result, resultHandler);
                }
            }, TaskScheduler.Current);
            return resultHandler;
        }

        /// <summary>
        ///     Await a <see cref="Task{TSource}"/>
        /// </summary>
        /// <typeparam name="TSource">The type of the task result to wait</typeparam>
        /// <typeparam name="TResult">The type of the result to generate</typeparam>
        /// <param name="resultHandler">The instance of handler to handle the result</param>
        /// <param name="source">The task to wait</param>
        /// <param name="onComplete">Callback to invoke when the task is completed</param>
        /// <returns></returns>
        public static IExternalEventResultHandler<TResult> Await<TSource, TResult>(
            this IExternalEventResultHandler<TResult> resultHandler,
            Task<TSource>                             source,
            Action<TSource>                           onComplete)
        {
            Guard.ThrowIfNull(resultHandler, nameof(resultHandler));
            Guard.ThrowIfNull(source, nameof(source));
            Guard.ThrowIfNull(onComplete, nameof(onComplete));

            source.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // A faulted task always carries the aggregate exception, so no placeholder is needed.
                    resultHandler.ThrowException(task.Exception!);
                }
                else if (task.IsCanceled)
                {
                    resultHandler.Cancel();
                }
                else
                {
                    onComplete(task.Result);
                }
            }, TaskScheduler.Current);
            return resultHandler;
        }

        /// <summary>
        ///     Await a <see cref="Task{TResult}"/>
        /// </summary>
        /// <typeparam name="TResult">The type of the result</typeparam>
        /// <param name="resultHandler"></param>
        /// <param name="task">The task to wait</param>
        public static async Task Await<TResult>(
            this IExternalEventResultHandler<TResult> resultHandler,
            Task<TResult>                             task)
        {
            Guard.ThrowIfNull(resultHandler, nameof(resultHandler));
            // Guard returns the argument, and the argument is itself a Task, so the call must be
            // discarded explicitly to keep it out of the awaitable-expression warning.
            _ = Guard.ThrowIfNull(task, nameof(task));

            try
            {
                var result = await task.ConfigureAwait(false);
                resultHandler.SetResult(result);
            }
            catch (OperationCanceledException)
            {
                resultHandler.Cancel();
            }
            catch (Exception e)
            {
                resultHandler.ThrowException(e);
            }
        }

        /// <summary>
        ///     Wait for an sync delegate to complete
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="resultHandler"></param>
        /// <param name="function"></param>
        public static void Wait<TResult>(this IExternalEventResultHandler<TResult> resultHandler, Func<TResult> function)
        {
            Guard.ThrowIfNull(resultHandler, nameof(resultHandler));
            Guard.ThrowIfNull(function, nameof(function));

            try
            {
                var result = function();
                resultHandler.SetResult(result);
            }
            catch (Exception e)
            {
                resultHandler.ThrowException(e);
            }
        }
    }
}
