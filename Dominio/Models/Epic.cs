namespace Dominio;

public class Epic
{

    public string Title { get; set; }
    public Precedence Priority { get; set; }
    public string Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Task> Tasks { get; set; }
    public int TaskCount { get; set; }


    public enum Precedence
    {
        Low ,
        Medium ,
        Urgent
    }
    
    
}