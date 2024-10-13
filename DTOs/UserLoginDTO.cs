using Dominio;
namespace DTOs;

public class UserLoginDTO
{
    public string Email { get; set; }
    
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