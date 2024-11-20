using System.Collections;
using DataAccess;
using Test.Context;
namespace Test.DataAccessTests;
using Dominio;

[TestClass]
public class TeamDatabaseRepositoryTest
{
    private Team team1;
    private Team team2;
    private SqlContext _context;
    private TeamDatabaseRepository _repository;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _repository = new TeamDatabaseRepository(_context);

        team1 = new Team
        {
            Name = "Team 1",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre desarrollo",
            MaxUsers = 5,
            MembersCount = 1
        };
        team2 = new Team
        {
            Name = "Team 2",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre exploracion",
            MaxUsers = 5,
            MembersCount = 1
        };
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Teams.RemoveRange(_context.Teams);
        _context.SaveChanges();
    }

    [TestMethod]
    public void AddTest()
    {
        var addedTeam = _repository.Add(team1);

        Assert.IsNotNull(addedTeam);
        Assert.AreEqual("Team 1", addedTeam.Name);
        Assert.AreEqual(1, _context.Teams.Count());
    }

    [TestMethod]
    public void FindTest()
    {
        _context.Teams.Add(team1);
        _context.SaveChanges();

        var foundTeam = _repository.Find(t => t.Name == team1.Name);
        var notFoundTeam = _repository.Find(t => t.Name == "Team 3");
        
        Assert.IsNull(notFoundTeam);
        Assert.IsNotNull(foundTeam);
        Assert.AreEqual("Team 1", foundTeam.Name);
    }

    [TestMethod]
    public void UpdateTest()
    {
        _context.Teams.Add(team1);
        _context.SaveChanges();

        var updatedTeam = new Team
        {
            Id = team1.Id,
            Name = "Team 11",
            MembersCount = 7,
            MaxUsers = 15,
            TasksDescription = "Tareas",
            Panels = new List<Panel>(),
            TeamMembers = new List<User>()
        };

        // Act
        var result = _repository.Update(updatedTeam);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Team 11", result.Name);
        Assert.AreEqual(7, result.MembersCount);
       
    }

    [TestMethod]
    public void DeleteTest()
    {
        _context.Teams.Add(team1);
        _context.SaveChanges();

        _repository.Delete(team1.Id);

        Assert.AreEqual(0, _context.Teams.Count());
    }

    [TestMethod]
    public void FindAllTest()
    {
        _context.Teams.Add(team1);
        _context.Teams.Add(team2);
        _context.SaveChanges();

        var allTeams = _repository.FindAll();

        Assert.AreEqual(2, allTeams.Count);
        CollectionAssert.Contains((ICollection)allTeams, team1);
        CollectionAssert.Contains((ICollection)allTeams, team2);
    }
}