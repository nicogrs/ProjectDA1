using Dominio;
namespace Test;


[TestClass]
public class CommentTest
{
    private User u1;
    private User u2;
    
    [TestInitialize]
    public void Setup()
    {
        u1 = new User();
        u2 = new User();
    }
}