namespace Test;
using Dominio;
using Moq;

[TestClass]

public class TeamServiceTest
{
    private Team team;
    private TeamService _teamService;
    Mock<IUserService> _mockUserService;
    Mock<ITeamDataBase> _mockTeamDataBase;
    
    [TestInitialize]
    public void Setup()
    {
        _mockTeamDataBase = new Mock<ITeamDataBase>();
        _mockUserService = new Mock<IUserService>();
        _teamService = new TeamService(_mockTeamDataBase.Object, _mockUserService.Object);
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
    public void CreateTeamTest()
    {
        _mockTeamDataBase.Setup(x => x.GetTeams()).Returns( new List<Team>());
        var isTeamCreated = _teamService.CreateTeam(team);
        Assert.IsTrue(isTeamCreated);
    }
    
    [TestMethod]
    public void CreateTeamAlreadyExists()
    {
        _mockTeamDataBase.Setup(x => x.GetTeams()).Returns(new List<Team>() {team});
        var isTeamCreated = _teamService.CreateTeam(team);
        Assert.IsFalse(isTeamCreated);
    }


    [TestMethod]

    public void AddUserToTeamTest()
    {
        var userEmail = "user@email.com";
        var user = new User { Email = userEmail };
        _mockTeamDataBase.Setup(x => x.GetTeams()).Returns(() => new List<Team> { team });
        _mockTeamDataBase.Setup(x => x.GetTeamByName(team.Name)).Returns(team);
        _mockUserService.Setup(x => x.GetUserByEmail(userEmail)).Returns(user);
        var isUserAdded = _teamService.AddUserToTeam(team.Name, userEmail);
        Assert.IsTrue(isUserAdded);
    }

    [TestMethod]
    public void UserInTeamAlreadyExists()
    {
        var userEmail = "user@email.com";
        var user = new User { Email = userEmail };
        team.TeamMembers.Add(user);
        _mockTeamDataBase.Setup(x => x.GetTeams()).Returns(() => new List<Team> { team });
        _mockTeamDataBase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        _mockUserService.Setup(x => x.GetUserByEmail(userEmail)).Returns(user);
        var isUserAdded = _teamService.AddUserToTeam(team.Name, userEmail);
        Assert.IsFalse(isUserAdded);
    }
    [TestMethod]
    public void RemoveUserFromTeam()
    {
        var userEmail = "user@email.com";
        var user = new User { Email = userEmail };
        team.TeamMembers.Add(user);
        _mockTeamDataBase.Setup(x => x.GetTeams()).Returns(new List<Team> { team });
        _mockTeamDataBase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        _mockUserService.Setup(x => x.GetUserByEmail(userEmail)).Returns(user);
        var isUserDeleted = _teamService.RemoveUserFromTeam(team.Name, userEmail);
        Assert.IsTrue(isUserDeleted);
    }

    [TestMethod]
    public void RemoveUserThatNotExists()
    {
        var userEmail = "user@email.com";
        _mockTeamDataBase.Setup(x => x.GetTeams()).Returns(new List<Team> { team });
        _mockTeamDataBase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        _mockUserService.Setup(x => x.GetUserByEmail(userEmail)).Returns((User)null);
        var isUserDeleted = _teamService.RemoveUserFromTeam(team.Name, userEmail);
        Assert.IsFalse(isUserDeleted);
    }
    
    [TestMethod]
    public void UpdateTeamTest()
    {
        _mockTeamDataBase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        var teamTest = new Team
        {
            Name = "New name",
            MaxUsers = 10,
            TasksDescription = "Tareas sobre facultad"
        };
        var isTeamUpdated = _teamService.UpdateTeam(team.Name, teamTest);
        Assert.IsTrue(isTeamUpdated);
    }

    [TestMethod]
    public void RemoveUserFromAllTeams()
    {
        var userEmail = "user@email.com";
        var user = new User { Email = userEmail };
        team.TeamMembers.Add(user);
        _mockTeamDataBase.Setup(x => x.GetTeams()).Returns(() => new List<Team> { team });
        _mockUserService.Setup(x => x.GetUserByEmail(userEmail)).Returns(user);
        var isUserRemoved = _teamService.RemoveUserFromAllTeams(userEmail);
         Assert.IsTrue(isUserRemoved);
        
    }

    [TestMethod]

    public void AddNewPanel()
    {
        _mockTeamDataBase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        Panel panelTest = new Panel{Name = "New panel"};
        var isPanelAdded = _teamService.AddPanel(team.Name, panelTest);
        Assert.IsTrue(isPanelAdded);
    }
    
    [TestMethod]

    public void GetPanelByNAme()
    {
        _mockTeamDataBase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        Panel panelTest = new Panel{Name = "New panel"};
        team.Panels.Add(panelTest);
        var panelFromTeam = _teamService.GetPanelById(team.Name, panelTest.Id);
        Assert.AreEqual(panelFromTeam, panelTest);
    }
    
    
}