using DataAccess;
using Dominio;
using Interfaces;
using Services;
using Test.Context;

namespace Test;

[TestClass]
public class NotificationServiceTest
{
    private NotificationService _notificationService;
    private SqlContext _context;
    private IRepository<Notification> _notificationDatabase;

    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _notificationDatabase = new NotificationDatabaseRepository(_context);
        _notificationService = new NotificationService(_notificationDatabase);
    }

    [TestMethod]
    public void CreateNotificationTest()
    {
        User user = new User
        {
            Email = "test@test.com",
            Name = "Test",
            Surname = "Test",
            Password = "Test123$"
        };
        Notification notification = new Notification();
        notification.Text = "Test";
        notification.ToUser = user;
        var resultNotification = _notificationService.AddNotification(notification);
        Assert.AreEqual(resultNotification, notification);
    }
}