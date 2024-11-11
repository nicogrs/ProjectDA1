namespace Services;
using Dominio;
using Interfaces;

public class TeamService : ITeamService
{
    private readonly IRepository<Team> _teamDatabase;
    private readonly IUserService _userService;

    public TeamService(IRepository<Team> teamDatabase, IUserService userService)
    {
        _teamDatabase = teamDatabase;
        _userService = userService;
    }
    
    public bool CreateTeam(Team team)
    {
        if(team.TeamValidation() && !TeamExists(team.Name))
        {
            _teamDatabase.Add(team);
            return true;
        }
        return false;
    }

    public bool TeamExists(string teamName)
    {
        return _teamDatabase.Find(t => t.Name == teamName) != null;
    }
    

    public Team GetTeamByName(string teamName)
    {
        return _teamDatabase.Find(t => t.Name == teamName);
    }
    
    public bool UserExistsInTeam(string userEmail, string teamName)
    {
        if (TeamExists(teamName))
        { 
            var team = GetTeamByName(teamName);
            return team.TeamMembers.Any(x => x.Email == userEmail);
        }
        return false;
    }
    
    public bool AddUserToTeam(string teamName, string userEmail)
    {
       var user = _userService.GetUserByEmail(userEmail);
       var team = GetTeamByName(teamName);
       if (!UserExistsInTeam(userEmail, teamName) && team.MembersCount < team.MaxUsers)  
       {
           team.TeamMembers.Add(user);
           team.MembersCount = team.TeamMembers.Count;
           _teamDatabase.Update(team);
           return true;
       }
        return false;
    }
    public bool UpdateTeam(string teamName, Team team)
    {
        var teamToUpdate = GetTeamByName(teamName);
        teamToUpdate.Name = team.Name;
        teamToUpdate.MaxUsers = team.MaxUsers;
        teamToUpdate.TasksDescription = team.TasksDescription;
        if (teamToUpdate.TeamValidation())
        {
            _teamDatabase.Update(teamToUpdate);
            Console.WriteLine($"Team {teamToUpdate.Name} has been updated");
            return true;
        }

        return false;
    }
    
    public bool RemoveUserFromTeam(string teamName, string userEmail)
    {
        var user = _userService.GetUserByEmail(userEmail);
        var team = GetTeamByName(teamName);
        var result = team.TeamMembers.Remove(user);
        team.MembersCount--;
        _teamDatabase.Update(team);
        return result;
    }

    public bool RemoveUserFromAllTeams(string userEmail)
    {
        var hasBeenRemoved = false;
        var user = _userService.GetUserByEmail(userEmail);
        var teams = GetTeamsByUserEmail(userEmail);
        foreach (var team in teams)
        {
          hasBeenRemoved = team.TeamMembers.Remove(user);
          _teamDatabase.Update(team);
        }
        return hasBeenRemoved;
    }

    public void AddPanelToTeam(Panel panel, string teamName)
    {
        var team = GetTeamByName(teamName);
        team.Panels.Add(panel);
        _teamDatabase.Update(team);
    }

    public void RemovePanelFromTeam(Panel panel, string teamName)
    {
        var team = GetTeamByName(teamName);
        team.Panels.Remove(panel);
        _teamDatabase.Update(team);
    }
    
    public List<Team> GetAllTeams()
    {
        return _teamDatabase.FindAll().ToList();
    }

    public List<Team> GetTeamsByUserEmail(string userEmail)
    {
        List<Team> teams = _teamDatabase.FindAll().ToList();
        return teams.Where(x => x.TeamMembers.Exists(y => y.Email == userEmail)).ToList();
    }

    public void DeleteTeam(string teamName)
    {
        var team = GetTeamByName(teamName);
        _teamDatabase.Delete(team.Id);
    }
    
}