namespace Dominio.Data;

public class PanelDatabase
{
    public List<Panel> Panels { get; set; }
    public PanelDatabase()
    {
        this.Panels = new List<Panel>();
    }
    public Panel GetPanelByName(string name)
    {
        return Panels.Find(p => p.Name == name);
    }
    
}