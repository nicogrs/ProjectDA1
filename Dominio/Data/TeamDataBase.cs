namespace Dominio.Data;

public class TeamDataBase : ITeamDatabase
{
    public Team GetTeamByName(string teamName)
    {
        throw new NotImplementedException();
    }

    public void AddTeamToDatabase(Team team)
    {
        throw new NotImplementedException();
    }

    public void RemoveTeamFromDatabase(string teamName)
    {
        throw new NotImplementedException();
    }

    public void UpdateTeamInDatabase(Team team)
    {
        throw new NotImplementedException();
    }

    public List<Team> GetTeamsByUserEmail(string email)
    {
        throw new NotImplementedException();
    }

    public List<Team> GetTeams()
    {
        throw new NotImplementedException();
    }
}