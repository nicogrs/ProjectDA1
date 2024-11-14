using Dominio;
using Interfaces;

namespace DataAccess;

public class TeamDatabaseRepository : IRepository<Team>
{
    private SqlContext _context;

    public TeamDatabaseRepository(SqlContext context)
    {
        _context = context;
    }
    
    public Team Add(Team team)
    {
        _context.Teams.Add(team);
        _context.SaveChanges();
        return team;
    }

    public Team? Find(Func<Team, bool> filter)
    {
        return _context.Teams.Where(filter).FirstOrDefault();
    }

    public IList<Team> FindAll()
    {
        return _context.Teams.ToList();
    }

    public Team? Update(Team updatedEntity)
    {
        var actualTeam = Find(t => t.Id == updatedEntity.Id);
        actualTeam.Name = updatedEntity.Name;
        actualTeam.TeamMembers = updatedEntity.TeamMembers;
        actualTeam.MembersCount = updatedEntity.MembersCount;
        actualTeam.Panels = updatedEntity.Panels;
        actualTeam.MaxUsers = updatedEntity.MaxUsers;
        actualTeam.TasksDescription = updatedEntity.TasksDescription;
        _context.SaveChanges();
        return actualTeam;
    }
    
    public void Delete(int id)
    {
        var team = Find(t => t.Id == id);
        _context.Teams.RemoveRange(team);
        _context.SaveChanges();
    }

}