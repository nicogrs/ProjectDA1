using System.Collections;
using DataAccess;
using Test.Context;
namespace Test.DataAccessTests;
using Dominio;

[TestClass]
public class NotificationDatabaseRepositoryTest
{
    private Notification notification1;
    private Notification notification2;
    private User user1;
    private User user2;
    private SqlContext _context;
    private NotificationDatabaseRepository _repository;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _repository = new NotificationDatabaseRepository(_context);

        user1 = new User
        {
            Email = "test@test.com",
            Name = "Test",
            Surname = "Test",
            Password = "Test123$"
        };
        notification1 = new Notification();
        notification1.Text = "Test";
        notification1.ToUser = user1;
        
        user2 = new User
        {
            Email = "coso@coso.com",
            Name = "Coso",
            Surname = "Coso",
            Password = "Coso123$"
        };
        notification2 = new Notification();
        notification2.Text = "Coso";
        notification2.ToUser = user2;
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Notifications.RemoveRange(_context.Notifications);
        _context.SaveChanges();
    }

    [TestMethod]
    public void AddTest()
    {
        var addedNotification = _repository.Add(notification1);

        Assert.IsNotNull(addedNotification);
        Assert.AreEqual("Test", addedNotification.Text);
        Assert.AreEqual(1, _context.Notifications.Count());
    }

    [TestMethod]
    public void FindTest()
    {
        _context.Notifications.Add(notification1);
        _context.SaveChanges();

        var foundNotification = _repository.Find(n => n.Text == notification1.Text);
        var notFoundNotification = _repository.Find(n => n.Text == "Notification 3");
        
        Assert.IsNull(notFoundNotification);
        Assert.IsNotNull(foundNotification);
        Assert.AreEqual("Test", foundNotification.Text);
    }
    
    [TestMethod]
    public void UpdateTest()
    {
        _context.Notifications.Add(notification1);
        _context.SaveChanges();

        var updatedNotification = new Notification();
        updatedNotification.ToUser = user1;
        updatedNotification.Text = "Prueba";

        try
        {
            var result = _repository.Update(updatedNotification);
            Assert.IsNotNull(result);
            Assert.AreEqual("Prueba", result.Text);
            Assert.AreEqual(user1, result.ToUser);
        }
        catch (NotImplementedException ex)
        {
            Assert.AreEqual("The method or operation is not implemented.", ex.Message);
        }
    }


    [TestMethod]
    public void DeleteTest()
    {
        _context.Notifications.Add(notification1);
        _context.SaveChanges();

        _repository.Delete(notification1.Id);

        Assert.AreEqual(0, _context.Notifications.Count());
    }

    [TestMethod]
    public void FindAllTest()
    {
        _context.Notifications.Add(notification1);
        _context.Notifications.Add(notification2);
        _context.SaveChanges();

        var allNotifications = _repository.FindAll();

        Assert.AreEqual(2, allNotifications.Count);
        CollectionAssert.Contains((ICollection)allNotifications, notification1);
        CollectionAssert.Contains((ICollection)allNotifications, notification2);
    }
}