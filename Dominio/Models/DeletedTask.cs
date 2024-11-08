namespace Dominio;

public class DeletedTask
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int TrashId { get; set; }
    public Task Task { get; set; }
    public Trash Trash { get; set; }
}