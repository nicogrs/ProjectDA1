using Dominio;

namespace Interfaces;

public interface INotificationService
{
    public Notification AddNotification(Notification notification);
    public List<Notification> GetNotificationsByUserId(int userId);
    public void DeleteNotification(Notification notification);
}