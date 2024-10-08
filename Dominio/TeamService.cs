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
        if(team.TeamValidation())
        {
            _teamDatabase.AddTeamToDatabase(team);
            Console.WriteLine("Team has been created");
            return true;
        }
        return false;
    }

    public bool TeamExists(string teamName)
    { 
        return _teamDatabase.GetTeamByName(teamName) != null ? true : false;
    }

    public Team GetTeamByName(string teamName)
    {
        return _teamDatabase.GetTeamByName(teamName);
    }
    
    public bool UserExistsInTeam(string userEmail, string teamName)
    {
        var team = _teamDatabase.GetTeamByName(teamName);
        if (team == null)
        {
            return false;
        }
        return team.TeamMembers.Any(x => x.Email == userEmail);
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
}