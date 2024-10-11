namespace Logica;
using Dominio;

public class Session
{
    public User CurrentUser { get; private set; }
    public bool IsAdmin => CurrentUser != null && CurrentUser.Admin;

    public void Login(User user)
    {
        CurrentUser = user;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
    
}