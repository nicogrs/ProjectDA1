using System.Data;
using DataAccess;
using Interfaces;
using Services;
using Test.Context;

namespace Test;
using Dominio;


[TestClass]
public class UserServiceTest
{
    private UserService _service;
    private User _user;
    private IRepository<User> _userRepository;
    private SqlContext _context;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        
        _userRepository = new UserDatabaseRepository(_context);
        _service = new UserService(_userRepository);
        
        _user = new User        
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
    public void UserExists()
    {
       // _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
        var isUserAdded = _service.UserExists(_user.Email);
        Assert.IsTrue(isUserAdded);
    }

    [TestMethod]
    public void UserNotExists()
    {
        //_mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns((User)null);
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
    public void CreateUserWithInvalidPassword()
    {
        _user.Password = "123456";
        _service.CreateUser(_user);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateUserWithInvalidName()
    {
        _user.Name = "a";
        _service.CreateUser(_user);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void CreateUserCatchException()
    {
       // _mockUserDatabase.Setup(x => x.AddUser(_user)).Throws(new NullReferenceException());
       // _mockUserDatabase.Setup(x => x.GetUserByEmail(null) ).Returns((User)null);
        _user.Email = null;
        _user.Password = "";
        _service.CreateUser(_user);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddExistingUser()
    {
      //  _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
         _service.CreateUser(_user);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddInvalidUser()
    {
        _user.Name = "a";
        var isInvalidUserAdded = _service.CreateUser(_user);
    }
    
    [TestMethod]
    public void GetUserByEmail()
    {
      //  _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
        var userFromService = _service.GetUserByEmail(_user.Email);
        Assert.IsNotNull(userFromService);
    }
    
    [TestMethod]
    public void ResetUserPassword()
    {
      //  _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
        var oldPassword = _user.Password;
        var resetedPassword = _service.ResetUserPassword(_user.Email);
        Assert.AreNotEqual(oldPassword, resetedPassword);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DataException
        ))]
    public void InvalidUserResetPassword()
    {
     //   _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns((User)null);
        var resetedPassword = _service.ResetUserPassword(_user.Email);
    }

    [TestMethod]
    public void UpdateUser()
    {
    //    _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
        var updatedUser = new User
        {
            Name = _user.Name,
            Password = "NuevaPass1$",
            Email = _user.Email,
            Surname = _user.Surname,
            BirthDate = _user.BirthDate
        };
        var isUserUpdated = _service.UpdateUser(updatedUser);
        Assert.IsTrue(isUserUpdated);
    }
    
    [TestMethod]
    public void UpdateInvalidUser()
    {
     //   _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns(_user);
        var updatedUser = new User
        {
            Name = _user.Name,
            Password = "a",
            Email = _user.Email,
            Surname = _user.Surname,
            BirthDate = _user.BirthDate
        };
        var isUserUpdated = _service.UpdateUser(updatedUser);
        Assert.IsFalse(isUserUpdated);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void UpdateUserCatchException()
    {
     //   _mockUserDatabase.Setup(x => x.UpdateUser(_user)).Throws(new InvalidDataException());
        _service.UpdateUser(_user);
    }
    
    
    [TestMethod]
    public void DeleteUser()
    {
   //     _mockUserDatabase.Setup(x => x.DeleteUser(_user.Email)).Returns(true);
        var isUserDeleted = _service.DeleteUser(_user.Email);
        Assert.IsTrue(isUserDeleted );
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteInvalidUser()
    {
   //     _mockUserDatabase.Setup(x => x.GetUserByEmail(_user.Email)).Returns((User)null);
        _service.DeleteUser("");
    }

}