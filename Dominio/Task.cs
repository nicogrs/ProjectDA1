namespace Dominio;

public class Task
{
    public string Title { get; set; }
    public Priority Precedence { get; set; }
    public string Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Comment> Comments { get; set; }


    public enum Priority
    {
        Low,
        Medium,
        Urgent
    }

    public bool ChangePriority(Priority _priority)
    {
        Precedence = _priority;
        return Precedence == _priority;
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

        try
        {
            Comment commentToAdd = new Comment(user, content);
            Comments.Add(commentToAdd);
            success = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return success;
    }
    
    public bool IsExpired()
    {
        return DateTime.Now > ExpirationDate;
    }
}