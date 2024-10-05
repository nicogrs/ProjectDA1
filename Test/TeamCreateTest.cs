namespace Test;
using Dominio;

[TestClass]
public class TeamCreateTest
{
    
    [TestMethod]
    public void CreateTeam()
    {
        Team teamTest = new Team
        {
            Name = "Team Example",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre desarrollo",
            MaxUsers = 5,
            Panels = new List<Panel>(),
        };
        Assert.IsNotNull(teamTest);

    }
    
}