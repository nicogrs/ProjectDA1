namespace Test.ModelsTests;
using Dominio;

[TestClass]
public class UserTest
{
    private User u;
    
    [TestInitialize]
    public void Setup()
    {
     u  = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1",
            Admin = false,
            UserTeams = new List<Team>()
        };
    }
    
    [TestMethod]
    public void ValidName()
    {
       var isValidName = u.IsNameValid();
       Assert.IsTrue(isValidName);
    }

    [TestMethod]
    public void ValidSurname()
    {
        var isValidSurname = u.IsSurnameValid();
        Assert.IsTrue(isValidSurname);
    }
    
    [TestMethod]
    public void ValidEmail()
    {
        var isValidEmail = u.IsEmailValid();
        Assert.IsTrue(isValidEmail);
    }

    [TestMethod]
    public void ValidBirthDate()
    {
        var isValidBirthDate = u.IsBirthDateValid();
        Assert.IsTrue(isValidBirthDate);
    }

    [TestMethod]
    public void ValidUser()
    {
        var isUserValid = u.IsUserValid();
        Assert.IsTrue(isUserValid);
    }
    

    [TestMethod]
    public void UserAdmin()
    {
        u.Admin = true;
        var isUserAdmin = u.IsUserAdmin();
        Assert.IsTrue(isUserAdmin);
    }

    [TestMethod]
    public void UserTeamsTest()
    {
        Team t = new Team();
        
        u.UserTeams.Add(t);
        
        Assert.AreEqual(u.UserTeams.Count,1);
        Assert.AreSame(u.UserTeams[0],t);
    }
        
}