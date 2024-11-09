using System.Data;
using System.Text;
using Interfaces;

namespace Services;
using Dominio;
public class UserService : IUserService
{
    private readonly IRepository<User> _userDatabase;
    private readonly PasswordService _passwordService;

    public UserService(IRepository<User> userRepo)
    {
        _passwordService = new PasswordService();
        _userDatabase= userRepo;
    }

    public bool UserExists(string email)
    {
       return _userDatabase.Find(u => u.Email == email) != null;
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
            _userDatabase.Add(user);
            return true;
        }
        catch (Exception e)
        {
            throw new NullReferenceException("Error al agregar un usuario en la base de datos", e);
        }

    }

    public User GetUserByEmail(string email)
    {
      return  _userDatabase.Find(u => u.Email == email);
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
                Surname = actualUser.Surname,
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
                Console.WriteLine("entro a update");
                _userDatabase.Update(user);
                return true;
            }
        Console.WriteLine("No se puede modificar el usuario");
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
       _userDatabase.Delete(GetUserByEmail(email).Id);
       return true;
    }

    public void AddTaskToPaperBin(Panel panel, Task task, string email)
    {
        throw new NotImplementedException();
    }

    public void AddPanelToPaperBin(Team team, Panel panel, string email)
    {
        throw new NotImplementedException();
    }
}