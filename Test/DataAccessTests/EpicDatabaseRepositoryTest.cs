using System.Collections;
using DataAccess;
using Test.Context;
namespace Test.DataAccessTests;
using Dominio;

[TestClass]
public class EpicDatabaseRepositoryTest
{
    private Epic epic1;
    private Epic epic2;
    private SqlContext _context;
    private EpicDatabaseRepository _repository;
    
    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _repository = new EpicDatabaseRepository(_context);

        epic1 = new Epic
        {
            Name = "epic1",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
        };
        epic2 = new Epic
        {
            Name = "epic2",
            Description = "DescriptionTest",
            ExpirationDate = DateTime.Now.AddDays(1),
        };
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Epic.RemoveRange(_context.Epic);
        _context.SaveChanges();
    }

    [TestMethod]
    public void AddTest()
    {
        var addedEpic = _repository.Add(epic1);

        Assert.IsNotNull(addedEpic);
        Assert.AreEqual("epic1", addedEpic.Name);
        Assert.AreEqual(1, _context.Epic.Count());
    }

    [TestMethod]
    public void FindTest()
    {
        _context.Epic.Add(epic1);
        _context.SaveChanges();

        var foundEpic = _repository.Find(e => e.Name == epic1.Name);
        var notFoundEpic = _repository.Find(e => e.Name == "Epic 3");
        
        Assert.IsNull(notFoundEpic);
        Assert.IsNotNull(foundEpic);
        Assert.AreEqual("epic1", foundEpic.Name);
    }

    [TestMethod]
    public void UpdateTest()
    {
        _context.Epic.Add(epic1);
        _context.SaveChanges();

        var updatedEpic = new Epic { Name = "Epic 11", Description = "Descripcion 1"};
        var result = _repository.Update(updatedEpic);

        Assert.IsNotNull(result);
        Assert.AreEqual("Epic 11", result.Name);
        Assert.AreEqual("Descripcion 1", result.Description);
    }

    [TestMethod]
    public void DeleteTest()
    {
        _context.Epic.Add(epic1);
        _context.SaveChanges();

        _repository.Delete(epic1.Id);

        Assert.AreEqual(0, _context.Epic.Count());
    }

    [TestMethod]
    public void FindAllTest()
    {
        _context.Epic.Add(epic1);
        _context.Epic.Add(epic2);
        _context.SaveChanges();

        var allEpic = _repository.FindAll();

        Assert.AreEqual(2, allEpic.Count);
        CollectionAssert.Contains((ICollection)allEpic, epic1);
        CollectionAssert.Contains((ICollection)allEpic, epic2);
    }
}