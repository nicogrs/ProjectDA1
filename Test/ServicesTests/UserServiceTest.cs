using System.Data;
using Azure.Core;
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
    private User _user1;
    private User _user2;
    private IRepository<User> _userRepository;
    private SqlContext _context;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        
        _userRepository = new UserDatabaseRepository(_context);
        _service = new UserService(_userRepository);
        
        _user1 = new User        
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
        };
        
        _user2 = new User        
        {
            Name = "Paco",
            Surname = "Lopez",
            Email = "paco@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
        };
        _userRepository.Add(_user2);

    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void UserExists()
    {
        var isUserAdded = _service.UserExists(_user2.Email);
        Assert.IsTrue(isUserAdded);
    }

    [TestMethod]
    public void UserNotExists()
    {
        var isUserDeleted = _service.UserExists(_user1.Email);
        Assert.IsFalse(isUserDeleted);
    }

    [TestMethod]
    public void AddUser()
    {
        var isUserAdded = _service.CreateUser(_user1);
        Assert.IsTrue(isUserAdded);
    }
    
    /*[TestMethod]
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
        _user.Email = null;
        _user.Password = "";
        _service.CreateUser(_user);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddExistingUser()
    {
         _service.CreateUser(_user);
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
        _service.CreateUser(_user);
        var userFromService = _service.GetUserByEmail(_user.Email);
        Assert.IsNotNull(userFromService);
    }
    
    [TestMethod]
    public void ResetUserPassword()
    {
        _service.CreateUser(_user);
        var oldPassword = _user.Password;
        var resetedPassword = _service.ResetUserPassword(_user.Email);
        Assert.AreNotEqual(oldPassword, resetedPassword);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DataException))]
    public void InvalidUserResetPassword()
    {
        var resetedPassword = _service.ResetUserPassword(_user.Email);
    }

    [TestMethod]
    public void UpdateUser()
    {
        _service.CreateUser(_user);
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
        _service.CreateUser(_user);
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
        _service.UpdateUser(_user);
    }
    
    [TestMethod]
    public void DeleteUser()
    {
        _service.CreateUser(_user);
        var isUserDeleted = _service.DeleteUser(_user.Email);
        Assert.IsTrue(isUserDeleted );
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteInvalidUser()
    {
        _service.DeleteUser("");
    }

    [TestMethod]
    public void AddElementToPaperBinTest()
    {
        Task task = new Task
        {
            Name = "Test",
            Description = "DescriptionTest",
        };
        _service.CreateUser(_user);
        _service.AddElementToPaperBin(_user.Email,task);
        Assert.AreEqual(_user.PaperBin.ElementsCount, 1);
    }
    [TestMethod]
    public void DeleteElementFromPaperbinTest()
    {
        Task task = new Task
        {
            Name = "Test",
            Description = "DescriptionTest",
        };
        _service.CreateUser(_user);
        _service.AddElementToPaperBin(_user.Email,task);
        _service.DeleteElementFromPaperbin(_user.Email,task);
        Assert.AreEqual(_user.PaperBin.ElementsCount, 0);
    }
    
    [TestMethod]
    public void GetDeletedElementsTest()
    {
        Task task = new Task
        {
            Name = "Test",
            Description = "DescriptionTest",
        };
        _service.CreateUser(_user);
        _service.AddElementToPaperBin(_user.Email,task);
        var elements = _service.GetDeletedElements(_user.Email);
        CollectionAssert.Contains(elements,task);
    }
    
    [TestMethod]
    public void RestoreElementTest()
    {
        Task task = new Task
        {
            Name = "Test",
            Description = "DescriptionTest",
        };
        _service.CreateUser(_user);
        _service.AddElementToPaperBin(_user.Email,task);
        Assert.AreEqual(task.IsDeleted,true);
        _service.RestoreElement(task,_user.Email);
        Assert.AreEqual(task.IsDeleted,false);
    }*/
    
}