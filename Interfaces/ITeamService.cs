namespace Interfaces;
using Dominio;
public interface ITeamService
{
    public bool CreateTeam(Team team);
    public bool TeamExists(string teamName);
    public bool UserExistsInTeam(string userEmail, string teamName);
    public bool AddUserToTeam(string teamName, string userEmail);
    public bool UpdateTeam(string teamName, Team team);
    public List<Team> GetTeamsByUserEmail(string userEmail);
    public List<Team> GetAllTeams();
    public void DeleteTeam(string teamName);
    public bool RemoveUserFromAllTeams(string userEmail);
    public bool RemoveUserFromTeam(string teamName, string userEmail);
    public void AddPanelToTeam(Panel panel, string teamName);
    public void RemovePanelFromTeam(Panel panel, string teamName);
    public Team GetTeamByName(string teamName);
}