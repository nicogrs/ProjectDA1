namespace Dominio;

public interface IUserDatabase
{
    User GetUserByEmail(string email);
    void AddUser(User user);
    void UpdateUser(User user);
    bool DeleteUser(string email);
}