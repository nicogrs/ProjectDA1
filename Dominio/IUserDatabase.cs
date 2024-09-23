namespace Dominio;

public interface IUserDatabase
{
    User GetUserByEmail(string email);
    List<User> GetAllUsers();
    bool AddUser(User user);
    bool UpdateUser(User user);
    bool DeleteUser(string email);
}