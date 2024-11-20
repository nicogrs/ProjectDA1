using Dominio;
using Interfaces;

namespace DataAccess;

public class PanelDatabaseRepository : IRepository<Panel>
{
    private SqlContext _context;

    public PanelDatabaseRepository(SqlContext context)
    {
        _context = context;
    }
    public Panel Add(Panel panel)
    {
        _context.Panels.Add(panel);
        _context.SaveChanges();
        return panel;
    }

    public Panel? Find(Func<Panel, bool> filter)
    {
       return _context.Panels.Where(filter).FirstOrDefault();
    }

    public IList<Panel> FindAll()
    {
        return _context.Panels.ToList();
    }

    public Panel? Update(Panel updatedEntity)
    {
        var panelToUpdate = Find(p => p.Id == updatedEntity.Id);
        panelToUpdate.Name = updatedEntity.Name;
        panelToUpdate.Description = updatedEntity.Description;
        panelToUpdate.Team = updatedEntity.Team;
        panelToUpdate.Tasks = updatedEntity.Tasks;
        _context.SaveChanges();
        return panelToUpdate;
    }

    public void Delete(int id)
    {
        var panel = Find(p => p.Id == id);
        _context.Panels.RemoveRange(panel);
        _context.SaveChanges();
    }
}