namespace InterfazWeb;
using Dominio;

public class Session
{
    public User CurrentUser { get; set; }

    public bool IsAdmin => CurrentUser != null && CurrentUser.Admin;

    public event Action OnChange;


    
    public void Login(User user)
    {
        CurrentUser = user;
        NotifyStateChanged();
    }

    public void Logout()
    {
        CurrentUser = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    { 
        OnChange?.Invoke();
    }
        
    
}