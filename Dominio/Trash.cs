namespace Dominio;

public class Trash
{
    public List<IDeleteable> Paperbin  { get; set; }
    public Trash()
    {
        Paperbin = new List<IDeleteable>();
        ElementsCount = 0;
    }

    public int Id { get; set; }
    public int ElementsCount { get; set; }

    public void AddElementToPaperbin(IDeleteable item)
    {
        if (ElementsCount < 10)
            Paperbin.Add(item);
        item.IsDeleted = true;
        ElementsCount++;
    }

    public void DeleteElementFromPaperbin(IDeleteable item)
    {
        if (Paperbin.Remove(item))
        {
            ElementsCount--;
        }
    }

    public void RestoreElementFromPaperbin(IDeleteable item)
    {
        if (Paperbin.Remove(item))
        {
            ElementsCount--;
            item.IsDeleted = false;
        }
    }
}