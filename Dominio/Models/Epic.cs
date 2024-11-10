namespace Dominio;

public class Epic
{
    public string Title { get; set; }
    public Priority Priority { get; set; }
    public string Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Task> Tasks { get; set; }
    
    
    public enum Priority
    {
        Low,
        Medium,
        Urgent
    }
    
    
}