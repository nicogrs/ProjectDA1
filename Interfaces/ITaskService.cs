using Dominio;

namespace Interfaces;
using Task = Dominio.Task;
public interface ITaskService
{
    public List<Task> GetAllExpiredTasks(int panelId);
    public List<Task> GetNonExpiredTasks(int panelId);
    public Task GetTaskById(int taskId);
    public List<Comment> GetCommentsFromTask(int taskId);
    public void AddCommentToTask(int taskId, Comment comment);
    public int GetPanelIdByTask(string teamName, int taskId);
}