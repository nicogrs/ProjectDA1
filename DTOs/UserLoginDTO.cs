using Dominio;
namespace DTOs;

public class UserLoginDTO
{
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public User ToEntity()
    {
        return new User();
    }
    
    public static UserLoginDTO FromEntity(User u)
    {
        return new UserLoginDTO();
        
    }
}