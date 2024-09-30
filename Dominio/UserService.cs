using System.Text;

namespace Dominio;

public class UserService
{
    private readonly IUserDatabase _userDatabase;
    private readonly PasswordService _passwordService;

    public UserService(IUserDatabase userDatabase)
    {
        _passwordService = new PasswordService();
        _userDatabase = userDatabase;
    }

    public bool UserExists(string email)
    {
       return _userDatabase.GetUserByEmail(email) != null;
    }

    public bool CreateUser(User user)
    {
        if (UserExists(user.Email))
        {
            throw new InvalidOperationException("El usuario ya existe");
        }
        if (user.IsUserValid() && _passwordService.IsPasswordValid(user.Password))
        {
          _userDatabase.AddUser(user);
          return true;
        }
        return false;
    }
    
    
    public bool DeleteUser(string email)
    {
        if (string.IsNullOrEmpty(email)) 
        {
            throw new ArgumentException("El Email no Puede ser vacio", nameof(email));
        }

        return _userDatabase.DeleteUser(email);
    }
}