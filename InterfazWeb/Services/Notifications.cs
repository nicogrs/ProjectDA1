namespace InterfazWeb;

public class Notifications
{
    public event Action OnChangeNotifications;

    public void NotifyNewNotifications()
    {
        OnChangeNotifications?.Invoke();
    }
    
}