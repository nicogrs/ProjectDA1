using Dominio.Data;

namespace Test;
using Dominio;

[TestClass]
public class TeamDatabaseTest
{
    private Team team;
    private TeamDataBase teamDb;

    [TestInitialize]
    public void Setup()
    {
        teamDb = new TeamDataBase();
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
    public void GetTeamByNameTest()
    {
        teamDb.Teams.Add(team);
        var teamTest = teamDb.GetTeamByName(team.Name);
        Assert.AreEqual(team, teamTest);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void GetTeamByNameTestException()
    {
        var teamTest = teamDb.GetTeamByName(team.Name);
    }

    [TestMethod]

    public void AddTeamTest()
    {
        teamDb.AddTeamToDatabase(team);
        CollectionAssert.Contains(teamDb.Teams, team);

    }

    [TestMethod]
    public void RemoveTeamFromDatabaseTest()
    {
        teamDb.AddTeamToDatabase(team);
        teamDb.RemoveTeamFromDatabase(team.Name);
        CollectionAssert.DoesNotContain(teamDb.Teams, team);
    }

    [TestMethod]

    public void UpdateTeamInDatabaseTest()
    {
        teamDb.AddTeamToDatabase(team);
        var teamToUpdate = new Team
        {
            Name = "Team Example",
            MaxUsers = 10,
            TasksDescription = "Nueva descripcion",
            CreatedOn = new DateTime(2020, 05, 05),
            MembersCount = 1
        };
        teamDb.UpdateTeamInDatabase(team);
    }

    [TestMethod]
    public void GetTeamsTest()
    {
        var team1 = new Team{Name = "Team 1"};
        var team2 = new Team{Name = "Team 2"};
        teamDb.AddTeamToDatabase(team1);
        teamDb.AddTeamToDatabase(team2);
        var teams = teamDb.GetTeams();
        CollectionAssert.AreEquivalent(teams, new List<Team>{team1, team2});
    }
    
    [TestMethod]
    public void GetTeamsByUserEmailTest()
    {
        var team1 = new Team{Name = "Team 1"};
        var team2 = new Team{Name = "Team 2"};
        teamDb.AddTeamToDatabase(team1);
        teamDb.AddTeamToDatabase(team2);
        var user1 = new User{Email = "user1@example.com"};
        team1.TeamMembers.Add(user1);
        team2.TeamMembers.Add(user1);
        var teams = teamDb.GetTeamsByUserEmail(user1.Email);
        CollectionAssert.AreEquivalent(teams, new List<Team>{team1, team2});
    }
}