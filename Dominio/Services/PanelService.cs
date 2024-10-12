namespace Dominio;

public class PanelService
{
    private readonly ITeamService _teamService;

    public PanelService(ITeamService teamService)
    {
        _teamService = teamService;
    }
    public Panel GetPanelById(string teamName, int panelId)
    {
        var team = _teamService.GetTeamByName(teamName);
        return team.Panels.Find(x => x.PanelId == panelId);
    }
    
    public bool AddPanel(string teamName, Panel panel)
    {
        if (GetPanelById(teamName, panel.PanelId) == null)
        {
            var team = _teamService.GetTeamByName(teamName);
            team.Panels.Add(panel);
            return true;
        }
        return false;
    }
    
    
    public List<Panel> GetAllPanelsFromTeam(string teamName)
    {
        var team = _teamService.GetTeamByName(teamName);
        if (team.Panels.Count == 0)
        {
            throw new InvalidOperationException($"Team {teamName} does not have a panels");
        }
        return team.Panels;
    }
    
    public void RemovePanel(string teamName,int panelId)
    {
        var team = _teamService.GetTeamByName(teamName);
        var panel = team.Panels.Find(x => x.PanelId == panelId);
        if (panel != null)
        {
            team.Panels.Remove(panel); 
        }
    }
}