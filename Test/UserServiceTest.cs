namespace Test;
using Dominio;
using Moq;

[TestClass]
public class UserServiceTest
{
    private UserService _service;
    private User _user;
    Mock<IUserDatabase> _mockUserDatabase;
    
    [TestInitialize]
    public void Setup()
    {
        _mockUserDatabase = new Mock<IUserDatabase>();
        _service = new UserService(_mockUserDatabase.Object);
        _user = new User        
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
        };

    }
    
    [TestMethod]
    public void UserExists()
    {
        _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
        var isUserAdded = _service.UserExists(_user.Email);
        Assert.IsTrue(isUserAdded);
    }

    [TestMethod]
    public void UserNotExists()
    {
        _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns((User)null);
        var isUserDeleted = _service.UserExists(_user.Email);
        Assert.IsFalse(isUserDeleted);
    }

    [TestMethod]
    public void AddUser()
    {
        var isUserAdded = _service.CreateUser(_user);
        Assert.IsTrue(isUserAdded);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddExistingUser()
    {
        _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
         _service.CreateUser(_user);
    }
    
    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void AddInvalidUser()
    {
        _user.Name = "a";
        var isInvalidUserAdded = _service.CreateUser(_user);
        Assert.IsFalse(isInvalidUserAdded);
    }

    [TestMethod]
    public void DeleteUser()
    {
     _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
     _mockUserDatabase.Setup(x => x.DeleteUser(_user.Email)).Returns(true);
     var isUserDeleted = _service.DeleteUser(_user.Email);
     Assert.IsTrue(isUserDeleted );
    }

    [TestMethod]
    public void GetUserByEmail()
    {
        _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
        var userFromService = _service.GetUserByEmail(_user.Email);
        Assert.IsNotNull(userFromService);
    }
    
    [TestMethod]
    public void ResetUserPassword()
    {
        _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
        var oldPassword = _user.Password;
        var resetedPassword = _service.ResetUserPassword(_user.Email);
        Assert.AreNotEqual(oldPassword, resetedPassword);
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteInvalidUser()
    {
        _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns((User)null);
        _service.DeleteUser("");
    }

}