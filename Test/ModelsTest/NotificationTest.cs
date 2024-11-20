using Dominio;

namespace Test.ModelsTests;


[TestClass]
public class NotificationTest
{
    [TestMethod]
    public void NotificationCreate()
    {
        User user1 = new User()
        {
            Email = "from@email.com",
            Id = 1,
        };
        User user2 = new User()
        {
            Email = "another@email.com",
            Id = 2
        };

        Notification n = new Notification()
        {
            Id = 1,
            Text = "Notification text",
            FromUser = user1,
            FromUserId = user1.Id,
            ToUser = user2,
            ToUserId = user2.Id,
        };
        
        Assert.AreEqual(n.Id, 1);
        Assert.AreEqual(n.Text, "Notification text");
        Assert.AreSame(n.FromUser, user1);
        Assert.AreEqual(n.FromUserId, user1.Id);
        Assert.AreSame(n.ToUser, user2);
        Assert.AreEqual(n.ToUserId, user2.Id);
    }
}