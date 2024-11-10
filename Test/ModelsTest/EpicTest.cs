namespace Test;

[TestClass]
public class EpicTest
{
    private Epic e;
    private List<Task> tasks;
    
    [TestInitialize]
    public void Setup()
    {
        tasks = new List<Task>();
        
        e = new Epic()
        {
            Title = "Titulo de epica",
            Priority = Epic.Priority.Low,
            Description = "Descripcion de la epica",
            ExpirationDate = new DateTime(2030,1,1),
            Tasks = tasks
        }
    }
    
    [TestMethod]
    public void EpicCreate()
    {
        Assert.AreEqual(e.Title, "Titulo de epica");
        Assert.AreEqual(e.Priority,Epic.Priority.Low);
        Assert.AreEqual(e.Description, "Descripcion de la epica");
        Assert.AreEqual(e.ExpirationDate, new DateTime(2030,1,1));
        Assert.AreEqual(e.Tasks, tasks);
    }
}