namespace Dominio.Data;

public class TeamDataBase : ITeamDatabase
{
    public List<Team> Teams { get; set; }

    public TeamDataBase()
    {
        Teams = new List<Team>();
    }
    
    public Team GetTeamByName(string teamName)
    {
        var team = Teams.Find(x => x.Name == teamName);
        if (team == null)
        {
            throw new NullReferenceException("Team not found");
        }

        return team;
    }

    public void AddTeamToDatabase(Team team)
    {
        Teams.Add(team);
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
      return  Teams.Where(x => x.TeamMembers.Exists(y=> y.Email == email)).ToList();
    }

    public List<Team> GetTeams()
    {
        throw new NotImplementedException();
    }
}