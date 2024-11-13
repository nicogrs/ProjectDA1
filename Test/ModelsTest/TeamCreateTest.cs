namespace Test;
using Dominio;

[TestClass]
public class TeamCreateTest
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
            TeamMembers  = new List<User>()
            {
                new User()
            },
            MembersCount = 1
        };
    }
    
    [TestMethod]
    public void ConstructorTest()
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

    [TestMethod]

    public void HasRightName()
    {
        var name = "Team Example";
        Assert.AreEqual(team.Name, name);
    }

    [TestMethod]
    public void HasRightCreatedOn()
    {
        var createdOn = new DateTime(2020, 05, 05);
        Assert.AreEqual(team.CreatedOn, createdOn);
    }

    [TestMethod]
    public void HasRigthTasksDescription()
    {
        var description = "Tareas sobre desarrollo";
        Assert.AreEqual(team.TasksDescription, description);
    }

    [TestMethod]
    public void HasRightMaxUsers()
    {
        var maxUsers = 5;
        Assert.AreEqual(team.MaxUsers, maxUsers);
    }

    [TestMethod]
    public void HasRightPanels()
    {
        var panelsNotNull = team.Panels != null;
        Assert.IsTrue(panelsNotNull);
    }

    [TestMethod]
    public void TeamValidationFalse()
    {
        Assert.IsTrue(team.TeamValidation());
        
        team.Name = "";
        
        Assert.IsFalse(team.TeamValidation());
    }
}