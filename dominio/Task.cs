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

    public bool ChangePriority(Priority _priority)
    {
        priority = _priority;
        return priority == _priority;
    }

    public void MarkCommentAsResolved(User user, Comment comment)
    {
        comment.Resolved = true;
        comment.ResolvedBy = user;
        comment.ResolvedTime = DateTime.Now;
    }

    public bool AddComment(User user, string content)
    {
        bool success = false;
        
        
        
        return success;
    }
}