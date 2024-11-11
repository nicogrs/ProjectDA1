using DataAccess;
using Interfaces;
using Test.Context;

namespace Test;
using Dominio;
using Moq;

[TestClass]
public class TrashTest
{
    Mock<IUserDatabase> mockUserDatabase;
    private IUserService _userService;
    private User _user;
    [TestInitialize]
    public void Setup()
    {

        
        mockUserDatabase = new Mock<IUserDatabase>();
        //_userService = new UserService(mockUserDatabase.Object);
        _user = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1",
            PaperBin = new Trash(),
            Admin = false
        };
    }
    
    [TestMethod]
    public void AddElementToPaperBin()
    {
        Task taskTest = new Task{Name = "Task 1"};
        _user.PaperBin.AddElementToPaperbin(taskTest);
        Assert.AreEqual(_user.PaperBin.ElementsCount, 1);
    }
    
    [TestMethod]
    public void RemoveElementFromPaperBin()
    {
        Task taskTest = new Task{Name = "Task 1"};
        _user.PaperBin.AddElementToPaperbin(taskTest);
        _user.PaperBin.DeleteElementFromPaperbin(taskTest);
        Assert.AreEqual(_user.PaperBin.ElementsCount, 0);
    }
    
    [TestMethod]
    public void RestoreElementFromPaperBin()
    {
        Task taskTest = new Task{Name = "Task 1"};
        _user.PaperBin.AddElementToPaperbin(taskTest);
        _user.PaperBin.RestoreElementFromPaperbin(taskTest);
        Assert.AreEqual(_user.PaperBin.ElementsCount, 0);
    }
    
}