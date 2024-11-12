namespace Dominio.Services;
using System;
using ClosedXML.Excel;

public class XlsReader : TaskImportService
{
    public List<string> ConvertXlsFileContentToCsv(string filePath)
    {
        List<string> lines = new List<string>();
        
        // Ensure the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return lines;
        }
        
        // Load the workbook
        using (var workbook = new XLWorkbook(filePath))
        {
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip the header row

            foreach (var row in rows)
            {
                string line = "";
                
                for (int i = 0; i < 5; i++)
                {
                  line += row.Cell(i).Value.ToString();  
                }

                lines.Add(line);
            }
        }
        
        return lines;
    }
}