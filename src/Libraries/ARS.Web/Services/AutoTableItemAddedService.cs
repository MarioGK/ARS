

namespace ARS.Web.Services;

public class AutoTableItemAddedService
{
    private readonly Dictionary<Type, object> _messageBus = new();

    public IObservable<T> Listen<T>()
    {
        var subject = SetupSubject<T>();
        return subject.AsObservable();
    }

    public void Publish<T>(T data)
    {
        var subject = SetupSubject<T>();
        subject.OnNext(data);
    }

    private Subject<T> SetupSubject<T>()
    {
        _messageBus.TryGetValue(typeof(T), out var obj);

        if (obj is Subject<T> subject and not null)
        {
            return subject;
        }

        var sub = new Subject<T>();
        _messageBus.Add(typeof(T), sub);
        return sub;
    }
}