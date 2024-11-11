namespace Dominio;

public class Comment
{
    public int Id { get; set; }
    public int? CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public bool Resolved { get; set; }
    public int? ResolvedById { get; set; }
    public int TaskId { get; set; }
    public Task Task { get; set; }
    public User ResolvedBy { get; set; }
    public DateTime ResolvedTime { get; set; }
    public string Content { get; set; }
}