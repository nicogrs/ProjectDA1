namespace Dominio;

public class TeamService
{
    private readonly ITeamDatabase _teamDatabase;

    public TeamService(ITeamDatabase teamDatabase)
    {
        _teamDatabase = teamDatabase;
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


    public bool AddUserToTeam(Team team, string userEmail)
    {
        return false;
    }
}