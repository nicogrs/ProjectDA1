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
            MaxUsers = 5,
            Panels = new List<Panel>(),
        };
    }

    [TestMethod]
    public void CreateTeam()
    {
        var isTeamCreated = _service.CreateTeam(team);
        Assert.IsTrue(isTeamCreated);
    }
    
}