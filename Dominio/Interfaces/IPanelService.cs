namespace Dominio;

public interface IPanelService
{
    public Panel GetPanelById(string teamName, int panelId);
}