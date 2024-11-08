namespace Test;
using Dominio;
using Task = Dominio.Task;

[TestClass]
public class PanelTest
{
    public Task t1;
    private Task t2;
    private User u;
    private Panel panel;
    private List<Comment> comments1;

    [TestInitialize]
    public void Setup()
    {
        u = new User();
        Comment c1 = new Comment("Primer comentario");
        Comment c2 = new Comment("Segundo comentario");
        Comment c3 = new Comment("Primer comentario T2");
        Comment c4 = new Comment("Segundo comentario T2");
        comments1 = new List<Comment>() { c1, c2 };
        List<Comment> comments2 = new List<Comment>() { c3, c4 };

        panel = new Panel()
        {
            Name = "Panel numero 1",
            Team = "Equipo 1",
            Description = "Descripcion panel 1",
            CreatedBy = u,
            Id = 0
        };

        t1 = new Task()
        {
            Title = "Tarea 1",
            Precedence = Task.Priority.Low,
            Description = "Descripcion tarea 1",
            ExpirationDate = new DateTime(2025, 10, 01),
            Comments = comments1
        };
        t2 = new Task()
        {
            Title = "Tarea 2",
            Precedence = Task.Priority.Medium,
            Description = "Descripcion tarea 2",
            ExpirationDate = new DateTime(2025, 03, 01),
            Comments = comments2
        };
    }

    [TestMethod]
    public void PanelCreateTest()
    {
        Assert.IsNotNull(panel);
        Assert.AreEqual(panel.Name, "Panel numero 1");
        Assert.AreEqual(panel.Team, "Equipo 1");
        Assert.AreEqual(panel.Description, "Descripcion panel 1");
        Assert.AreEqual(panel.CreatedBy, u);
        Assert.AreEqual(panel.Id, 0);
    }

    [TestMethod]
    public void PanelAddTaskTest()
    {
        int initialTaskCount = panel.GetTaskCount();

        panel.AddTask(t1);

        Assert.AreEqual(t1, panel.Tasks.Last());
        Assert.AreEqual(initialTaskCount + 1, panel.GetTaskCount());
    }

    [TestMethod]
    public void RemoveTaskTest()
    {
        Task taskTest = new Task { Title = "Task 1" };
        panel.AddTask(taskTest);
        panel.DeleteItem(taskTest.Id);
        CollectionAssert.DoesNotContain(panel.Tasks, taskTest);
    }

    [TestMethod]
    public void ToStringTest()
    {
        var result = "Tipo: Panel - ID: 0 - Nombre: Panel numero 1";
        Assert.AreEqual(panel.ToString(), result);
    }
}