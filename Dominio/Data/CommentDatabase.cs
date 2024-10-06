namespace Dominio.Data;

public class CommentDatabase
{
    public List<Comment> Comments { get; set; }
    

    public CommentDatabase(UserDataBase UDatabase)
    {
        this.Comments = new List<Comment>();
    }
    
}