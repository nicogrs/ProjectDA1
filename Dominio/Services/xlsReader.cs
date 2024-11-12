namespace Dominio.Services;
using System;
using ClosedXML.Excel;

public class XlsReader : TaskImportService
{

    public string XlsToText(string filePath)
    {
        return "";
    }
    
    public string ConvertXlsFileContentToCsv(string filePath)
    {
        string content = "";
        
        // Ensure the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return content;
        }
        
        // Load the workbook
        using (var workbook = new XLWorkbook(filePath))
        {
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip the header row
            int columnCount = worksheet.Columns().Count();
            
            foreach (var row in rows)
            {
                string line = row.Cell(0).Value.ToString();
                
                for (int i = 1; i < columnCount; i++)
                {
                  line += "," + row.Cell(i).Value.ToString();
                }
                
                line += Environment.NewLine;
                content += line;
            }
        }
        
        return content;
    }
    
    public override List<string> SplitLine(string str)
    {
        return null;
    }

    public override List<string> MakeLineListFromContent(string content)
    {
        return content.Split('\n').ToList();
    }
    
    
}