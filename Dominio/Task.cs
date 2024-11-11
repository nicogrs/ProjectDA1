namespace Dominio;

public class Task : IDeleteable

{
    public Priority Precedence { get; set; }
    public int PanelId { get; set; }
    public Panel Panel { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Comment> Comments { get; set; }
    public enum Priority
    {
        Low,
        Medium,
        Urgent
    }

    public Task()
    {
        Comments = new List<Comment>();
        IsDeleted = false;
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

    public override string ToString()
    {
        return $"Tipo: Task - ID: {Id.ToString()} - Nombre: {Name} - Prioridad: {Precedence}";
    }
    
}