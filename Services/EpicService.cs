using Dominio;
using Interfaces;
using Task = Dominio.Task;

namespace Services;

public class EpicService
{
    private readonly IRepository<Epic> _epicDatabase;

    public EpicService(IRepository<Epic> epicDatabase)
    {
        _epicDatabase = epicDatabase;
    }
    
    public Epic CreateEpic(Epic epic)
    {
      return _epicDatabase.Add(epic);
    }

    public Epic GetEpicById(int epicId)
    {
        return _epicDatabase.Find(x => x.Id == epicId);
    }
    public Epic AddTaskToEpic(int epicId, Task task)
    {
        var epic = GetEpicById(epicId);
        epic.AddTask(task);
        UpdateEpic(epic);
        return epic;
    }

    public Epic RemoveTaskFromEpic(int epicId, Task task)
    {
        var epic = GetEpicById(epicId);
        epic.RemoveTask(task);
        UpdateEpic(epic);
        return epic;
    }
    
    public Epic UpdateEpic(Epic epic){
        var epicToUpdate = GetEpicById(epic.Id);
        epicToUpdate.Title = epic.Title;
        epicToUpdate.Description = epic.Description;
        epicToUpdate.Tasks = epic.Tasks;
        epicToUpdate.Priority = epic.Priority;
        epicToUpdate.Tasks = epic.Tasks;
        _epicDatabase.Update(epic);
        return epicToUpdate;
    }


}