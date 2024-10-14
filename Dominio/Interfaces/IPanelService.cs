namespace Dominio;

public interface IPanelService
{
    public Panel GetPanelById(string teamName, int panelId);
    public List<Panel> GetAllPanelsFromTeam(string teamName);
}