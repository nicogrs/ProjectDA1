namespace Dominio;

public class UserService
{
    private readonly IUserDatabase _userDatabase;

    public UserService(IUserDatabase userDatabase)
    {
        _userDatabase = userDatabase;
    }

    public bool UserExists(string email)
    {
        return false;
    }
}