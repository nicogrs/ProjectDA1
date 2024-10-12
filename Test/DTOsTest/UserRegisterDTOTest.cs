namespace Test;
using DTOs;
using Dominio;

[TestClass]
public class UserRegisterDTOTest
{
    private UserRegisterDTO _userRegisterDTO;

    [TestInitialize]
    public void SetUp()
    {
        _userRegisterDTO = new UserRegisterDTO
        {
            Name = "Juan",
            Surname = "Perez",
            Email = "juanpe@gmail.com",
            BirthDate = new DateTime(1999, 6, 14),
            Password = "TestPass$1",
            Admin = false
        };
    }

    [TestMethod]
    public void ToEntity()
    {
        var user = _userRegisterDTO.ToEntity();
        
        Assert.IsNotNull(user);
        Assert.AreEqual(user.Name, _userRegisterDTO.Name);
        Assert.AreEqual(user.Surname, _userRegisterDTO.Surname);
        Assert.AreEqual(user.Email, _userRegisterDTO.Email);
        Assert.AreEqual(user.BirthDate, _userRegisterDTO.BirthDate);
        Assert.AreEqual(user.Password, _userRegisterDTO.Password);
        Assert.AreEqual(user.Admin, _userRegisterDTO.Admin);
    }
    
}