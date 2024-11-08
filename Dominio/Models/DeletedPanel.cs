namespace Dominio;

public class DeletedPanel
{
    public int Id { get; set; }
    public int PanelId { get; set; }
    public int TrashId { get; set; }
    public Panel Panel { get; set; }
    public Trash Trash { get; set; }
}