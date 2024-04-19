namespace gitlist.core;

public sealed class AsyncLock
{
    private readonly Task<IDisposable> _releaserTask;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private readonly DisposableScope _releaser;

    public AsyncLock()
    {
        _releaser = new DisposableScope(() => _semaphore.Release());
        _releaserTask = Task.FromResult((IDisposable)_releaser);
    }

    /// <summary>
    /// Lock
    /// </summary>
    /// <returns></returns>
    public IDisposable Lock()
    {
        _semaphore.Wait();
        return _releaser;
    }

    /// <summary>
    /// Async Lock
    /// </summary>
    /// <returns></returns>
    public Task<IDisposable> LockAsync()
    {
        var waitTask = _semaphore.WaitAsync();
#pragma warning disable CS8619 // Допустимость значения NULL для ссылочных типов в значении не соответствует целевому типу.
        return waitTask.IsCompleted
            ? _releaserTask
            : waitTask.ContinueWith(
                (_, releaser) => releaser as IDisposable,
                _releaser,
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);
#pragma warning restore CS8619 // Допустимость значения NULL для ссылочных типов в значении не соответствует целевому типу.
    }
}

public sealed class DisposableScope : IDisposable
{
    private readonly Action _closeScopeAction;

    public DisposableScope(Action closeScopeAction)
    {
        _closeScopeAction = closeScopeAction;
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        _closeScopeAction();
    }
}