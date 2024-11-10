using Dominio;
using Task = Dominio.Task;

namespace Test;

[TestClass]
public class EpicTest
{
    private Epic e;
    private List<Task> tasks;
    
    [TestInitialize]
    public void Setup()
    {
        tasks = new List<Task>(){};

        e = new Epic()
        {
            Title = "Titulo de epica",
            Priority = Epic.Precedence.Low,
            Description = "Descripcion de la epica",
            ExpirationDate = new DateTime(2030, 1, 1),
            Tasks = new List<Task>()
        };
    }
    
    [TestMethod]
    public void EpicCreate()
    {
        Assert.AreEqual(e.Title, "Titulo de epica");
        Assert.AreEqual(e.Priority,Epic.Precedence.Low);
        Assert.AreEqual(e.Description, "Descripcion de la epica");
        Assert.AreEqual(e.ExpirationDate, new DateTime(2030,1,1));
        Assert.AreEqual(e.Tasks.Count, 0);
    }

    [TestMethod]
    public void AddTaskToEpicTest()
    {
        Task t = new Task()
        {
            Title = "Titulo de tarea",
            Description = "Descripcion de tarea",
            Comments = new List<Comment>(),
            ExpirationDate = new DateTime(2029, 1, 1),
            Precedence = Task.Priority.Low
        };

        e.AddTask(t);

        Assert.AreEqual(e.Tasks.Count, 1);
        Assert.AreSame(e.Tasks[0], t);
    }
}