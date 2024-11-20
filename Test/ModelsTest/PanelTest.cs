namespace Test.ModelsTests;
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
            Name = "Tarea 1",
            Precedence = Task.Priority.Low,
            Description = "Descripcion tarea 1",
            ExpirationDate = new DateTime(2025, 10, 01),
            Comments = comments1
        };
        t2 = new Task()
        {
            Name = "Tarea 2",
            Precedence = Task.Priority.Medium,
            Description = "Descripcion tarea 2",
            ExpirationDate = new DateTime(2025, 03, 01)
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
        Task taskTest = new Task { Name = "Task 1" };
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