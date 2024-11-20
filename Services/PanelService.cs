namespace Services;
using Dominio;
using Interfaces;
public class PanelService : IPanelService
{
    private readonly IRepository<Panel> _panelDatabase;

    public PanelService(IRepository<Panel> panelDatabase)
    {
        _panelDatabase = panelDatabase;
    }
    public Panel GetPanelById(int panelId)
    {
        return _panelDatabase.Find(x => x.Id == panelId);
    }
    
    public bool AddPanel(Panel panel)
    {
        if (GetPanelById(panel.Id) == null)
        {
            _panelDatabase.Add(panel);
            return true;
        }
        return false;
    }

    public void AddTaskToPanel(int panelId, Task task)
    {
        var panel = GetPanelById(panelId);
        panel.Tasks.Add(task);
        _panelDatabase.Update(panel);
    }

    public void RemoveTaskFromPanel(int panelId, Task task)
    {
        var panel = GetPanelById(panelId);
        panel.Tasks.Remove(task);
        _panelDatabase.Update(panel);
    }
    
    public List<Panel> GetAllPanelsFromTeam(string teamName)
    {
        var panels = _panelDatabase.FindAll();
        return panels.Where(x => x.Team == teamName && x.IsDeleted == false).ToList();
    }
    
    public Panel RemovePanel(int panelId)
    {
        var panel = _panelDatabase.Find(x => x.Id == panelId);
        if (panel != null)
        {
            _panelDatabase.Delete(panel.Id);
        }

        return panel;
    }
}