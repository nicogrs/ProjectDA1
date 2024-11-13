using Interfaces;
using Services;
using DataAccess;
using Test.Context;
namespace Test;
using Dominio;


[TestClass]
public class TaskServiceTest
{
    private TaskService _taskService;
    private IRepository<Task> _taskRepository;
    private SqlContext _context;
    
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();

        _taskRepository = new TaskDatabaseRepository(_context);
        _taskService = new TaskService(_taskRepository);
    }
    
    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void GetExpiredTasksFromPanels()
    {
        var panelId = 1;
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "This is a test task description",
            ExpirationDate = DateTime.Now.AddHours(-1),
            PanelId = panelId
        };
        _taskRepository.Add(task1);
        var expiredTasks = _taskService.GetAllExpiredTasks(panelId);
        CollectionAssert.AreEqual(new List<Task> { task1 }, expiredTasks);
    }

    [TestMethod]
    public void GetNonExpiredTasksFromPanel()
    {
        var panelId = 1;
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpirationDate = DateTime.Now.AddHours(+1),
            PanelId = panelId
        };
        _taskRepository.Add(task1);
        var nonExpiredTasks = _taskService.GetNonExpiredTasks(panelId);
        CollectionAssert.AreEqual(new List<Task> { task1 }, nonExpiredTasks);
    }

    [TestMethod]
    public void GetTaskByIdTest()
    {
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpirationDate = DateTime.Now.AddHours(+1),
        };
        _taskRepository.Add(task1);
        
        var task = _taskService.GetTaskById(task1.Id);
        Assert.AreEqual(task1, task);
    }

    [TestMethod]
    public void GetPanelIdByTaskTest()
    {
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpirationDate = DateTime.Now.AddHours(-1),
            PanelId = 1
        };
        _taskRepository.Add(task1);

        var panelId = _taskService.GetPanelIdByTask("Team Name", task1.Id);

        Assert.AreEqual(1, panelId);
    }

    [TestMethod]
    public void AddEffort_PositiveTime()
    {
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpirationDate = DateTime.Now.AddHours(-1),
            InvertedEffort = 0
        };
        _taskRepository.Add(task1);
        _taskService.AddEffort(task1.Id, 5);
        var updatedTask = _taskRepository.Find(x => x.Id == task1.Id);
        Assert.AreEqual(5, updatedTask.InvertedEffort);
    }

    [TestMethod]
    public void AddEffort_ZeroTime()
    {
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpirationDate = DateTime.Now.AddHours(-1),
            InvertedEffort = 10
        };
        _taskRepository.Add(task1);
        _taskService.AddEffort(task1.Id, 0);
        var updatedTask = _taskRepository.Find(x => x.Id == task1.Id);
        Assert.AreEqual(10, updatedTask.InvertedEffort);
    }
    
    [TestMethod]
    public void AddEffort_NegativeTime()
    {
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpirationDate = DateTime.Now.AddHours(-1),
            InvertedEffort = 10
        };
        _taskRepository.Add(task1);
        _taskService.AddEffort(task1.Id, -5);
        var updatedTask = _taskRepository.Find(x => x.Id == task1.Id);
        Assert.AreEqual(10, updatedTask.InvertedEffort);
    }

    [TestMethod]
    public void AddEffort_AccumulateInvertedEffort()
    {
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpirationDate = DateTime.Now.AddHours(-1),
            InvertedEffort = 0
        };
        _taskRepository.Add(task1);
        _taskService.AddEffort(task1.Id, 2);
        _taskService.AddEffort(task1.Id, 3);
        var updatedTask = _taskRepository.Find(x => x.Id == task1.Id);
        Assert.AreEqual(5, updatedTask.InvertedEffort);
    }
}