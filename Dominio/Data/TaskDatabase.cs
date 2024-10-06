namespace Dominio.Data;


public class TaskDatabase
{
    public List<Task> Tasks { get; set; }
    
    
    public TaskDatabase()
    {
        Tasks = new List<Task>();
    }
    public Task GetTaskByName(string name)
    {
        return Tasks.Find(t => t.Title == name);
    }
}