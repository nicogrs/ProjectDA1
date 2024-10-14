namespace Dominio;

public class Panel : IPaperBinItem
{
    public static int contadorId = 0;
    
    public string Name { get; set; }
    public string Team { get; set; }
    public int PanelId { get; set; }
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
        contadorId++;
        PanelId = contadorId;
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
        var task = Tasks.Find(x => x.TaskId == taskId);
        if (task != null)
        {
            Tasks.Remove(task);
        }
    }

    public override string ToString()
    {
        return $"Tipo: Panel - ID: {PanelId} - Nombre: {Name}";
    }
}
