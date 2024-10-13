namespace Test;
using DTOs;

[TestClass]
public class UserModifyDTOTest
{
    private UserModifyDTO _userModifyDTO;

    [TestInitialize]
    public void SetUp()
    {
        _userModifyDTO = new UserModifyDTO
        {
            Name = "Sofia",
            Surname = "Gomez",
            Email = "sofi@gmail.com",
            BirthDate = new DateTime(1988, 2, 28),
            Password = "TestPass$1",
        };
    }

    [TestMethod]
    public void ToEntity()
    {
        var user = _userModifyDTO.ToEntity();
        
        Assert.IsNotNull(user);
        Assert.AreEqual(user.Name, _userModifyDTO.Name);
        Assert.AreEqual(user.Surname, _userModifyDTO.Surname);
        Assert.AreEqual(user.Email, _userModifyDTO.Email);
        Assert.AreEqual(user.BirthDate, _userModifyDTO.BirthDate);
        Assert.AreEqual(user.Password, _userModifyDTO.Password);
    }
    
}