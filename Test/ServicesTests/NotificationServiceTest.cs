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
    private IRepository<Notification> _panelDatabase;

    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _notificationService = new NotificationService(_panelDatabase);
    }

    [TestMethod]
    public void CreateNotificationTest()
    {
        User user = new User
        {
            Email = "test@test.com",
            Name = "Test",
            Surname = "Test",
        };
        Notification notification = new Notification();
        notification.Text = "Test";
        notification.ToUser = user;
        _notificationService.AddNotification(notification);
    }
}