namespace Dominio.Data;

public class TeamDatabase
{
    public List<Team> Teams { get; set; }

    public TeamDatabase()
    {
        this.Teams = new List<Team>();
    }
    
    public Team GetTeamByName(string name)
    {
        return Teams.Find(t => t.Name == name);
    }
}