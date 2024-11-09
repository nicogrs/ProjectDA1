using Interfaces;
using Services;

namespace Test;
using Dominio;
using Moq;

[TestClass]
public class PanelServiceTest
{
    
    private Team team;
    private PanelService panelService;
    Mock<ITeamService> _mockTeamService;
    
    [TestInitialize]
    public void Setup()
    {
        _mockTeamService = new Mock<ITeamService>();
        panelService = new PanelService(_mockTeamService.Object);
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
    public void AddNewPanel()
    {
        _mockTeamService.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        Panel panelTest = new Panel{Name = "New panel"};
        var isPanelAdded = panelService.AddPanel(team.Name, panelTest);
        Assert.IsTrue(isPanelAdded);
    }
    
    [TestMethod]
    public void RemovePanelTest()
    {
        _mockTeamService.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        Panel panelTest = new Panel{Name = "Panel Test"};
        team.Panels.Add(panelTest);
        panelService.RemovePanel(team.Name, panelTest.Id);
        CollectionAssert.DoesNotContain(team.Panels, panelTest);
    }
    
    [TestMethod]
    
    public void GetPanelById()
    {
        _mockTeamService.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        Panel panelTest = new Panel{Name = "New panel"};
        team.Panels.Add(panelTest);
        var panelFromTeam = panelService.GetPanelById(team.Name, panelTest.Id);
        Assert.AreEqual(panelFromTeam, panelTest);
    }

    [TestMethod]
    public void GetAllPanelsFromTeamTest()
    {
        var panel1 = new Panel{Name = "Panel 1"};
        var panel2 = new Panel{Name = "Panel 2"};
        team.Panels.Add(panel1);
        team.Panels.Add(panel2);
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        var teamPanels = panelService.GetAllPanelsFromTeam(team.Name);
        CollectionAssert.AreEquivalent(teamPanels, new List<Panel>{panel1, panel2});
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void GetAllPanelsFromTeamTestException()
    {
        _mockTeamService.Setup(x=> x.GetTeamByName(team.Name)).Returns(team);
        var teamPanels = panelService.GetAllPanelsFromTeam(team.Name);
    }

}