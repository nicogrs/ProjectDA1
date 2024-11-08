namespace Dominio;

public class Panel : IPaperBinItem
{
    public int Id { get; set; }
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

    public int GetTaskCount()
    {
        return Tasks.Count;
    }


    public void DeleteItem(int taskId)
    {
        var task = Tasks.Find(x => x.Id == taskId);
        if (task != null)
        {
            Tasks.Remove(task);
        }
    }

    public override string ToString()
    {
        return $"Tipo: Panel - ID: {Id} - Nombre: {Name}";
    }
    
}
