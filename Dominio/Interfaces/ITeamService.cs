namespace Dominio;

public interface ITeamService
{
    public bool CreateTeam(Team team);
    public bool TeamExists(string teamName);
    public bool UserExistsInTeam(string userEmail, string teamName);
    public bool AddUserToTeam(string teamName, string userEmail);
    public bool RemoveUserFromTeam(string teamName, string userEmail);
    public List<Team> GetTeamsByUserEmail(string userEmail);
    public List<Team> GetAllTeams();
    public Team GetTeamByName(string teamName);
    
    
}