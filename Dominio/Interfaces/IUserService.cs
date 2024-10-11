namespace Dominio;

public interface IUserService
{
    public User GetUserByEmail(string email);
    public bool UpdateUser(User user);
    public string ResetUserPassword(string email);
    public bool CreateUser(User user);
    public bool DeleteUser(string email);
}