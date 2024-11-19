namespace Test.DTOsTest;
using DTOs;
using Dominio;

[TestClass]
public class UserLoginDTOTest
{
    private UserLoginDTO _userLoginDTO1;
    private UserLoginDTO _userLoginDTO2;
    private User user;

    [TestInitialize]
    public void SetUp()
    {
        _userLoginDTO1 = new UserLoginDTO
        {
            Email = "juanpe@gmail.com",
            Password = "TestPass$1",
        };
        user = new User
        {
            Email = "lolalamas@gmail.com",
            Password = "TestPass$2",
        };
    }

    [TestMethod]
    public void ToEntity()
    {
        var user = _userLoginDTO1.ToEntity();
        
        Assert.IsNotNull(user);
        Assert.AreEqual(user.Email, _userLoginDTO1.Email);
        Assert.AreEqual(user.Password, _userLoginDTO1.Password);
    }

    [TestMethod]
    public void FromEntity()
    {
        _userLoginDTO2 = UserLoginDTO.FromEntity(user);
        Assert.IsNotNull(_userLoginDTO2);
        Assert.AreEqual(_userLoginDTO2.Email, user.Email);
        Assert.AreEqual(_userLoginDTO2.Password, user.Password);
    }
}