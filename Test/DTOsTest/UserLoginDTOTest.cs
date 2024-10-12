namespace Test;
using DTOs;
using Dominio;

[TestClass]
public class UserLoginDTOTest
{
    private UserLoginDTO _userLoginDTO;

    [TestInitialize]
    public void SetUp()
    {
        _userLoginDTO = new UserLoginDTO
        {
            Email = "juanpe@gmail.com",
            Password = "TestPass$1",
        };
    }

    [TestMethod]
    public void ToEntity()
    {
        var user = _userLoginDTO.ToEntity();
        
        Assert.IsNotNull(user);
        Assert.AreEqual(user.Email, _userLoginDTO.Email);
        Assert.AreEqual(user.Password, _userLoginDTO.Password);
    }
    
}