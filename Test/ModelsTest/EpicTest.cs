using Dominio;
using Task = Dominio.Task;

namespace Test.ModelsTests;

[TestClass]
public class EpicTest
{
    private Epic e;
    private List<Task> tasks;
    private Task t, t2;
    
    [TestInitialize]
    public void Setup()
    {
        tasks = new List<Task>(){};

        e = new Epic()
        {
            Name = "Titulo de epica",
            Priority = Epic.Precedence.Low,
            Description = "Descripcion de la epica",
            ExpirationDate = new DateTime(2030, 1, 1),
            Tasks = new List<Task>()
        };
        
        t = new Task()
        {
            Name = "Titulo de tarea",
            Description = "Descripcion de tarea",
            Comments = new List<Comment>(),
            ExpirationDate = new DateTime(2029, 1, 1),
            Precedence = Task.Priority.Low
        };
        t2 = new Task()
        {
            Name = "Titulo de tarea 2",
            Description = "Descripcion de tarea 2",
            Comments = new List<Comment>(),
            ExpirationDate = new DateTime(2028, 1, 1),
            Precedence = Task.Priority.Medium
        };
    }
    
    [TestMethod]
    public void EpicCreate()
    {
        Assert.AreEqual(e.Name, "Titulo de epica");
        Assert.AreEqual(e.Priority,Epic.Precedence.Low);
        Assert.AreEqual(e.Description, "Descripcion de la epica");
        Assert.AreEqual(e.ExpirationDate, new DateTime(2030,1,1));
        Assert.AreEqual(e.Tasks.Count, 0);
    }

    [TestMethod]
    public void AddTaskToEpicTest()
    {
        e.AddTask(t);

        Assert.AreEqual(e.Tasks.Count, 1);
        Assert.AreSame(e.Tasks[0], t);
        //Assert.AreSame(t.Epic,e);
    }

    [TestMethod]
    public void RemoveTaskFromEpicTest()
    {
        e.AddTask(t);
        e.RemoveTask(t);
        
        Assert.AreEqual(e.Tasks.Count, 0);
      //  Assert.AreNotSame(t.Epic,e);
    }
    
    [TestMethod]
    public void RemoveTaskFromEpicTest2()
    {
        e.AddTask(t);
        e.RemoveTask(t2);
        
        Assert.AreEqual(e.Tasks.Count, 1);
       // Assert.AreSame(t.Epic,e);
        //Assert.AreNotSame(t2.Epic,e);
    }
    
    [TestMethod]
    public void RemoveTaskFromEpicTest3()
    {
        e.RemoveTask(t);
        
        Assert.AreEqual(e.Tasks.Count, 0);
       // Assert.AreNotSame(t.Epic,e);
    }
}