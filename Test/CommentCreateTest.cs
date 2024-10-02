using Dominio;
namespace Test;


[TestClass]
public class CommentCreateTest
{
    private User u1;
    private User u2;
    
    [TestInitialize]
    public void Setup()
    {
        u1 = new User();
        u2 = new User();
    }

    [TestMethod]
    public void CommentCreateCreatedBy()
    {
        Comment c = new Comment(u1, "ComentarioPrueba");
        Assert.Equals(c.CreatedBy, u1);
    }
}