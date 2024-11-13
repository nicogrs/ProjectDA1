using Interfaces;
using Services;
using System.Data;
using Azure.Core;
using DataAccess;
using Test.Context;

namespace Test;
using Dominio;
using Moq;

[TestClass]
public class TaskServiceTest
{
    private Team team;
    private TaskService _taskService;
    private TeamService _teamService;
    private PanelService _panelService;
    private IRepository<Task> _taskRepository;
    private SqlContext _context;
    
    Mock<ITeamService> _mockTeamService;
    Mock<IPanelService> _mockPanelService;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();

        _taskRepository = new TaskDatabaseRepository(_context);
        _taskService = new TaskService(_taskRepository);
        
        _mockTeamService = new Mock<ITeamService>();
        _mockPanelService = new Mock<IPanelService>();
       // taskService = new TaskService(_mockPanelService.Object, _mockTeamService.Object);
        team = new Team
        {
            Name = "Team Example",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre desarrollo",
            MaxUsers = 5,
            MembersCount = 1
        };
    }
    
    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void GetExpiredTasksFromPanels()
    {
        var panel1 = new Panel{Name = "Panel 1"};
        var task1 = new Task{Name = "Task 1", ExpirationDate = DateTime.Now.AddHours(-1)};
        panel1.Tasks.Add(task1);
        team.Panels.Add(panel1);
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        var expiredTasks = taskService.GetAllExpiredTasks(panel1.Id);
        CollectionAssert.AreEquivalent(expiredTasks, new List<Task>{task1});
    }

    [TestMethod]

    public void GetNonExpiredTasksFromPanel()
    {
        var panel1 = new Panel{Name = "Panel 1",Id = 1};
        var task1 = new Task{Name = "Task 1", ExpirationDate = DateTime.Now.AddHours(+1)};
        panel1.Tasks.Add(task1);
        team.Panels.Add(panel1);
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        _mockPanelService.Setup(x => x.GetPanelById(panel1.Id)).Returns(panel1);
        var nonExpiredTasks = taskService.GetNonExpiredTasks(panel1.Id);
        CollectionAssert.AreEquivalent(nonExpiredTasks, new List<Task>{task1});
    }
    
    [TestMethod]
    public void GetTaskByIdTest()
    {
        var panel1 = new Panel{Name = "Panel 1",Id = 1};
        var task1 = new Task{Name = "Task 1", ExpirationDate = DateTime.Now.AddHours(+1)};
        panel1.Tasks.Add(task1);
        team.Panels.Add(panel1);
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        _mockPanelService.Setup(x => x.GetPanelById(panel1.Id)).Returns(panel1);
        var task = taskService.GetTaskById(task1.Id);
        Assert.AreSame(task1, task);
    }

    [TestMethod]
    public void GetPanelIdByTaskTest()
    {
        var panel1 = new Panel{Name = "Panel 1",Id = 1};
        var task1 = new Task{Name = "Task 1", ExpirationDate = DateTime.Now.AddHours(+1)};
        panel1.Tasks.Add(task1);
        team.Panels.Add(panel1);
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        _mockPanelService.Setup(x => x.GetPanelById(panel1.Id)).Returns(panel1);
        _mockPanelService.Setup(x => x.GetAllPanelsFromTeam(team.Name)).Returns(team.Panels);
        var id = taskService.GetPanelIdByTask(team.Name, task1.Id);
        Assert.AreEqual(id, panel1.Id);
    }

    [TestMethod]
    public void AddEffort_PositiveTime()
    {
        var panel1 = new Panel { Name = "Panel 1", Id = 1 };
        var task1 = new Task { Name = "Task 1", ExpirationDate = DateTime.Now.AddHours(+1), InvertedEffort = 0 };
        panel1.Tasks.Add(task1);
        team.Panels.Add(panel1);
        _mockTeamService.Setup(x => x.GetTeamByName(team.Name)).Returns(team);
        _mockPanelService.Setup(x => x.GetPanelById(panel1.Id)).Returns(panel1);

        taskService.AddEffort(task1.Id, 5);

        Assert.AreEqual(5, task1.InvertedEffort);
    }
    
    [TestMethod]
    public void AddEffort_ZeroTime()
    {
        var panel1 = new Panel{Name = "Panel 1",Id = 1};
        var task1 = new Task{Name = "Task 1", ExpirationDate = DateTime.Now.AddHours(+1), InvertedEffort = 10};
        panel1.Tasks.Add(task1);
        team.Panels.Add(panel1);
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        _mockPanelService.Setup(x => x.GetPanelById(panel1.Id)).Returns(panel1);

        taskService.AddEffort(task1.Id, 0);

        Assert.AreEqual(10, task1.InvertedEffort);
    }
    
    [TestMethod]
    public void AddEffort_NegativeTime()
    {
        var panel1 = new Panel{Name = "Panel 1",Id = 1};
        var task1 = new Task{Name = "Task 1", ExpirationDate = DateTime.Now.AddHours(+1), InvertedEffort = 10};
        panel1.Tasks.Add(task1);
        team.Panels.Add(panel1);
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        _mockPanelService.Setup(x => x.GetPanelById(panel1.Id)).Returns(panel1);

        taskService.AddEffort(task1.Id, -5);

        Assert.AreEqual(10, task1.InvertedEffort);
    }
    
    [TestMethod]
    public void AddEffort_AccumulateInvertedEffort()
    {
        var panel1 = new Panel{Name = "Panel 1",Id = 1};
        var task1 = new Task{Name = "Task 1", ExpirationDate = DateTime.Now.AddHours(+1), InvertedEffort = 0};
        panel1.Tasks.Add(task1);
        team.Panels.Add(panel1);
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        _mockPanelService.Setup(x => x.GetPanelById(panel1.Id)).Returns(panel1);

        taskService.AddEffort(task1.Id, 3);
        taskService.AddEffort(task1.Id, 2);

        Assert.AreEqual(5, task1.InvertedEffort);
    }
}