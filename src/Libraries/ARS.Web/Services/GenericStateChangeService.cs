namespace ARS.Web.Services;

//TODO Make this better
public class GenericStateChangeService
{
    public event Action? OnStateChanged;

    public void NotifyStateChanged()
    {
        OnStateChanged?.Invoke();
    }
}