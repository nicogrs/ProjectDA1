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
        var team = GetTeamByName(teamName);
        Teams.Remove(team);
    }

    public void UpdateTeamInDatabase(Team team)
    {
        var teamInDb = GetTeamByName(team.Name);
        if (teamInDb != null)
        {
            teamInDb.MaxUsers = team.MaxUsers;
            teamInDb.TasksDescription = team.TasksDescription;    
        }
    }

    public List<Team> GetTeamsByUserEmail(string email)
    {
      return  Teams.Where(x => x.TeamMembers.Exists(y=> y.Email == email)).ToList();
    }

    public List<Team> GetTeams()
    {
        return Teams;
    }
}