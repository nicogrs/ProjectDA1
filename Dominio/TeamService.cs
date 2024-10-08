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
       if (!UserExistsInTeam(userEmail, teamName))
       {
           var team = _teamDatabase.GetTeamByName(teamName);
           team.TeamMembers.Add(user);
           return true;
       }
        return false;
    }
}