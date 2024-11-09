using Dominio;

namespace DataAccess;

using Interfaces;
public class UserDatabaseRepository : IRepository<User>
{
    private SqlContext _context;

    public UserDatabaseRepository(SqlContext context)
    {
        _context = context;
    }
    
    public User Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User? Find(Func<User, bool> filter)
    {
        return _context.Users.Where(filter).FirstOrDefault();
    }

    public IList<User> FindAll()
    {
        throw new NotImplementedException();
    }

    public User? Update(User updatedEntity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}