namespace Test;
using Dominio;

[TestClass]

public class TeamServiceTest
{
    private Team team;
    
    [TestInitialize]
    public void Setup()
    {
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
        
    }
    
}