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

    public Notification Add(Notification oneElement)
    {
        throw new NotImplementedException();
    }

    public Notification? Find(Func<Notification, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IList<Notification> FindAll()
    {
        throw new NotImplementedException();
    }

    public Notification? Update(Notification updatedEntity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}