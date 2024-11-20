using Dominio;
using Task = Dominio.Task;

namespace Test.ModelsTests;


[TestClass]
public class CommentTest
{
    private User u1;
    private User u2;
    private Comment c;
    private Task t;
    
    [TestInitialize]
    public void Setup()
    {
        u1 = new User { Id = 1, Name = "User1" };
        u2 = new User { Id = 2, Name = "User2" };
        t = new Task { Id = 1, Name = "Task" };
    }

    [TestMethod]
    public void CommentCreate()
    {
        c = new Comment
        {
            Id = 1,
            CreatedById = 1,
            CreatedBy = u1,
            Resolved = false,
            ResolvedById = null,
            ResolvedBy = null,
            Task = t,
            TaskId = 1,
            ResolvedTime = default(DateTime),
            Content = "ComentarioPrueba",
        };
        
        Assert.AreEqual(c.Id, 1);
        Assert.AreEqual(c.CreatedById, 1);
        Assert.AreSame(c.CreatedBy, u1);
        Assert.IsFalse(c.Resolved);
        Assert.IsNull(c.ResolvedBy);
        Assert.IsNull(c.ResolvedById);
        Assert.AreSame(c.Task, t);
        Assert.AreEqual(c.TaskId, 1);
        Assert.AreEqual(c.ResolvedTime,default(DateTime));
        Assert.AreEqual(c.Content, "ComentarioPrueba");
    }
}