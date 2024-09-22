namespace Test;
using Dominio;

[TestClass]
public class UserCreateTest
{

    [TestInitialize]
    public void Setup()
    {
    }
    
    [TestMethod]
    public void CreateNewUser()
    {
        User u = new User
        {
            Nickname = "carlos123",
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "123456"
        };
        
        User userExists = u.getUserByNickname(u.Nickname);
        
        Assert.Equals(userExists, u);

    }
}