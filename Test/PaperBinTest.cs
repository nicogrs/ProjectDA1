namespace Test;
using Dominio;
using Moq;

[TestClass]
public class PaperBinTest
{
    private User _user;

    [TestInitialize]
    public void Setup()
    {
        _user = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1",
            PaperBin = new List<IPaperBinElement>(),
            Admin = false
        };
    }
    
    [TestMethod]
    public void AddTaskToPaperBin()
    {
        Task taskTest = new Task{Title = "Task 1"};
        _user.PaperBin.Add(taskTest);
        Assert.AreEqual(_user.PaperBin.Count, 1);
    }
    
    [TestMethod]
    public void AddPanelToPaperBin()
    {
        Panel panelTest = new Panel{Name = "Panel 1"};
        _user.PaperBin.Add(panelTest);
        Assert.AreEqual(_user.PaperBin.Count, 1);
    }
}