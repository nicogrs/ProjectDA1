namespace Dominio;

public class Task
{
    public string Title { get; set; }
    public string priority { get; set; }
    public string Description { get; set; }
    public DateTime expDate { get; set; }
    public List<Comment> comments { get; set; }
    
}