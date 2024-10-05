using System.Data;
using System.Text;

namespace Dominio;

public class UserService : IUserService
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

        if (!_passwordService.IsPasswordValid(user.Password))
        {
            throw new InvalidOperationException("La contraseña es invalida");
        }

        if (!user.IsUserValid())
        {
            throw new ArgumentException("Usuario no valido");
        }
        try
        {
            _userDatabase.AddUser(user);
            return true;
        }
        catch (Exception e)
        {
            throw new NullReferenceException("Error al agregar un usuario en la base de datos", e);
        }

    }

    public User GetUserByEmail(string email)
    {
        return _userDatabase.GetUserByEmail(email);
    }
    
    public string ResetUserPassword(string email)
    {
        try
        {
            var actualUser = GetUserByEmail(email);
            string newPassword = _passwordService.GenerateRandomPassword();
            var userWithNewPassword = new User
            {
                Name = actualUser.Name,
                Email = actualUser.Email,
                Password = newPassword,
                BirthDate = actualUser.BirthDate,
            };
            UpdateUser(userWithNewPassword);
            return newPassword;
        }
        catch (Exception e)
        {
            throw new DataException("Error al resetear la contraseña en la base de datos", e);
        }
    }

    public bool UpdateUser(User user)
    {
        try
        {
            if (user.IsUserValid() && _passwordService.IsPasswordValid(user.Password))
            {
                _userDatabase.UpdateUser(user);
                return true;
            }
        Console.WriteLine("No se puede realizar el usuario");
            return false;
        }
        catch (Exception e)
        {
            throw new InvalidDataException("No es posible actualizar el usuario en la base de datos", e);
        }
        
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