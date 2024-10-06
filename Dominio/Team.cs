namespace Dominio;

public class Team
{
    public string Name { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public List<Dominio.Task> Tasks { get; set; }
    
    public int UserAmount { get; set; }
    
    public List<User> Users { get; set; }
    
    public List<Panel> Panels { get; set; }
    
    public void MoveExpiredTasksToExpiredPanel()
    {
        string expiredPanelName = $"{Name}_ExpiredTasks";
        Panel expiredPanel = Panels.FirstOrDefault(p => p.Name == expiredPanelName);
        if (expiredPanel == null)
        {
            expiredPanel = new Panel { Name = "ExpiredTasks" };
            Panels.Add(expiredPanel);
        }
        
        foreach (var panel in Panels)
        {
            if (panel.Name != "ExpiredTasks")
            {
                List<Task> expiredTasks = panel.Tasks.Where(t => t.IsExpired()).ToList();
                expiredPanel.Tasks.AddRange(expiredTasks);
                panel.Tasks.RemoveAll(t => t.IsExpired());
            }
        }
    }
}
    