﻿using System.Data;
using Azure.Core;
using DataAccess;
using Interfaces;
using Services;
using Test.Context;

namespace Test.ServicesTests;
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

    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void UserExists()
    {
        _service.CreateUser(_user2);
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
    
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void CreateUserWithInvalidPassword()
    {
        _user1.Password = "123456";
        _service.CreateUser(_user1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateUserWithInvalidName()
    {
        _user1.Name = "a";
        _service.CreateUser(_user1);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void CreateUserCatchException()
    {
        _user1.Email = null;
        _user1.Password = "";
        _service.CreateUser(_user1);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddExistingUser()
    {
         _service.CreateUser(_user1);
         _service.CreateUser(_user1);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AddInvalidUser()
    {
        _user1.Name = "a";
        var isInvalidUserAdded = _service.CreateUser(_user1);
    }
    
    [TestMethod]
    public void GetUserByEmail()
    {
        _service.CreateUser(_user1);
        var userFromService = _service.GetUserByEmail(_user1.Email);
        Assert.IsNotNull(userFromService);
    }
    
    [TestMethod]
    public void ResetUserPassword()
    {
        _service.CreateUser(_user1);
        var oldPassword = _user1.Password;
        var resetedPassword = _service.ResetUserPassword(_user1.Email);
        Assert.AreNotEqual(oldPassword, resetedPassword);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DataException))]
    public void InvalidUserResetPassword()
    {
        var resetedPassword = _service.ResetUserPassword(_user1.Email);
    }

    [TestMethod]
    public void UpdateUser()
    {
        _service.CreateUser(_user1);
        var updatedUser = new User
        {
            Name = _user1.Name,
            Password = "NuevaPass1$",
            Email = _user1.Email,
            Surname = _user1.Surname,
            BirthDate = _user1.BirthDate
        };
        var isUserUpdated = _service.UpdateUser(updatedUser);
        Assert.IsTrue(isUserUpdated);
    }
    
    [TestMethod]
    public void UpdateInvalidUser()
    {
        _service.CreateUser(_user1);
        var updatedUser = new User
        {
            Name = _user1.Name,
            Password = "a",
            Email = _user1.Email,
            Surname = _user1.Surname,
            BirthDate = _user1.BirthDate
        };
        var isUserUpdated = _service.UpdateUser(updatedUser);
        Assert.IsFalse(isUserUpdated);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void UpdateUserCatchException()
    {
        _service.UpdateUser(_user1);
    }
    
    [TestMethod]
    public void DeleteUser()
    {
        _service.CreateUser(_user1);
        var isUserDeleted = _service.DeleteUser(_user1.Email);
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
        _service.CreateUser(_user1);
        _service.AddElementToPaperBin(_user1.Email,task);
        Assert.AreEqual(_user1.PaperBin.ElementsCount, 1);
    }
    [TestMethod]
    public void DeleteElementFromPaperbinTest()
    {
        Task task = new Task
        {
            Name = "Test",
            Description = "DescriptionTest",
        };
        _service.CreateUser(_user1);
        _service.AddElementToPaperBin(_user1.Email,task);
        _service.DeleteElementFromPaperbin(_user1.Email,task);
        Assert.AreEqual(_user1.PaperBin.ElementsCount, 0);
    }
    
    [TestMethod]
    public void GetDeletedElementsTest()
    {
        Task task = new Task
        {
            Name = "Test",
            Description = "DescriptionTest",
        };
        _service.CreateUser(_user1);
        _service.AddElementToPaperBin(_user1.Email,task);
        var elements = _service.GetDeletedElements(_user1.Email);
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
        _service.CreateUser(_user1);
        _service.AddElementToPaperBin(_user1.Email,task);
        Assert.AreEqual(task.IsDeleted,true);
        _service.RestoreElement(task,_user1.Email);
        Assert.AreEqual(task.IsDeleted,false);
    }

    [TestMethod]
    public void GetAllUsersTest()
    {
        _service.CreateUser(_user1);
        _service.CreateUser(_user2);
        var result = _service.GetAllUsers();
        CollectionAssert.AreEquivalent(result,new List<User>{_user2, _user1});
        
    }
}