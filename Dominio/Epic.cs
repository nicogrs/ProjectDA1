using System.Data;

namespace Dominio;

public class Epic
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Precedence Priority { get; set; }
    public string Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Task> Tasks { get; set; }



    public enum Precedence
    {
        Low ,
        Medium ,
        Urgent
    }

    public void AddTask(Task task)
    {
        Tasks.Add(task);
        task.Epic = this;
    }

    public void RemoveTask(Task task)
    {
        if (task.Epic == this)
        {
            Tasks.Remove(task);
            task.Epic = null;
        }
    }
}