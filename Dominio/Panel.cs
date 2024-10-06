namespace Dominio;
using Task = Dominio.Task;

public class Panel
{
    public string Name { get; set; }
    
    public Team Team { get; set; }
    
    public string Description { get; set; }
    
    public User Creator { get; set; }
    
    public List<Dominio.Task> Tasks { get; set; }
}