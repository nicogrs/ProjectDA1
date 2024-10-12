using System.Collections;

namespace Dominio;

public class TeamService
{
    private readonly ITeamDatabase _teamDatabase;
    private readonly IUserService _userService;

    public TeamService(ITeamDatabase teamDatabase, IUserService userService)
    {
        _teamDatabase = teamDatabase;
        _userService = userService;
    }
    
    public bool CreateTeam(Team team)
    {
        Console.WriteLine($"Creating team {team.Name}");
        if(team.TeamValidation() && !TeamExists(team.Name))
        {
            _teamDatabase.AddTeamToDatabase(team);
            Console.WriteLine("Team has been created");
            return true;
        }
        return false;
    }

    public bool TeamExists(string teamName)
    {
        return _teamDatabase.GetTeams().Exists(t => t.Name == teamName);
    }

    public Team GetTeamByName(string teamName)
    {
        return _teamDatabase.GetTeamByName(teamName);
    }
    
    public bool UserExistsInTeam(string userEmail, string teamName)
    {
        if (TeamExists(teamName))
        { 
            var team = _teamDatabase.GetTeamByName(teamName);
            return team.TeamMembers.Any(x => x.Email == userEmail);
        }
        return false;
    }
    
    public bool AddUserToTeam(string teamName, string userEmail)
    {
       var user = _userService.GetUserByEmail(userEmail);
       var team = _teamDatabase.GetTeamByName(teamName);
       if (!UserExistsInTeam(userEmail, teamName) && team.MembersCount < team.MaxUsers)  
       {
           team.TeamMembers.Add(user);
           team.MembersCount = team.TeamMembers.Count;
           return true;
       }
        return false;
    }
    public bool UpdateTeam(string teamName, Team team)
    {
        var teamToUpdate = _teamDatabase.GetTeamByName(teamName);
        Console.WriteLine($"Team {teamToUpdate.Name} has been updated");
        team.CreatedOn = teamToUpdate.CreatedOn;
        team.Panels = teamToUpdate.Panels;
        team.TeamMembers = teamToUpdate.TeamMembers;
        team.MembersCount = teamToUpdate.MembersCount;

        if (team.TeamValidation())
        {
            _teamDatabase.UpdateTeamInDatabase(team);
            return true;
        }

        return false;
    }


    public Panel GetPanelByName(string teamName, string panelName)
    {
        var team = GetTeamByName(teamName);
       return team.Panels.Find(x => x.Name == panelName);
    }
    
    public bool AddPanel(string teamName, Panel panel)
    {
        if (GetPanelByName(teamName, panel.Name) == null)
        {
            var team = _teamDatabase.GetTeamByName(teamName);
            team.Panels.Add(panel);
            return true;
        }
        return false;
    }

    public bool RemoveUserFromTeam(string teamName, string userEmail)
    {
        var user = _userService.GetUserByEmail(userEmail);
        var team = GetTeamByName(teamName);
        var result = team.TeamMembers.Remove(user);
        return result;
    }

    public bool RemoveUserFromAllTeams(string userEmail)
    {
        var hasBeenRemoved = false;
        var user = _userService.GetUserByEmail(userEmail);
        var teams = _teamDatabase.GetTeams();
        foreach (var team in teams)
        {
          hasBeenRemoved = team.TeamMembers.Remove(user);
        }
        return hasBeenRemoved;
    }


    public List<Team> GetAllTeams()
    {
        return _teamDatabase.GetTeams();
    }
}