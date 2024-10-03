using Dominio;
using Task = Dominio.Task;

namespace Test;

[TestClass]
public class TaskCreateTest
{
    private User u1;
    private User u2;
    private Comment c1;
    private Comment c2;
    private Comment c3;
    private List<Comment> comments;
    private Task t1;
    
    [TestInitialize]
    public void Setup()
    {
        u1 = new User();
        u2 = new User();
        c1 = new Comment(u1, "Primer comentario de u1");
        c2 = new Comment(u1, "Segundo comentario de u1");
        c3 = new Comment(u2, "Primer comentario de u2");
        comments = new List<Comment>(){c1, c2, c3};
        t1 = new Task()
        {
            Title = "Titulo 1",
            priority = Task.Priority.Low,
            Description = "Descripcion tarea 1",
            expDate = new DateTime(2025, 10, 01),
            comments = comments
        };
    }
    
    [TestMethod]
    public void TaskCreate()
    {
        Assert.AreEqual(t1.priority, Task.Priority.Low);
        Assert.AreEqual(t1.Title, "Titulo 1");
        Assert.AreEqual(t1.Description, "Descripcion tarea 1");
        Assert.AreEqual(t1.expDate, new DateTime(2025, 10, 01));
        Assert.AreEqual(t1.comments, comments);
    }

    [TestMethod]
    public void ChangePriorityTest()
    {
        bool succesfulChange;
        
        succesfulChange = t1.ChangePriority(Task.Priority.Medium);
        
        Assert.IsTrue(succesfulChange);
        Assert.AreEqual(Task.Priority.Medium, t1.priority);
    }
    
    
    [TestMethod]
    public void ResolveCommentTest1()
    {
        DateTime timeOfChange;
        TimeSpan timeDifference;
        
        t1.MarkCommentAsResolved(u2, c1);
        timeOfChange = DateTime.Now;
        timeDifference = timeOfChange - c1.ResolvedTime;
        
        Assert.IsTrue(c1.Resolved);
        Assert.AreEqual(c1.ResolvedBy,u2);
        Assert.IsTrue(timeDifference.TotalSeconds <= 5);
    }
}