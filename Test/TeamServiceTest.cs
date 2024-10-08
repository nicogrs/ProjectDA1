namespace Test;
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
            MaxUsers = 5
        };
    }

    [TestMethod]
    public void CreateTeamTest()
    {
        team.MembersCount = 1;
        var isTeamCreated = _teamService.CreateTeam(team);
        Assert.IsTrue(isTeamCreated);
    }

    [TestMethod]

    public void AddUserToTeamTest()
    {
        var userEmail = "user@email.com";
        var user = new User { Email = userEmail };
        _mockTeamDatabase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
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
        _mockTeamDatabase.Setup(x => x.GetTeamByName(team.Name) ).Returns(team);
        _mockUserService.Setup(x => x.GetUserByEmail(userEmail)).Returns(user);
        var isUserAdded = _teamService.AddUserToTeam(team.Name, userEmail);
        Assert.IsFalse(isUserAdded);
    }

    [TestMethod]
    public void UpdateTeamTest()
    {
        var team = new Team
        {
            Name = "Team Example",
            MaxUsers = 10,
            TasksDescription = "Tareas sobre facultad"
        };
        var isTeamUpdated = _teamService.UpdateTeam(team);
        Assert.IsTrue(isTeamUpdated);
    }
    
}