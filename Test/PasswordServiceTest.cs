namespace Test;
using Dominio;
using Moq;

[TestClass]
public class PasswordServiceTest
{
    private PasswordService service;

    [TestInitialize]
    public void Setup()
    {
        this.service = new PasswordService();
    }
    
    [TestMethod]
    public void ValidPassword()
    {
        var isPasswordValid = service.IsPasswordValid("Hola123$");
        Assert.IsTrue(isPasswordValid);
    }

    [TestMethod]
    public void InvalidPassword()
    {
        var isPasswordInvalid = service.IsPasswordValid("ThisIsAInvalidPassword");
        Assert.IsFalse(isPasswordInvalid);
    }
    
    [TestMethod]
    public void PasswordGenerate()
    {
    }
    
}