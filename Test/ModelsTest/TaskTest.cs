using Dominio;
using Task = Dominio.Task;

namespace Test;

[TestClass]
public class TaskTest
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
        c1 = new Comment();
        c2 = new Comment();
        c3 = new Comment();
        comments = new List<Comment>(){c1, c2, c3};
        t1 = new Task()
        {
            Name = "Titulo 1",
            Precedence = Task.Priority.Low,
            Description = "Descripcion tarea 1",
            ExpirationDate = new DateTime(2025, 10, 01),
            Comments = comments
        };
    }
    
    [TestMethod]
    public void TaskCreateTest()
    {
        Assert.AreEqual(t1.Precedence, Task.Priority.Low);
        Assert.AreEqual(t1.Name, "Titulo 1");
        Assert.AreEqual(t1.Description, "Descripcion tarea 1");
        Assert.AreEqual(t1.ExpirationDate, new DateTime(2025, 10, 01));
        Assert.AreEqual(t1.Comments, comments);
    }

    [TestMethod]
    public void ChangePriorityTest()
    {
        bool succesfulChange;
        
        succesfulChange = t1.ChangePriority(Task.Priority.Medium);
        
        Assert.IsTrue(succesfulChange);
        Assert.AreEqual(Task.Priority.Medium, t1.Precedence);
    }
    
    [TestMethod]
    public void ChangePriorityTest2()
    {
        bool firstSuccesfulChange;
        bool secondSuccesfulChange;
        
        firstSuccesfulChange = t1.ChangePriority(Task.Priority.Medium);
        secondSuccesfulChange = t1.ChangePriority(Task.Priority.Urgent);
        
        Assert.IsTrue(firstSuccesfulChange);
        Assert.IsTrue(secondSuccesfulChange);
        Assert.AreEqual(Task.Priority.Urgent, t1.Precedence);
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

    [TestMethod]
    public void AddCommentTest()
    {
        int initialCommentCount = t1.Comments.Count;
        string content = "Comentario de AddComentTest";
        Comment lastAddedComment;
        
        //`t1.AddComment(u1,content);
        
        lastAddedComment = t1.Comments.Last();
        Assert.AreEqual(t1.Comments.Count, initialCommentCount + 1);
        Assert.AreEqual(t1.Comments.Last().Content, content);
    }

    [TestMethod]
    public void ToStringTest()
    {
        var result = $"Tipo: Task - ID: {t1.Id} - Nombre: Titulo 1 - Prioridad: Low" ;
        Assert.AreEqual(t1.ToString(), result);
    }

    [TestMethod]
    public void AddCommentExceptionTest()
    {
        
    }
}