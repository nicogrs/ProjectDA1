namespace Dominio.Data;

public class UserDataBase : IUserDatabase
{
    private List<User> Users;
    public UserDataBase()
    {
        Users = new List<User>();
        
    }
    
    public User GetUserByEmail(string email)
    {
        return (User)null;
    }

    public List<User> GetAllUsers()
    {
        return null;
    }

    public bool AddUser(User user)
    {
        return false;
    }

    public bool UpdateUser(User user)
    {
        return false;
    }

    public bool DeleteUser(string email)
    {
        return false;
    }
}
