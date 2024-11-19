using System.ComponentModel.DataAnnotations;
using Dominio;
namespace DTOs;

public class UserModifyDTO
{

    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string Name { get; set; }
    
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 100 caracteres")]
    public string Surname { get; set; }

    
    [Range(typeof(DateTime), "01/01/1900", "12/12/2023", ErrorMessage = "La fecha de nacimiento debe ser menor al año actual.")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; }
    
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{8,}$", 
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluir una mayúscula, una minúscula, un número y un carácter especial (@, #, $, etc.).")]
    public string Password { get; set; }
  
    
    public User ToEntity()
        {
            return new User()
            {
                Name = this.Name,
                Surname = this.Surname,
                BirthDate = this.BirthDate,
                Email = this.Email,
                Password = this.Password,
            };
        }
}