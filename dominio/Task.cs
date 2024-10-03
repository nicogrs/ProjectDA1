namespace Dominio;

public class Task
{
    public string Title { get; set; }
    public Priority priority { get; set; }
    public string Description { get; set; }
    public DateTime expDate { get; set; }
    public List<Comment> comments { get; set; }


    public enum Priority
    {
        Low,
        Medium,
        Urgent
    }

    public bool ChangePriority(Priority priority)
    {
        return false;
    }
}