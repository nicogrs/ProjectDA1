using System.Collections;
using Test.Context;
using DataAccess;
namespace Test.DataAccessTests;
using Dominio;

[TestClass]
public class PanelDatabaseRepositoryTests
{
    private SqlContext _context;
    private PanelDatabaseRepository _repository;
    private Panel panel1;
    private Panel panel2;

    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _repository = new PanelDatabaseRepository(_context);
        panel1 = new Panel
        {
            Name = "Panel1", 
            Description = "Descripcion", 
            Team = "Equipo1"
        };
        panel2 = new Panel
        {
            Name = "Panel2", 
            Description = "Descripcion", 
            Team = "Equipo1"
        };
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void AddTest()
    {
        var addedPanel = _repository.Add(panel1);

        Assert.IsNotNull(addedPanel);
        Assert.AreEqual("Panel1", addedPanel.Name);
        Assert.AreEqual(1, _context.Panels.Count());
    }

    [TestMethod]
    public void FindTest()
    {
        _repository.Add(panel1);

        var foundPanel = _repository.Find(p => p.Name == panel1.Name);
        var notFoundPanel = _repository.Find(p => p.Name == "Panel3");
        
        Assert.IsNull(notFoundPanel);
        Assert.IsNotNull(foundPanel);
        Assert.AreEqual("Panel1", foundPanel.Name);
    }

    [TestMethod]
    public void FindAllTest()
    {
        _repository.Add(panel1);
        _repository.Add(panel2);

        var panels = _repository.FindAll();

        Assert.AreEqual(2, panels.Count);
        CollectionAssert.Contains((ICollection)panels, panel1);
        CollectionAssert.Contains((ICollection)panels, panel2);

    }

    [TestMethod]
    public void UpdateTest()
    {
        _repository.Add(panel1);

        var updatedPanel = new Panel
        {
            Id = panel1.Id,
            Name = "Panel11",
            Description = "Descripcion1",
            Team = "Equipo2"
        };


        var result = _repository.Update(updatedPanel);

        Assert.IsNotNull(result);
        Assert.AreEqual("Panel11", result.Name);
        Assert.AreEqual("Descripcion1", result.Description);
    }

    [TestMethod]
    public void DeleteTest()
    {
        _repository.Add(panel1);

        _repository.Delete(panel1.Id);

        Assert.AreEqual(0, _context.Panels.Count());
    }
}
