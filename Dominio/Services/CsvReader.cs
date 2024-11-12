namespace Dominio.Services;

public class CsvReader : TaskImportService
{
    public override List<string> SplitLine(string str)
    {
        string[] strArr = str.Split(",");
        
        List<string> toReturn = strArr.ToList();
        
        return toReturn;
    }
    
    public override List<string> MakeLineListFromContent(string content)
    {
        return content.Split('\n').ToList();
    }
}

