namespace Dominio;

public class TaskService
{
    private readonly IPanelService _panelService;
    private readonly ITeamService _teamService;

    public TaskService(IPanelService panelService, ITeamService teamService)
    {
        _panelService = panelService;
        _teamService = teamService;
    }
    
   public List<Task> GetAllExpiredTasks(string teamName)
    {
        List<Task> expiredTasks = new List<Task>();
        var team = _teamService.GetTeamByName(teamName);
        foreach (var panel in team.Panels )
        {
            var expiredInPanel = panel.Tasks.
                Where(x => x.expDate <= DateTime.Now).ToList();  
          
            expiredTasks.AddRange(expiredInPanel);
        }
        return expiredTasks;
    }
    
    public List<Task> GetNonExpiredTasks(string teamName, int panelId)
    {
        var panel = _panelService.GetPanelById(teamName, panelId);
        return panel.Tasks.Where(x => x.expDate > DateTime.Now).ToList();
    }
}