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
        return Users.Find(u => u.Email == email);
    }

    public List<User> GetAllUsers()
    {
        return null;
    }

    public void AddUser(User user)
    {
        this.Users.Add(user);
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
