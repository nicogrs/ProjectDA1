namespace Dominio;

public class Panel : IPaperBinElement
{
    public static int contadorDeId = 0;
    
    public string Name { get; set; }
    public string Team { get; set; }
    public string Description { get; set; }
    public User CreatedBy { get; set; }
    public List<Task> Tasks { get; set; }
    public int PanelId { get; private set; }
    

    public Panel()
    {
        Name = "";
        Team = "";
        Description = "";
        CreatedBy = null;
        Tasks = new List<Task>();
        contadorDeId++;
        PanelId = contadorDeId;
    }
    
    public void AddTask(Task task)
    {
        Tasks.Add(task);
    }

    public int getTaskCount()
    {
        return Tasks.Count;
    }


    public void DeleteTask(int taskId)
    {
        var task = Tasks.Find(x => x.TaskId == taskId);
        if (task != null)
        {
            Tasks.Remove(task);
        }
    }
}
