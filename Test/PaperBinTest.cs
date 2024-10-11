namespace Test;
using Dominio;
using Moq;

[TestClass]
public class PaperBinTest
{
    Mock<IUserDatabase> mockUserDatabase;
    private IUserService _userService;
    private User _user;

    [TestInitialize]
    public void Setup()
    {
        mockUserDatabase = new Mock<IUserDatabase>();
        _userService = new UserService(mockUserDatabase.Object);
        _user = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1",
            Trash = new PaperBin(),
            Admin = false
        };
    }
    
    [TestMethod]
    public void AddTaskToPaperBin()
    {
        Task taskTest = new Task{Title = "Task 1"};
        _user.Trash.AddElementToPaperbin(taskTest);
        Assert.AreEqual(_user.Trash.ElementsCount, 1);
    }
    
    [TestMethod]
    public void AddPanelToPaperBin()
    {
        Panel panelTest = new Panel{Name = "Panel 1"};
        _user.Trash.AddElementToPaperbin(panelTest);
        Assert.AreEqual(_user.Trash.ElementsCount, 1);
    }

    [TestMethod]
    public void DeleteItem()
    {
        Panel panelTest = new Panel{Name = "Panel 1"};
        _user.Trash.AddElementToPaperbin(panelTest);
        CollectionAssert.DoesNotContain(_user.Trash.Paperbin, panelTest);
        
    }
}