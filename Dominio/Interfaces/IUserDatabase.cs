namespace Dominio;

public interface IUserDatabase
{
    User GetUserByEmail(string email);
    void AddUser(User user);
    void UpdateUser(User user);
    List<User> GetUsers();
    bool DeleteUser(string email);
}