using System.Collections;
using DataAccess;
using Test.Context;
namespace Test.DataAccessTests;
using Dominio;


[TestClass]
public class UserDatabaseRepositoryTests
{
    private User user1;
    private User user2;
    private SqlContext _context;
    private UserDatabaseRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        SqlContextFactory sqlContextFactory = new SqlContextFactory();
        _context = sqlContextFactory.CreateMemoryContext();
        _repository = new UserDatabaseRepository(_context);

        user1 = new User        
        {
            Name = "Carlos",
            Surname = "Lopez",
            Email = "carlos@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
        };
        user2 = new User        
        {
            Name = "Paco",
            Surname = "Lopez",
            Email = "paco@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            Password = "TestPass$1"
        };
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.Users.RemoveRange(_context.Users);
        _context.SaveChanges();
    }

    [TestMethod]
    public void AddTest()
    {
        var addedUser = _repository.Add(user1);

        Assert.IsNotNull(addedUser);
        Assert.AreEqual("Carlos", addedUser.Name);
        Assert.AreEqual(1, _context.Users.Count());
    }

    [TestMethod]
    public void FindTest()
    {
        _context.Users.Add(user1);
        _context.SaveChanges();

        var foundUser = _repository.Find(u => u.Email == user1.Email);
        var notFoundUser = _repository.Find(u => u.Email == "user@mail.com");
        
        Assert.IsNull(notFoundUser);
        Assert.IsNotNull(foundUser);
        Assert.AreEqual("Carlos", foundUser.Name);
    }

    [TestMethod]
    public void UpdateTest()
    {
        _context.Users.Add(user1);
        _context.SaveChanges();

        var updatedUser = new User { Name = "Carlos Pablo", Surname = "Lopez Diaz", Email = "carlos@gmail.com" };
        var result = _repository.Update(updatedUser);

        Assert.IsNotNull(result);
        Assert.AreEqual("Carlos Pablo", result.Name);
        Assert.AreEqual("Lopez Diaz", result.Surname);
    }

    [TestMethod]
    public void DeleteTest()
    {
        _context.Users.Add(user1);
        _context.SaveChanges();

        _repository.Delete(user1.Id);

        Assert.AreEqual(0, _context.Users.Count());
    }

    [TestMethod]
    public void FindAllTest()
    {
        _context.Users.Add(user1);
        _context.Users.Add(user2);
        _context.SaveChanges();

        var allUsers = _repository.FindAll();

        Assert.AreEqual(2, allUsers.Count);
        CollectionAssert.Contains((ICollection)allUsers, user1);
        CollectionAssert.Contains((ICollection)allUsers, user2);
    }
}
