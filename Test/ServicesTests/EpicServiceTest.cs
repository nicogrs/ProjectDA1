using DataAccess;
using Dominio;
using Interfaces;
using Services;
using Test.Context;
using Task = Dominio.Task;

namespace Test;

[TestClass]
public class EpicServiceTest
{
    private EpicService epicService;
    private SqlContext _context;
    private IRepository<Epic> _epicDatabase;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _epicDatabase = new EpicDatabaseRepository(_context);
        epicService = new EpicService(_epicDatabase);
    }

    [TestMethod]
    public void CreateEpicTest()
    {
        Task task = new Task
        {
            Name = "Test",
            Description = "TaskDescription"
        };
        Epic epic = new Epic
        {
            Title = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
            Tasks = new List<Task>{task}
        };
        var result = epicService.CreateEpic(epic);
        Assert.AreEqual(epic, result);
    }
}