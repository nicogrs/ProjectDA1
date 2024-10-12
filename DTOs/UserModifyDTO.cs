using System.ComponentModel.DataAnnotations;
using Dominio;
namespace DTOs;

public class UserModifyDTO
{

    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string Name { get; set; }
    
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 100 caracteres")]
    public string Surname { get; set; }

    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public User ToEntity()
    {
        return new User();
    }
}