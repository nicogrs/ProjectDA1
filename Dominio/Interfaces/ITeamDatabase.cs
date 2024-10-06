namespace Dominio;

public interface ITeamDatabase
{
    public Team GetTeamByName(int teamId);
    public void AddTeamToDatabase(Team team);
    public void RemoveTeamFromDatabase(Team team);
    public void UpdateTeamInDatabase(Team team);
}