using Microsoft.VisualBasic;

namespace Dominio;

public class Task : IPaperBinItem
{
    public static int taskCounter = 0;
    
    public string Title { get; set; }
    public Priority Precedence { get; set; }
    public string Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Comment> Comments { get; set; }
    public int TaskId { get; private set; }
    
    public Epic Epic { get; set; }

    public Task()
    {
        Comments = new List<Comment>();
        taskCounter++;
        TaskId = taskCounter;
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
        
        Comment commentToAdd = new Comment(user, content);
        Comments.Add(commentToAdd);
        success = true;
        
        return success;
    }

    public override string ToString()
    {
        return $"Tipo: Task - ID: {TaskId.ToString()} - Nombre: {Title} - Prioridad: {Precedence}";
    }
}