using Dominio.Data;
using Task = Dominio.Task;

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


    public List<Team> GetTeams()
    {
        return CurrentUser?.Teams ?? new List<Team>();
    }
    public List<Panel> GetPanels(string teamName)
    {
        var team = CurrentUser?.Teams.FirstOrDefault(t => t.Name == teamName);
        return team?.Panels ?? new List<Panel>();
    }
    
    public List<Task> GetTasks(string teamName, string panelName)
    {
        var team = CurrentUser?.Teams.FirstOrDefault(t => t.Name == teamName);
        var panel = team?.Panels.FirstOrDefault(p => p.Name == panelName);
        return panel?.Tasks ?? new List<Task>();
    }
}