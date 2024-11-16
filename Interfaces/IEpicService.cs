using Dominio;
using Task = Dominio.Task;

namespace Interfaces;

public interface IEpicService
{
    public Epic CreateEpic(Epic epic);
    public Epic GetEpicById(int epicId);
    public List<Epic> GetEpicsByPanelId(int pId);
    public Epic AddTaskToEpic(int epicId, Task task);
    public Epic RemoveTaskFromEpic(int epicId, Task task);
    public Epic UpdateEpic(Epic epic);
    public int CalculateEpicValues(int epicId, Func<Task, int> valueSelector);
    public int EffortInverted(int epicId);
    public int EffortExpected(int epicId);
    public int CompletedTasks(int epicId);
    public int NotCompletedTasks(int epicId);
}