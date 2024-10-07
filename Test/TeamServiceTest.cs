namespace Test;
using Dominio;
using Moq;

[TestClass]

public class TeamServiceTest
{
    private Team team;
    private TeamService _service;
    Mock<ITeamDatabase> _mockTeamDatabase;
    
    [TestInitialize]
    public void Setup()
    {
        _mockTeamDatabase = new Mock<ITeamDatabase>();
        _service = new TeamService(_mockTeamDatabase.Object);
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
        var isTeamCreated = _service.CreateTeam(team);
        Assert.IsTrue(isTeamCreated);
    }

    [TestMethod]

    public void AddUserToTeam()
    {
        var userEmail = "user@email.com";
        var isUserAdded = _service.AddUserToTeam(team, userEmail);
        Assert.IsTrue(isUserAdded);
    }
    
}