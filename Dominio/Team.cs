namespace Dominio;

public class Team
{
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
    public string TasksDescription { get; set; }
    public int MaxUsers { get; set; }
    public List<Panel> Panels { get; set; }
}