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
    public void GetUserByEmail()
    {
        db.AddUser(_user);
        var user = db.GetUserByEmail(_user.Email);
        Assert.AreEqual(user, _user);
    }
    
    
    [TestMethod]
    public void UpdateUser()
    {
        db.AddUser(_user);
        var user = db.GetUserByEmail(_user.Email);
        var newUser = new User
        {
            Email = _user.Email,
            Surname = _user.Surname,
            Name = "Nuevo Nombre",
            BirthDate = _user.BirthDate,
            Admin = _user.Admin,
            Password = _user.Password,
        };
        db.UpdateUser(newUser);
        var userFromDb = db.GetUserByEmail(_user.Email);
        Assert.AreEqual(newUser.Name, userFromDb.Name);
    }
    
    [TestMethod]
    public void DeleteUserFromDataBase()
    {
        db.AddUser(_user);
        var user = db.GetUserByEmail(_user.Email);
        var userExists = user != null;
        var result = db.DeleteUser(_user.Email);
        Assert.IsTrue(userExists && result);
    }
    
}