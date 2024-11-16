using System.Collections;
using Dominio;
using Interfaces;
using Task = Dominio.Task;

namespace Services;

public class EpicService : IEpicService
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
    
    public Epic UpdateEpic(Epic epic)
    {
        var epicToUpdate = GetEpicById(epic.Id);
        epicToUpdate.Name = epic.Name;
        epicToUpdate.Description = epic.Description;
        epicToUpdate.Tasks = epic.Tasks;
        epicToUpdate.Priority = epic.Priority;
        epicToUpdate.Tasks = epic.Tasks;
        _epicDatabase.Update(epic);
        return epicToUpdate;
    }
    
    public List<Epic> GetEpicsByPanelId(int pId)
    {
        var epics = _epicDatabase.FindAll();
        return epics.Where(x => x.FromPanelId == pId).ToList();
    }
    
    public int CalculateEpicValues(int epicId, Func<Task, int> valueSelector)
    {
        return 0;
    }

    public int EffortInverted(int epicId)
    {
        return CalculateEpicValues(epicId, task => task.InvertedEffort);
    }

    public int EffortExpected(int epicId)
    {
        return CalculateEpicValues(epicId, task => task.ExpectedEffort);
    }

    public int CompletedTasks(int epicId)
    {
        return CalculateEpicValues(epicId, task => task.Ended ? 1 : 0);
    }

    public int NotCompletedTasks(int epicId)
    {
        return CalculateEpicValues(epicId, task => !task.Ended ? 1 : 0);
    }

}