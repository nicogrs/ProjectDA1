namespace Dominio;

public class PaperBin
{
    public List<IPaperBinElement> Paperbin { get; set; }
    public int ElementsCount { get; set; }

    public PaperBin()
    {
        Paperbin = new List<IPaperBinElement>();
        ElementsCount = 0;
    }

    public void AddElementToPaperbin(IPaperBinElement element)
    {
        if (ElementsCount < 10)
        {
            Paperbin.Add(element);
            ElementsCount++;
        }
    }

    public void DeleteElementFromPaperbin(IPaperBinElement element)
    {
        if (Paperbin.Remove(element))
        {
            ElementsCount--;
        }
    }
    
}