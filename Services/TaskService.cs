namespace Services;
using Dominio;
using Interfaces;

public class TaskService : ITaskService
{

    private readonly IRepository<Task> _taskDatabase;

    public TaskService(IRepository<Task> taskDatabase)
    {
        _taskDatabase = taskDatabase;
    }

    public List<Task> GetAllExpiredTasks(int panelId)
    {
        List<Task> expiredTasks = _taskDatabase.FindAll()
            .Where(x => x.ExpirationDate <= DateTime.Now
                        && x.PanelId == panelId && x.IsDeleted == false).ToList();

        return expiredTasks;

    }

    public List<Task> GetNonExpiredTasks(int panelId)
    {
        List<Task> expiredTasks = _taskDatabase.FindAll()
            .Where(x => x.ExpirationDate > DateTime.Now
                        && x.PanelId == panelId && x.IsDeleted == false).ToList();
        return expiredTasks;

    }

    public Task GetTaskById(int taskId)
    {
        return _taskDatabase.Find(x => x.Id == taskId);
    }

    public void AddCommentToTask(int taskId, Comment comment)
    {
        var task = GetTaskById(taskId);
        task.Comments.Add(comment);
        _taskDatabase.Update(task);
    }

    public List<Comment> GetCommentsFromTask(int taskId)
    {
        var task = GetTaskById(taskId);
        return task.Comments;
    }

    public int GetPanelIdByTask(string teamName, int taskId)
    {
        var task1 = GetTaskById(taskId);
        return task1.PanelId;
    }

    public void AddEffort(int taskId, int time)
    {
        if (time <= 0) return; 
        var task = GetTaskById(taskId);
        if (task != null)
        {
            task.InvertedEffort += time;
        }
    }
    
    public int EffortComparated(int taskId)
    {
        var task = GetTaskById(taskId);
        return task.ExpectedEffort - task.InvertedEffort;
    }

    public string EffortStatus(int taskId)
    {
        var task = GetTaskById(taskId);
        string s = "";
        if (task.Ended)
        {
            int effort = EffortComparated(taskId);
            if (effort < 0) s = "Subestimada";
            if (effort > 0) s = "Sobreestimada";
            if (effort == 0) s = "OK";
        }
        return s;

    }
}
