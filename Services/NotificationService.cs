using System.Collections;
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

    public Notification AddNotification(Notification notification)
    {
        return _notificationDatabase.Add(notification);
    }

    public List<Notification> GetNotificationsByUserId(int userId)
    {
        throw new NotImplementedException();
    }
}