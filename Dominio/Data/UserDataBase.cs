namespace Dominio.Data;

public class UserDataBase
{
    public List<User> Users { get; set; }

    public UserDataBase()
    {
        Users = new List<User>();
    }

    public void AddUser(User user)
    {
        
    }

    public bool userExists(string email)
    {
        return false;
    }

}