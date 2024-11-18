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
        return _context.Users.ToList();
    }

    public User? Update(User updatedEntity)
    {
        var actualUser = Find(x => x.Email == updatedEntity.Email);
        actualUser.Name = updatedEntity.Name;
        actualUser.Surname = updatedEntity.Surname;
        actualUser.BirthDate = updatedEntity.BirthDate;
        actualUser.Password = updatedEntity.Password;
        actualUser.Admin = updatedEntity.Admin;
        _context.SaveChanges();
        return actualUser;
    }

    public void Delete(int id)
    {
        var userToBeDeleted = Find(x => x.Id == id);
        _context.Users.RemoveRange(userToBeDeleted);
        _context.SaveChanges();
    }
}