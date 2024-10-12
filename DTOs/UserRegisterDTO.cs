using System.ComponentModel.DataAnnotations;
using Dominio;
namespace DTOs;

public class UserRegisterDTO
{
    [Required (ErrorMessage = "El nombre es requerido")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string Name { get; set; }
    
    [Required (ErrorMessage = "El apellido es requerido")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 100 caracteres")]
    public string Surname { get; set; }
    
    [Required (ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; }
    
    [Required (ErrorMessage = "La fecha de nacimiento es requerido")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    
    [Required (ErrorMessage = "La contraseña es requerido")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{8,}$", 
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluir una mayúscula, una minúscula, un número y un carácter especial (@, #, $, etc.).")]
    public string Password { get; set; }
    
    public bool Admin { get; set; }
    
    public List<Team> Teams { get; set; }
    
    public User ToEntity()
    {
        return new User(){
            Name = this.Name,
            Surname = this.Surname,
            BirthDate = this.BirthDate,
            Email = this.Email,
            Password = this.Password,
            Admin = this.Admin
        };
    }
    
    
}