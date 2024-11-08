using Dominio;
namespace Test;


[TestClass]
public class CommentCreateTest
{
    private User u1;
    private User u2;
    private Comment c;
    
    [TestInitialize]
    public void Setup()
    {
        u1 = new User();
        u2 = new User();
        c = new Comment("ComentarioPrueba");
    }

    [TestMethod]
    public void CommentCreateCreatedBy()
    {
        Assert.AreSame(c.CreatedBy, u1);
    }
    
    [TestMethod]
    public void CommentCreateContent()
    {
        Assert.AreSame(c.Content, "ComentarioPrueba");
    }
}