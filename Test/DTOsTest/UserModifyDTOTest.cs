using System.ComponentModel.DataAnnotations;

namespace Test.DTOsTest;
using DTOs;

[TestClass]
public class UserModifyDTOTest
{
    private UserModifyDTO _userModifyDTO;
    private UserModifyDTO validUser;
    private UserModifyDTO invalidUser;

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
        
        validUser = new UserModifyDTO
        {
            Name = "Juan",
            Surname = "Perez",
            Email = "juanpe@gmail.com",
            BirthDate = new DateTime(1999, 6, 14),
            Password = "TestPass$1"
        };
        
        invalidUser = new UserModifyDTO
        {
            Name = "J", 
            Surname = "", 
            BirthDate = new DateTime(1890, 1, 1), 
            Email = "juan.perez", 
            Password = "pass" 
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
    
    [TestMethod]
    public void ValidUser()
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(validUser);
        
        bool isValid = Validator.TryValidateObject(validUser, validationContext, validationResults, true);
        
        Assert.IsTrue(isValid);
        Assert.AreEqual(0, validationResults.Count);
    }

    [TestMethod]
    public void InvalidUser()
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(invalidUser);
        
        bool isValid = Validator.TryValidateObject(invalidUser, validationContext, validationResults, true);
        
        Assert.IsFalse(isValid);
        Assert.IsTrue(validationResults.Count > 0);
    }

    
}
