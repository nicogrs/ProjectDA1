namespace Dominio.Services;

public class CsvReader : TaskImportService
{
    
    
    public override List<string> MakeLineListFromContent(string content)
    {
        return content.Split('\n').ToList();
    }
}

