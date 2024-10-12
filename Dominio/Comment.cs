namespace Dominio;

public class Comment
{
    public User CreatedBy {get; set;}
    public bool Resolved {get; set;}
    public User ResolvedBy {get; set;}
    public string Content {get; set;}

    public Comment(User createdBy, string content)
    {
        CreatedBy = createdBy;
        Content = content;
        Resolved = false;
        ResolvedBy = null;
    }
}