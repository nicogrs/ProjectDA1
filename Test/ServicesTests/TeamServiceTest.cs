using DataAccess;
using Services;
using Test.Context;

namespace Test;
using Dominio;
using Moq;
using Interfaces;

[TestClass]
public class TeamServiceTest
{
    private Team team;
    private TeamService _teamService;
    Mock<IUserService> _mockUserService;
    private IRepository<Team> _teamRepository;
    private SqlContext _context;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        
        _teamRepository = new TeamDatabaseRepository(_context);
        _mockUserService = new Mock<IUserService>();
        _teamService = new TeamService(_teamRepository , _mockUserService.Object);
        team = new Team
        {
            Name = "Team Example",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre desarrollo",
            MaxUsers = 5,
            MembersCount = 1
        };
    }
    
    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void CreateTeamTest()
    {
        var isTeamCreated = _teamService.CreateTeam(team);
        Assert.IsTrue(isTeamCreated);
    }
    
    [TestMethod]
    public void CreateTeamAlreadyExists()
    {
      //  _mockTeamDataBase.Setup(x => x.GetTeams()).Returns(new List<Team>() {team});
        _teamService.CreateTeam(team);
        var isTeamCreated = _teamService.CreateTeam(team);
        Assert.IsFalse(isTeamCreated);
    }


    [TestMethod]

    public void AddUserToTeamTest()
    {
        _teamService.CreateTeam(team);
        var user = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
        };
        _mockUserService.Setup(x => x.GetUserByEmail(user.Email)).Returns(user);
        var isUserAdded = _teamService.AddUserToTeam(team.Name, user.Email);
        Assert.IsTrue(isUserAdded);
    }

    [TestMethod]
    public void UserInTeamAlreadyExists()
    {
        var user = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
        };
        _teamService.CreateTeam(team);
        _mockUserService.Setup(x => x.GetUserByEmail(user.Email)).Returns(user);
        _teamService.AddUserToTeam(team.Name, user.Email);
        var isUserAdded = _teamService.AddUserToTeam(team.Name, user.Email);
        Assert.IsFalse(isUserAdded);
    }
    [TestMethod]
    public void RemoveUserFromTeam()
    {
        _teamService.CreateTeam(team);
        var user = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
        };
        _mockUserService.Setup(x => x.GetUserByEmail(user.Email)).Returns(user);
        _teamService.AddUserToTeam(team.Name, user.Email);
        var isUserDeleted = _teamService.RemoveUserFromTeam(team.Name, user.Email);
        Assert.IsTrue(isUserDeleted);
    }

    [TestMethod]
    public void RemoveUserThatNotExists()
    {
        _teamService.CreateTeam(team);
        var userEmail = "user@email.com";
        _mockUserService.Setup(x => x.GetUserByEmail(userEmail)).Returns((User)null);
        var isUserDeleted = _teamService.RemoveUserFromTeam(team.Name, userEmail);
        Assert.IsFalse(isUserDeleted);
    }
    
    [TestMethod]
    public void UpdateTeamTest()
    {
        _teamService.CreateTeam(team);
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
    //    _mockTeamDataBase.Setup(x => x.GetTeams()).Returns(() => new List<Team> { team });
        _mockUserService.Setup(x => x.GetUserByEmail(userEmail)).Returns(user);
        var isUserRemoved = _teamService.RemoveUserFromAllTeams(userEmail);
         Assert.IsTrue(isUserRemoved);
        
    }
    
    
    [TestMethod]
    public void GetAllTeamsTest()
    {
        Team team1 = new Team {             
            Name = "Team Example",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre desarrollo",
            MaxUsers = 5,
            MembersCount = 1
        };
        Team team2 = new Team {            
            Name = "Team Example2",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre economia",
            MaxUsers = 7,
            MembersCount = 1
        };
        _teamService.CreateTeam(team1);
        _teamService.CreateTeam(team2);
        var allTeams = _teamService.GetAllTeams();
        CollectionAssert.AreEquivalent(allTeams, new List<Team> {team1, team2 });
    }


    [TestMethod]
    public void GetTeamsByUserEmailTest()
    {
        Team team1 = new Team { Name = "Team 1" };
        Team team2 = new Team { Name = "Team 2" };
        var user = new User { Email = "user@email.com" };
        team1.TeamMembers.Add(user);
        team2.TeamMembers.Add(user);
        var teamsByUserEmail = _teamService.GetTeamsByUserEmail(user.Email);
        CollectionAssert.AreEquivalent(teamsByUserEmail, new List<Team> { team1, team2 });
    }
    
    [TestMethod]
    public void DeleteTeamOk()
    {
        Team team1 = new Team { Name = "Team 1" };
        Team team2 = new Team { Name = "Team 2" };
        _teamService.CreateTeam(team1);
        _teamService.CreateTeam(team2);
        _teamService.DeleteTeam(team1.Name);
        var allTeams = _teamService.GetAllTeams();
        CollectionAssert.AreEquivalent(allTeams, new List<Team> {team1, team2 });
    }

    
}