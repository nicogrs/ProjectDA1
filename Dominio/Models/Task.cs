using Microsoft.VisualBasic;

namespace Dominio;

public class Task : IPaperBinItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Priority Precedence { get; set; }
    public string Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Comment> Comments { get; set; }
    public Task()
    {
        Comments = new List<Comment>();
    }

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
        
        Comment commentToAdd = new Comment(content);
        Comments.Add(commentToAdd);
        success = true;
        
        return success;
    }

    public override string ToString()
    {
        return $"Tipo: Task - ID: {Id.ToString()} - Nombre: {Title} - Prioridad: {Precedence}";
    }
}