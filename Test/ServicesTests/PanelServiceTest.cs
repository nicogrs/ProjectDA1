using DataAccess;
using Interfaces;
using Services;
using Test.Context;
using Dominio;
using Task = Dominio.Task;

namespace Test;


[TestClass]
public class PanelServiceTest
{
    
    private Team team;
    private PanelService panelService;
    private SqlContext _context;
    private IRepository<Panel> _panelDatabase;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _panelDatabase = new PanelDatabaseRepository(_context);
        panelService = new PanelService(_panelDatabase);
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
    public void AddNewPanel()
    {
        Panel panelTest = new Panel{Name = "New panel"};
        var isPanelAdded = panelService.AddPanel(panelTest);
        Assert.IsTrue(isPanelAdded);
    }
    
    [TestMethod]
    public void RemovePanelTest()
    {
        Panel panelTest = new Panel{Name = "Panel Test"};
        panelService.AddPanel(panelTest);
        var panelRemoved = panelService.RemovePanel(panelTest.Id);
        var panel = panelService.GetPanelById(panelTest.Id);
        Assert.AreEqual(panelRemoved,panelTest);
        Assert.IsNull(panel);
    }
    [TestMethod]
    public void RemovePanelNullTest()
    {
        var result = panelService.RemovePanel(3);
        Assert.IsNull(result);
    }
    
    [TestMethod]
    
    public void GetPanelById()
    {
        Panel panelTest = new Panel{Name = "New panel"};
        panelService.AddPanel(panelTest);
        var panelFromTeam = panelService.GetPanelById(panelTest.Id);
        Assert.AreEqual(panelFromTeam, panelTest);
    }

    [TestMethod]
    public void GetAllPanelsFromTeamTest()
    {
        var panel1 = new Panel { Name = "Panel 1", Team = team.Name };
        var panel2 = new Panel { Name = "Panel 2", Team = team.Name };
        panelService.AddPanel(panel1);
        panelService.AddPanel(panel2);
        var teamPanels = panelService.GetAllPanelsFromTeam(team.Name);
        CollectionAssert.AreEquivalent(teamPanels, new List<Panel>{panel1, panel2});
    }

    [TestMethod]
    public void AddTaskToPanel()
    {
        Panel panelTest = new Panel{Name = "Panel Test"};
        panelService.AddPanel(panelTest);
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpirationDate = DateTime.Now.AddHours(+1),
        };
        panelService.AddTaskToPanel(panelTest.Id, task1);
        CollectionAssert.Contains(panelTest.Tasks, task1);
    }
    
    [TestMethod]
    public void RemoveTaskFromPanel()
    {
        Panel panelTest = new Panel{Name = "Panel Test"};
        panelService.AddPanel(panelTest);
        var task1 = new Task
        {
            Name = "Task 1",
            Description = "description",
            ExpirationDate = DateTime.Now.AddHours(+1),
        };
        panelService.AddTaskToPanel(panelTest.Id, task1);
        panelService.RemoveTaskFromPanel(panelTest.Id, task1);
        CollectionAssert.DoesNotContain(panelTest.Tasks, task1);
    }
    
    
    [TestMethod]
    public void GetAllPanelsFromTeamTest_NoPanels()
    {
        var teamPanels = panelService.GetAllPanelsFromTeam(team.Name);
        Assert.AreEqual(0, teamPanels.Count);
    }

}