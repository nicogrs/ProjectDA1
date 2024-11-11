namespace Dominio;

public class Panel : IDeleteable
{
    public string Team { get; set; }
    public User CreatedBy { get; set; }
    public List<Task> Tasks { get; set; }

    public Panel()
    {
        Name = "";
        Team = "";
        IsDeleted = false;
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
        if (task != null) Tasks.Remove(task);
    }

    public override string ToString()
    {
        return $"Tipo: Panel - ID: {Id} - Nombre: {Name}";
    }
}