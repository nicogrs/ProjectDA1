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

        Epic epic = new Epic
        {
            Title = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
        };
        var result = epicService.CreateEpic(epic);
        Assert.AreEqual(epic, result);
    }

    [TestMethod]
    public void GetEpicByIdTest()
    {
        Epic epic = new Epic
        {
            Title = "Test",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
        };
        epicService.CreateEpic(epic);
        var result = epicService.GetEpicById(epic.Id);
        Assert.AreEqual(result, epic);
    }

    [TestMethod]
    public void AddEpicToTask()
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
        };
        epicService.AddTaskToEpic(epic.Id, task);
    }
}