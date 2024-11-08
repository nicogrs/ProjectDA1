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
        foreach (var panel in team.Panels)
        {
            var expiredInPanel = panel.Tasks.Where(x => x.ExpirationDate <= DateTime.Now).ToList();

            expiredTasks.AddRange(expiredInPanel);
        }

        return expiredTasks;
    }

    public List<Task> GetNonExpiredTasks(string teamName, int panelId)
    {
        var panel = _panelService.GetPanelById(teamName, panelId);
        return panel.Tasks.Where(x => x.ExpirationDate > DateTime.Now).ToList();
    }

    public Task GetTaskById(string teamName, int panelId, int taskId)
    {
        var panel = _panelService.GetPanelById(teamName, panelId);
        return panel.Tasks.Find(x => x.Id == taskId);
    }

    public int GetPanelIdByTask(string teamName, int taskId)
    {
        var panels = _panelService.GetAllPanelsFromTeam(teamName);
        foreach (var panel in panels)
        {
           var task = panel.Tasks.FirstOrDefault(x => x.Id == taskId);
           if (task != null)
           {
               return panel.Id;
           }
        }

        return 0;
    }
}