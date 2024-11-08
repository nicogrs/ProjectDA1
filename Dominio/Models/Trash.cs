using System.Collections.ObjectModel;

namespace Dominio;

public class Trash
{
    public int Id { get; set; }
    public int ElementsCount { get; set; }

    public Trash()
    {
       // Paperbin = new List<IPaperBinItem>();
        ElementsCount = 0;
    }

    public void AddElementToPaperbin(IPaperBinItem item)
    {
        if (ElementsCount < 10)
        {
         //   Paperbin.Add(item);
            ElementsCount++;
        }
    }

    public void DeleteElementFromPaperbin(IPaperBinItem item)
    {
       // if (Paperbin.Remove(item))
        {
            ElementsCount--;
        }
    }
    
}