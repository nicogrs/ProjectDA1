using ClosedXML.Excel;

namespace Services;

public class XlsxToCsvAdapter : TaskImportService
{
    public async Task<string> TranslateXlsxToCsvFromStreamAsync(Stream fileStream)
    {
        string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.xlsx");
        try
        {
            // Write the stream to the temporary file
            using (var fileStreamOut = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.CopyToAsync(fileStreamOut);
            }
            string crudeContent = TranslateXlsxToCsv(tempFilePath);
            return RemoveTimeFromDateTime(crudeContent);
        }
        finally
        {
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }
    }
    public string TranslateXlsxToCsv(string filePath)
    {
        string content = "";
        
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return content;
        }
        
        using (var workbook = new XLWorkbook(filePath))
        {
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed();
            int columnCount = worksheet.Columns().Count();
            
            foreach (var row in rows)
            {
                string line = row.Cell(1).Value.ToString();
                
                for (int i = 2; i <= columnCount; i++)
                {
                  line += "," + row.Cell(i).Value.ToString();
                }
                
                line += Environment.NewLine;
                content += line;
            }
        }
        
        return content;
    }

    public string RemoveTimeFromDateTime(string lines)
    {
        return lines.Replace(" 0:00:00", "");
    }
    
}