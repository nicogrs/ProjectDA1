namespace Test;
using Dominio;
using Dominio.Data;

[TestClass]
public class UserDatabaseTest
{
    private UserDataBase db;
    private User _user;
    
    [TestInitialize]
    
    public void Setup()
    {
        db = new UserDataBase();
        _user = new User        
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "123456"
        };
    }

    [TestMethod]
    public void AddUserToDataBase()
    {
        db.AddUser(_user);
        var result = db.GetUserByEmail(_user.Email);
        Assert.AreSame(_user.Email, result.Email);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void UpdateInvalidUser()
    {
        var getUser = db.GetUserByEmail(_user.Email);
        db.AddUser(_user);
    } 
    
    public void DeleteUserFromDataBase()
    {
        db.AddUser(_user);
        var user = db.GetUserByEmail(_user.Email);
        var userExists = user != null;
        var result = db.DeleteUser(_user.Email);
        Assert.IsTrue(userExists && result);
    }
    
}