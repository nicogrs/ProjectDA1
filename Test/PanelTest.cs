namespace Test;
using Task = Dominio.Task;
using Dominio;

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
        Comment c1 = new Comment(u, "Primer comentario");
        Comment c2 = new Comment(u, "Segundo comentario");
        Comment c3 = new Comment(u, "Primer comentario T2");
        Comment c4 = new Comment(u, "Segundo comentario T2");
        comments1 = new List<Comment>(){c1, c2};
        List<Comment> comments2 = new List<Comment>(){c3, c4};
        
        panel = new Panel()
        {
            Name = "Panel numero 1",
            Team = "Equipo 1",
            Description = "Descripcion panel 1",
            CreatedBy = u
        };
        
        t1 = new Task()
        {
            Title = "Tarea 1",
            priority = Task.Priority.Low,
            Description = "Descripcion tarea 1",
            expDate = new DateTime(2025, 10, 01),
            comments = comments1
        };
        t2 = new Task()
        {
            Title = "Tarea 2",
            priority = Task.Priority.Medium,
            Description = "Descripcion tarea 2",
            expDate = new DateTime(2025, 03, 01),
            comments = comments2
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
    }

    
    
}