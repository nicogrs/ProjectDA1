namespace Dominio.Data;

public class UserDataBase : IUserDatabase
{
    private List<User> Users;
    public UserDataBase()
    {
        Users = new List<User>();
        Users.Add(new User
        {
            Name = "Super",
            Surname = "Admin",
            Email = "admin@taskpanel.com",
            BirthDate = new DateTime(2000, 8, 30),
            Password = "Admin123$",
            Admin = true,
            Teams = new List<Team>()
        });
    }
    
    public User GetUserByEmail(string email)
    {
       var user = Users.Find(u => u.Email == email);
       return new User
       {
           Name = user.Name,
           Surname = user.Surname,
           BirthDate = user.BirthDate,
           Admin = user.Admin,
           Email = user.Email,
           Teams = user.Teams
       };
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
        var result = Users.RemoveAll(user => user.Email == email);
         return (result != 0);
    }
}
