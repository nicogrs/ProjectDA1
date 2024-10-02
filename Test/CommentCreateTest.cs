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
        Assert.AreSame(c.CreatedBy, u1);
    }
    
    [TestMethod]
    public void CommentCreateContent()
    {
        Comment c = new Comment(u1, "ComentarioPrueba");
        Assert.AreSame(c.Content, "ComentarioPrueba");
    }
}