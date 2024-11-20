using OfficeOpenXml;

namespace Services;

public class XlsxToCsvAdapter : TaskImportService
{
    public async Task<string> TranslateXlsxToCsvFromStreamAsync(Stream fileStream)
    {
        string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.xlsx");
        try
        {
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

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0]; 
            int rowCount = worksheet.Dimension.Rows;
            int columnCount = worksheet.Dimension.Columns;

            for (int row = 1; row <= rowCount; row++)
            {
                string line = worksheet.Cells[row, 1].Text; 

                for (int col = 2; col <= columnCount; col++)
                {
                    line += "," + worksheet.Cells[row, col].Text; 
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
