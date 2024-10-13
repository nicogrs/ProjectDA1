namespace Test;
using Dominio;
using Moq;

[TestClass]
public class TaskServiceTest
{
    
    private Team team;
    private TaskService taskService;
    Mock<ITeamService> _mockTeamService;
    Mock<IPanelService> _mockPanelService;
    
    [TestInitialize]
    public void Setup()
    {
        _mockTeamService = new Mock<ITeamService>();
        _mockPanelService = new Mock<IPanelService>();
        taskService = new TaskService(_mockPanelService.Object, _mockTeamService.Object);
        team = new Team
        {
            Name = "Team Example",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre desarrollo",
            MaxUsers = 5,
            MembersCount = 1
        };
    }
    
    [TestMethod]
    public void GetExpiredTasksFromPanels()
    {
        var panel1 = new Panel{Name = "Panel 1"};
        var task1 = new Task{Title = "Task 1", ExpirationDate = DateTime.Now.AddHours(-1)};
        panel1.Tasks.Add(task1);
        team.Panels.Add(panel1);
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        var expiredTasks = taskService.GetAllExpiredTasks(team.Name);
        CollectionAssert.AreEquivalent(expiredTasks, new List<Task>{task1});
    }

    [TestMethod]

    public void GetNonExpiredTasksFromPanel()
    {
        var panel1 = new Panel{Name = "Panel 1",PanelId = 1};
        var task1 = new Task{Title = "Task 1", ExpirationDate = DateTime.Now.AddHours(+1)};
        panel1.Tasks.Add(task1);
        team.Panels.Add(panel1);
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        _mockPanelService.Setup(x => x.GetPanelById(team.Name, panel1.PanelId)).Returns(panel1);
        var nonExpiredTasks = taskService.GetNonExpiredTasks(team.Name, panel1.PanelId);
        CollectionAssert.AreEquivalent(nonExpiredTasks, new List<Task>{task1});
    }
}