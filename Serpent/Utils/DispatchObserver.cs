namespace Serpent;

public class DispatchObserver<TValue> : IObserver<TValue>
{
    private Action<TValue>? _onNext;
    private Action<Exception>? _onError;
    private Action? _onCompleted;

    public DispatchObserver<TValue> OnNext(Action<TValue> onNext)
    {
        _onNext = onNext;
        return this;
    }

    public DispatchObserver<TValue> OnError(Action<Exception> onError)
    {
        _onError = onError;
        return this;
    }

    public DispatchObserver<TValue> OnCompleted(Action onCompleted)
    {
        _onCompleted = onCompleted;
        return this;
    }

    void IObserver<TValue>.OnNext(TValue value) => _onNext?.Invoke(value);

    void IObserver<TValue>.OnError(Exception error) => _onError?.Invoke(error);

    void IObserver<TValue>.OnCompleted() => _onCompleted?.Invoke();
}