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
       var isValidName = u.IsNameValid();
       Assert.IsTrue(isValidName);
    }

    [TestMethod]
    public void ValidSurname()
    {
        var isValidSurname = u.IsSurnameValid();
        Assert.IsTrue(isValidSurname);
    }
    
    [TestMethod]
    public void ValidEmail()
    {
        var isValidEmail = u.IsEmailValid();
        Assert.IsTrue(isValidEmail);
    }

    [TestMethod]
    public void ValidBirthDate()
    {
        var isValidBirthDate = u.IsBirthDateValid();
        Assert.IsTrue(isValidBirthDate);
    }

    [TestMethod]
    public void ValidUser()
    {
        var isUserValid = u.ValidateUser();
        Assert.IsTrue(isUserValid);
    }
    
    [TestMethod]
    public void ValidPassword()
    {
        var isPasswordValid = u.ValidatePassword();
        Assert.IsTrue(isPasswordValid);
    }
    

    
}