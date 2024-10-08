namespace Dominio;

public interface ITeamDatabase
{
    public Team GetTeamByName(string teamName);
    public void AddTeamToDatabase(Team team);
    public void RemoveTeamFromDatabase(string teamName);
    public void UpdateTeamInDatabase(Team team);
    public List<Team> GetTeamsByUserEmail(string email);
    public List<Team> GetTeams();
}