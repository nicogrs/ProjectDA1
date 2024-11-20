using DataAccess;
using Services;
using Test.Context;

namespace Test.ServicesTests;
using Dominio;
using Interfaces;

[TestClass]
public class TeamServiceTest
{
    private Team team;
    private User user;
    private TeamService _teamService;
    private UserService _userService;
    private IRepository<Team> _teamRepository;
    private SqlContext _context;
    private IRepository<User> _userRepository;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        
        _teamRepository = new TeamDatabaseRepository(_context);
        _userRepository = new UserDatabaseRepository(_context);
        _userService = new UserService(_userRepository);
        _teamService = new TeamService(_teamRepository , _userService);
        team = new Team
        {
            Name = "Team Example",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre desarrollo",
            MaxUsers = 5,
            MembersCount = 1
        };
        user = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
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
        _userService.CreateUser(user);
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
        _userService.CreateUser(user);
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
        _userService.CreateUser(user);
        _teamService.AddUserToTeam(team.Name, user.Email);
        var isUserDeleted = _teamService.RemoveUserFromTeam(team.Name, user.Email);
        Assert.IsTrue(isUserDeleted);
    }

    [TestMethod]
    public void RemoveUserThatNotExists()
    {
        _teamService.CreateTeam(team);
        var userEmail = "user@email.com";
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
        User user1 = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = userEmail,
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
        };
        
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
        _userService.CreateUser(user1);
        team1.TeamMembers.Add(user1);
        team2.TeamMembers.Add(user1);
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
        User user1 = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
        };
        _teamService.CreateTeam(team1);
        _teamService.CreateTeam(team2);
        team1.TeamMembers.Add(user1);
        team2.TeamMembers.Add(user1);
        var teamsByUserEmail = _teamService.GetTeamsByUserEmail(user1.Email);
        CollectionAssert.AreEquivalent(teamsByUserEmail, new List<Team> { team1, team2 });
    }
    
    [TestMethod]
    public void DeleteTeamOk()
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
        _teamService.DeleteTeam(team1.Name);
        var allTeams = _teamService.GetAllTeams();
        CollectionAssert.AreEquivalent(allTeams, new List<Team> {team2 });
        
    }

    [TestMethod]
    public void AddPanelToTeam()
    {
        Team team1 = new Team {             
            Name = "Team Example",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre desarrollo",
            MaxUsers = 5,
            MembersCount = 1
        };
        _teamService.CreateTeam(team1);
        Panel p = new Panel
        {
            Name = "Panel Example",
            Description = "Tareas sobre desarrollo",
            CreatedBy = user
        };
        _teamService.AddPanelToTeam(p, team1.Name);
        CollectionAssert.Contains(team1.Panels, p);
    }
    [TestMethod]
    public void RemovePanelFromTeam()
    {
        Team team1 = new Team {             
            Name = "Team Example",
            CreatedOn = new DateTime(2020, 05, 05),
            TasksDescription = "Tareas sobre desarrollo",
            MaxUsers = 5,
            MembersCount = 1
        };
        _teamService.CreateTeam(team1);
        Panel p = new Panel
        {
            Name = "Panel Example",
            Description = "Tareas sobre desarrollo",
            CreatedBy = user
        };
        _teamService.RemovePanelFromTeam(p, team1.Name);
        CollectionAssert.DoesNotContain(team1.Panels, p);
    }
    
}