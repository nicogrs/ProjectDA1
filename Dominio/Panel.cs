namespace Dominio;

public class Panel
{
    public string Id { get; set; } 
    public string Name { get; set; }
    public string Team { get; set; }
    public string Description { get; set; }
    public User CreatedBy { get; set; }
    public List<Task> Tasks { get; set; }

    private static int panelCounter = 0;

    public Panel()
    {
        Name = "";
        Team = "";
        Description = "";
        CreatedBy = null;
        Tasks = new List<Task>();
        Id = "";
    }
    
    public Panel(string type, int teams)
    {
        Name = "";
        Team = "";
        Description = "";
        CreatedBy = null;
        Tasks = new List<Task>();

        if (type == "expiredTasks")
        {
            Id = new string('0', teams);
        }
        else if(type == "panel")
        {
            panelCounter++;
            Id = panelCounter.ToString();
        }
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
