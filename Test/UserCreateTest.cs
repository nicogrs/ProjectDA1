namespace Test;
using Dominio;

[TestClass]
public class UserCreateTest
{
    private User u;
    
    [TestInitialize]
    public void Setup()
    {
     u  = new User
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "123456"
        };
    }
    
    [TestMethod]
    public void ValidName()
    {
       bool isValidName = u.isNameValid();
       Assert.IsTrue(isValidName);
    }
}