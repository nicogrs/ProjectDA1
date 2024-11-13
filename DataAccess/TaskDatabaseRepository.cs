using Interfaces;
using Dominio;
using Task = Dominio.Task;

namespace DataAccess;

public class TaskDatabaseRepository : IRepository<Task>
{
    private SqlContext _context;

    public TaskDatabaseRepository(SqlContext context)
    {
        _context = context;
    }
    
    public Task Add(Task task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return task;
    }

    public Task? Find(Func<Task, bool> filter)
    {
        return _context.Tasks.Where(filter).FirstOrDefault();
    }

    public IList<Task> FindAll()
    {
        return _context.Tasks.ToList();
    }

    public Task? Update(Task updatedEntity)
    {
        var taskToUpdate = Find(task => task.Id == updatedEntity.Id);
        taskToUpdate.ExpirationDate = updatedEntity.ExpirationDate;
        taskToUpdate.Description = updatedEntity.Description;
        taskToUpdate.Precedence = updatedEntity.Precedence;
        taskToUpdate.Description = updatedEntity.Description;
        taskToUpdate.Name = updatedEntity.Name;
        _context.SaveChanges();
        return taskToUpdate;
    }

    public void Delete(int id)
    {
        var taskToDelete = Find(task => task.Id == id);
        _context.Tasks.RemoveRange(taskToDelete);
        _context.SaveChanges();
    }
}