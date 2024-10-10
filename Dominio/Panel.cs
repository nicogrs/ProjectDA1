namespace Dominio;

public class Panel
{
    public string Name { get; set; }
    public string Team { get; set; }
    public string Description { get; set; }
    public User CreatedBy { get; set; }
    public List<Task> Tasks { get; set; }

    public Panel()
    {
        Name = "";
        Team = "";
        Description = "";
        CreatedBy = null;
        Tasks = new List<Task>();
    }
    
    public void AddTask(Task task)
    {
        Tasks.Add(task);
    }

    public int getTaskCount()
    {
        return Tasks.Count;
    }
}
