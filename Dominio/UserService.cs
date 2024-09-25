namespace Dominio;

public class UserService
{
    private readonly IUserDatabase _userDatabase;

    public UserService(IUserDatabase userDatabase)
    {
        _userDatabase = userDatabase;
    }

    public bool UserExists(string email)
    {
       return _userDatabase.GetUserByEmail(email) != null;
    }

    public bool CreateUser(User user)
    {
        if (user.IsUserValid())
        {
          _userDatabase.AddUser(user);
          return true;
        }

        return false;

    }
}