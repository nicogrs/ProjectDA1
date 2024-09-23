namespace Test;
using Dominio;
using Moq;

[TestClass]
public class UserListTest
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
            Password = "123456"
        };

    }
    
    [TestMethod]
    public void AddOneUser()
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
    
}