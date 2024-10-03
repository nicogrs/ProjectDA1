namespace Dominio;

public interface IUserDatabase
{
    User GetUserByEmail(string email);
    List<User> GetAllUsers();
    void AddUser(User user);
    void UpdateUser(User user);
    bool DeleteUser(string email);
}