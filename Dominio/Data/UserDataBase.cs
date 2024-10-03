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
       if (string.IsNullOrEmpty(user.Email))
       {
           throw new NullReferenceException("Usuario no encontrado");
       }

       return user;
    }
    
    public void AddUser(User user)
    {
        this.Users.Add(user);
    }

    public void UpdateUser(User user)
    {
       var actualUser = GetUserByEmail(user.Email);
       actualUser.Name = user.Name;
       actualUser.Surname = user.Surname;
       actualUser.BirthDate = user.BirthDate;
       actualUser.Password = user.Password;
    }

    public bool DeleteUser(string email)
    {
        var result = Users.RemoveAll(user => user.Email == email);
         return (result != 0);
    }
}
