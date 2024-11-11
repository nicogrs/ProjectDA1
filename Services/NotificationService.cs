using Dominio;
using Interfaces;

namespace Services;

public class NotificationService
{
    private readonly IRepository<Notification> _notificationDatabase;

    public NotificationService(IRepository<Notification> notificationDatabase)
    {
        _notificationDatabase = notificationDatabase;
    }

    public void AddNotification(Notification notification)
    {
        throw new NotImplementedException();
    }
}