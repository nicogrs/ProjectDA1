using System.Collections;
using DataAccess;
using Test.Context;
namespace Test.DataAccessTests;
using Task = Dominio.Task;

[TestClass]
public class TaskDatabaseRepositoryTest
{
    private Task task1;
    private Task task2;
    private SqlContext _context;
    private TaskDatabaseRepository _repository;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _repository = new TaskDatabaseRepository(_context);

        task1 = new Task
        {
            Name = "Task 1",
            Description = "This is a test task description",
            ExpirationDate = DateTime.Now.AddHours(+1),
            PanelId = 1,
            Ended = false
        };
        task2 = new Task
        {
            Name = "Task 1",
            Description = "This is a test task description",
            ExpirationDate = DateTime.Now.AddHours(+1),
            PanelId = 1,
            Ended = true
        };
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Tasks.RemoveRange(_context.Tasks);
        _context.SaveChanges();
    }

    [TestMethod]
    public void AddTest()
    {
        var addedTask = _repository.Add(task1);

        Assert.IsNotNull(addedTask);
        Assert.AreEqual("Task 1", addedTask.Name);
        Assert.AreEqual(1, _context.Tasks.Count());
    }

    [TestMethod]
    public void FindTest()
    {
        _context.Tasks.Add(task1);
        _context.SaveChanges();

        var foundTask = _repository.Find(t => t.Name == task1.Name);
        var notFoundTask = _repository.Find(t => t.Name == "Task 3");
        
        Assert.IsNull(notFoundTask);
        Assert.IsNotNull(foundTask);
        Assert.AreEqual("Task 1", foundTask.Name);
    }

    [TestMethod]
    public void UpdateTest()
    {
        _context.Tasks.Add(task1);
        _context.SaveChanges();

        var updatedTask = new Task { Name = "Task 11", Description = "Descripcion 1"};
        var result = _repository.Update(updatedTask);

        Assert.IsNotNull(result);
        Assert.AreEqual("Task 11", result.Name);
        Assert.AreEqual("Descripcion 1", result.Description);
    }

    [TestMethod]
    public void DeleteTest()
    {
        _context.Tasks.Add(task1);
        _context.SaveChanges();

        _repository.Delete(task1.Id);

        Assert.AreEqual(0, _context.Tasks.Count());
    }

    [TestMethod]
    public void FindAllTest()
    {
        _context.Tasks.Add(task1);
        _context.Tasks.Add(task2);
        _context.SaveChanges();

        var allTasks = _repository.FindAll();

        Assert.AreEqual(2, allTasks.Count);
        CollectionAssert.Contains((ICollection)allTasks, task1);
        CollectionAssert.Contains((ICollection)allTasks, task2);
    }
}