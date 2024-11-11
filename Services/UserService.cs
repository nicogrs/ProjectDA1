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
    public List<User> GetAllUsers()
    {
        return _userDatabase.FindAll().ToList();
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

    public void AddElementToPaperBin(string email, IDeleteable element)
    {
      var user = GetUserByEmail(email);
      user.PaperBin.AddElementToPaperbin(element);
      _userDatabase.Update(user);
    }

    public void DeleteElementFromPaperbin(string email, IDeleteable element)
    {
        var user = GetUserByEmail(email);
        user.PaperBin.DeleteElementFromPaperbin(element);
        _userDatabase.Update(user);
    }
    
    public List<IDeleteable> GetDeletedElements(string userEmail)
    {
        var user = GetUserByEmail(userEmail);
        return user.PaperBin.Paperbin;
    }

    public void RestoreElement(int elementId, string email)
    {
        throw new NotImplementedException();
    }
    
}