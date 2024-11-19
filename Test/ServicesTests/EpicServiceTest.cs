using DataAccess;
using Dominio;
using Interfaces;
using Services;
using Test.Context;
using Task = Dominio.Task;

namespace Test.ServicesTests;

[TestClass]
public class EpicServiceTest
{
    private EpicService epicService;
    private SqlContext _context;
    private IRepository<Epic> _epicDatabase;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _epicDatabase = new EpicDatabaseRepository(_context);
        epicService = new EpicService(_epicDatabase);
    }

    [TestMethod]
    public void CreateEpicTest()
    {

        Epic epic = new Epic
        {
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
        };
        var result = epicService.CreateEpic(epic);
        Assert.AreEqual(epic, result);
    }

    [TestMethod]
    public void GetEpicByIdTest()
    {
        Epic epic = new Epic
        {
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
        };
        epicService.CreateEpic(epic);
        var result = epicService.GetEpicById(epic.Id);
        Assert.AreEqual(result, epic);
    }

    [TestMethod]
    public void AddTaskToEpicTest()
    {
        Task task = new Task
        {
            Name = "Test",
            Description = "TaskDescription"
        };
        Epic epic = new Epic
        {
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
        };
        epicService.CreateEpic(epic);
        var result = epicService.AddTaskToEpic(epic.Id, task);
        CollectionAssert.Contains(result.Tasks, task);
    }
    
    [TestMethod]
    public void RemoveTaskFormEpicTest()
    {
        Task task = new Task
        {
            Name = "Test",
            Description = "TaskDescription"
        };
        Epic epic = new Epic
        {
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
            Tasks = new List<Task>{task}
        };
        epicService.CreateEpic(epic);
        var result = epicService.RemoveTaskFromEpic(epic.Id, task);
        CollectionAssert.DoesNotContain(result.Tasks, task);
    }
    
    [TestMethod]
    public void UpdateEpicTest()
    {
        Epic epic = new Epic
        {
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
        };
        epicService.CreateEpic(epic);
        epic.Description = "UpdatedDescriptionTest";
        var updatedEpic = epicService.UpdateEpic(epic);
        Assert.AreNotEqual(updatedEpic.Description , "DescriptionTest");
    }

    [TestMethod]
    public void DeleteEpicByIdTest()
    {
        Epic epic = new Epic
        {
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
        };
        var epicCreated = epicService.CreateEpic(epic);
        Assert.AreEqual(epicCreated, epic);
        epicService.DeleteEpicById(epic.Id);
       var result = epicService.GetEpicById(epic.Id);
       Assert.IsNull(result);
    }
    
    [TestMethod]
    public void GetEpicsByPanelId()
    {
        Panel p = new Panel
        {
            Name = "Test",
            Description = "DescriptionTest",
            Team = "Test"
        };
        Epic epic = new Epic
        {
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
            FromPanel = p
        };
        epicService.CreateEpic(epic);
        var result = epicService.GetEpicsByPanelId(p.Id);
        CollectionAssert.Contains(result, epic);
    }

    [TestMethod]
    public void CalculateEpicValues_Expected()
    {
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpectedEffort = 5,
            InvertedEffort = 7,
            Ended = true
        };
        var task2 = new Task
        {
            Name = "Task 2",
            Description = "description",
            ExpectedEffort = 10,
            InvertedEffort = 5,
            Ended = true
        };
        var task3 = new Task
        {
            Name = "Task 3",
            Description = "description",
            ExpectedEffort = 7,
            InvertedEffort = 10,
            Ended = true
        };
        Epic e = new Epic
        {
            Id=1,
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
            Tasks = new List<Task>{task1,task2,task3}
        };
        epicService.CreateEpic(e);
        
        var result = epicService.EffortExpected(e.Id);
        Assert.AreEqual(22, result); 
    }
    
    [TestMethod]
    public void CalculateEpicValues_Inverted()
    {
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpectedEffort = 5,
            InvertedEffort = 7,
            Ended = true
        };
        var task2 = new Task
        {
            Name = "Task 2",
            Description = "description",
            ExpectedEffort = 10,
            InvertedEffort = 7,
            Ended = true
        };
        var task3 = new Task
        {
            Name = "Task 3",
            Description = "description",
            ExpectedEffort = 7,
            InvertedEffort = 7,
            Ended = true
        };
        Epic e = new Epic
        {
            Id=1,
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
            Tasks = new List<Task>{task1,task2,task3}
        };
        epicService.CreateEpic(e);
        
        var result = epicService.EffortInverted(e.Id);
        Assert.AreEqual(21, result); 
    }
    
    [TestMethod]
    public void CalculateEpicValues_Completed()
    {
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpectedEffort = 5,
            InvertedEffort = 7,
            Ended = true
        };
        var task2 = new Task
        {
            Name = "Task 2",
            Description = "description",
            ExpectedEffort = 10,
            InvertedEffort = 7,
            Ended = false
        };
        var task3 = new Task
        {
            Name = "Task 3",
            Description = "description",
            ExpectedEffort = 7,
            InvertedEffort = 7,
            Ended = false
        };
        Epic e = new Epic
        {
            Id=1,
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
            Tasks = new List<Task>{task1,task2,task3}
        };
        epicService.CreateEpic(e);
        
        var result = epicService.CompletedTasks(e.Id);
        Assert.AreEqual(1, result); 
    }
    
    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void CalculateEpicValues_NotCompleted()
    {
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpectedEffort = 5,
            InvertedEffort = 7,
            Ended = true
        };
        var task2 = new Task
        {
            Name = "Task 2",
            Description = "description",
            ExpectedEffort = 10,
            InvertedEffort = 7,
            Ended = false
        };
        var task3 = new Task
        {
            Name = "Task 3",
            Description = "description",
            ExpectedEffort = 7,
            InvertedEffort = 7,
            Ended = false
        };
        Epic e = new Epic
        {
            Id=1,
            Name = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
            Tasks = new List<Task>{task1,task2,task3}
        };
        epicService.CreateEpic(e);
        
        var result = epicService.NotCompletedTasks(e.Id);
        Assert.AreEqual(2, result);
    }
}