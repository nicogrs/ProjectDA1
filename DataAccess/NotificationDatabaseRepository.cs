using Dominio;
using Interfaces;

namespace DataAccess;

public class NotificationDatabaseRepository : IRepository<Notification>
{
    private SqlContext _context;

    public NotificationDatabaseRepository(SqlContext context)
    {
        _context = context;
    }

    public Notification Add(Notification notification)
    {
        _context.Notifications.Add(notification);
        _context.SaveChanges();
        return notification;
    }

    public Notification? Find(Func<Notification, bool> filter)
    {
        return _context.Notifications.Where(filter).FirstOrDefault();
    }

    public IList<Notification> FindAll()
    {
        return _context.Notifications.ToList();
    }

    public Notification? Update(Notification updatedEntity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        var notification = Find(x => x.Id == id);
        _context.Notifications.RemoveRange(notification);
        _context.SaveChanges();
    }
}