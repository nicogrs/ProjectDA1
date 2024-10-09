﻿namespace Test;
using Dominio;
using Moq;

[TestClass]

public class TeamServiceTest
{
    private Team team;
    private TeamService _teamService;
    Mock<IUserService> _mockUserService;
    Mock<ITeamDatabase> _mockTeamDatabase;
    
    [TestInitialize]
    public void Setup()
    {
        _mockTeamDatabase = new Mock<ITeamDatabase>();
        _mockUserService = new Mock<IUserService>();
        _teamService = new TeamService(_mockTeamDatabase.Object, _mockUserService.Object);
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
        _mockTeamDatabase.Setup(x => x.GetTeams()).Returns(() => new List<Team>());
        var isTeamCreated = _teamService.CreateTeam(team);
        Assert.IsTrue(isTeamCreated);
    }

    [TestMethod]

    public void AddUserToTeamTest()
    {
        var userEmail = "user@email.com";
        var user = new User { Email = userEmail };
        _mockTeamDatabase.Setup(x => x.GetTeams()).Returns(() => new List<Team> { team });
        _mockTeamDatabase.Setup(x => x.GetTeamByName(team.Name)).Returns(team);
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
        _mockTeamDatabase.Setup(x => x.GetTeams()).Returns(() => new List<Team> { team });
        _mockTeamDatabase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
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
        _mockTeamDatabase.Setup(x => x.GetTeams()).Returns(() => new List<Team> { team });
        _mockTeamDatabase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        _mockUserService.Setup(x => x.GetUserByEmail(userEmail)).Returns(user);
        var isUserDeleted = _teamService.RemoveUserFromTeam(team.Name, userEmail);
        Assert.IsTrue(isUserDeleted);
    }

    [TestMethod]
    public void RemoveUserThatNotExists()
    {
        var userEmail = "user@email.com";
        _mockTeamDatabase.Setup(x => x.GetTeams()).Returns(() => new List<Team> { team });
        _mockTeamDatabase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        _mockUserService.Setup(x => x.GetUserByEmail(userEmail)).Returns((User)null);
        var isUserDeleted = _teamService.RemoveUserFromTeam(team.Name, userEmail);
        Assert.IsFalse(isUserDeleted);
    }
    
    [TestMethod]
    public void UpdateTeamTest()
    {
        _mockTeamDatabase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
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

    public void AddNewPanel()
    {
        _mockTeamDatabase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        Panel panelTest = new Panel{Name = "New panel"};
        var isPanelAdded = _teamService.AddPanel(team.Name, panelTest);
        Assert.IsTrue(isPanelAdded);
    }
    
    
}