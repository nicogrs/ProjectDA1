using System.Data;

namespace Dominio;

public class Epic
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Precedence Priority { get; set; }
    public string Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Task> Tasks { get; set; }
    
    public int FromPanelId { get; set; }
    public Panel FromPanel { get; set; }


    public Epic()
    {
        Tasks = new List<Task>();
    }

    public enum Precedence
    {
        Low ,
        Medium ,
        Urgent
    }

    public void AddTask(Task task)
    {
        Tasks.Add(task);
    }

    public void RemoveTask(Task task)
    {
            Tasks.Remove(task);
    }
}