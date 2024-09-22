namespace Dominio.Data;

public class UserDataBase
{
    public List<User> Users { get; set; }

    public UserDataBase()
    {
        Users = new List<User>();
    }
}