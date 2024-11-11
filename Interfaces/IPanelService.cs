namespace Interfaces;
using Dominio;
public interface IPanelService
{

    public bool AddPanel(Panel panel);
    public void AddTaskToPanel(int panelId, Task task);
    public void RemoveTaskFromPanel(int panelId, Task task);
    public void RemovePanel(int panelId);
    public Panel GetPanelById(int panelId);
    public List<Panel> GetAllPanelsFromTeam(string teamName);
}