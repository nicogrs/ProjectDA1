using System.ComponentModel.DataAnnotations;
using Dominio;
namespace DTOs;

public class UserLoginDTO
{
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato de email es incorrecto")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "La contraseña es requerida")]
    public string Password { get; set; }
    
    public User ToEntity()
    {
        return new User()
        {
            Email = this.Email,
            Password = this.Password,
        };
    }
    
    public static UserLoginDTO FromEntity(User u)
    {
        return new UserLoginDTO()
        {
            Email = u.Email,
            Password = u.Password
        };
        
    }
}