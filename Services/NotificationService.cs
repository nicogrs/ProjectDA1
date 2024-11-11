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
        var notifications = _notificationDatabase.FindAll();
        return notifications.Where(x => x.ToUserId == userId).ToList();
    }

    public void DeleteNotification(Notification notification)
    {
        _notificationDatabase.Delete(notification.Id);
    }
}