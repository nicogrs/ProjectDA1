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
        u1 = new User { Id = 1, Name = "User1" };
        u2 = new User { Id = 2, Name = "User2" };
        c = new Comment
        {
            CreatedBy = u1,
            Content = "ComentarioPrueba",
            Resolved = false,
            ResolvedBy = null,
            ResolvedTime = default(DateTime)
        };
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