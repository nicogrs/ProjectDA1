using Dominio;

namespace Interfaces;
using Task = Dominio.Task;
public interface ITaskService
{
    public List<Task> GetAllExpiredTasks(int panelId);
    public List<Task> GetNonExpiredTasks(int panelId);
    public Task GetTaskById(int taskId);
    public List<Comment> GetCommentsFromTask(int taskId);
    public Task UpdateTask(Task task);
    public void AddCommentToTask(int taskId, Comment comment);
    public int GetPanelIdByTask(string teamName, int taskId);
    public void AddEffort(int taskid, int time);
    public int EffortComparated(int taskId);
    public string EffortStatus(int taskId);
    public void ChangeStatus(int taskId);

}