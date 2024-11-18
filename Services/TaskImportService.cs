using Dominio;
using Interfaces;
using Task = Dominio.Task;

namespace Services;

public class TaskImportService
{
    private StreamReader reader;
    private StreamWriter writer;
    private List<ImportedTask> tasks;
    public List<string> errors;

    public class ImportedTask : Task
    {
        public int epicId { get; set; }
    }
    
    public void MakeErrorFile(string errorFileName)
    {
        string directory = Directory.GetCurrentDirectory();
        directory = Directory.GetParent(directory).FullName;
        
        string filePath = Path.Combine(directory, errorFileName);
        
        writer = new StreamWriter(filePath);
        foreach (string line in errors)
        {
            writer.WriteLine(line);
        }
        writer.Close();
    }

    public List<ImportedTask> ReadTasksFromContent(string content, User user)
    {
        tasks = new List<ImportedTask>();
        errors = new List<string>();

        List<string> linesOfContent = MakeLineListFromContent(content);

        ProcessLines(linesOfContent);
        
        MakeErrorFile($"ErroresImport-{user.Name}.txt");
        return tasks;
    }

    public List<string> MakeLineListFromContent(string content)
    {
        return content.Split('\n').ToList();
    }
    
    public void ProcessLines(List<string> linesOfLoadedFile)
    {
        foreach (string line in linesOfLoadedFile)
        {
            if (IsLineValid(line))
            {
                List<string> splitLine = SplitLine(line);
                ImportedTask newTask = TaskFromStringList(splitLine);
                tasks.Add(newTask);
            }
            else
            {
                ProcessError(line);
            }
        }
    }
    
    public void ProcessError(string line)
    {
        List<string> separatedLine = SplitLine(line);
        bool correctColumnAmount = separatedLine.Count is 5 or 6;
        
        if (!correctColumnAmount)
        {
            LogError(line,"Cantidad incorrecta de columnas.");
            return;
        }
        
        if (!IsStringValidDate(separatedLine[2]))
        {
            LogError(line,"Formato de fecha incorrecto.");
            return;
        }
        
        if (!IsStringPanelIdValid(separatedLine[3]))
        {
            LogError(line,"Id de panel incorrecto.");
            return;
        }
        
        if (!IsStringExpectedEffortValid(separatedLine[4]))
        {
            LogError(line,"Error en formato de esfuerzo esperado.");
            return;
        }
        
        if (!IsStringEpicIdValid(separatedLine[5]))
        {
            LogError(line,"Error en formato de Id de Epica.");
            return;
        }
    }

    public void LogError(string line, string errorMessage)
    {
        string currentTime = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");
        string error = $"{line} --- {errorMessage} --- {currentTime}";
        errors.Add(error);
    }

    public static List<string> SplitLine(string str)
    {
        string[] strArr = str.Split(",");
        
        List<string> toReturn = strArr.ToList();
        
        return toReturn;
    }
    public ImportedTask TaskFromStringList(List<string> strList)
    {
        //Título,Descripción,Fecha de vencimiento,ID de panel,Esfuerzo Estimado,ID de epica
        ImportedTask task = new ImportedTask();
        task.Name = strList[0];
        task.Description = strList[1];
        
        string strDate = strList[2];
        
        task.ExpirationDate = StringToDate(strDate);

        task.ExpectedEffort = int.Parse(strList[4]);
        task.epicId = int.Parse(strList[5]);
        if (task.epicId != null)
        {
            task.IsInEpic = true;
        }
        
        return task;
    }
    public DateTime StringToDate(string strDate)
    {
        string[] dateArray = strDate.Split("/");
        DateTime date = new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[1]), int.Parse(dateArray[0]));
        
        return date;
    }


    public bool IsLineValid(string line)
    {
        List<string> elements = SplitLine(line);
        if (!HasCorrectElementCount(elements))
            return false;
        
        bool isDateValid = IsStringValidDate(elements[2]);
        bool isPanelIdValid = IsStringPanelIdValid(elements[3]);
        bool isEspectedEffortValid = IsStringExpectedEffortValid(elements[4]);
        bool isEpicIdValid = IsStringEpicIdValid(elements[5]);
        
        return isDateValid && isPanelIdValid && isEspectedEffortValid && isEpicIdValid;
    }

    public bool HasCorrectElementCount(List<string> elements)
    {
        return (elements.Count <= 6 && elements.Count >= 5);
    }
    
    public bool IsStringValidDate(string str)
    {
        if (!str.Contains('/'))
            return false;
        
        string[] dateArray = str.Split("/");
        
        if (dateArray.Length != 3)
            return false;
        
        int day, month, year;
        bool isDayNumber = int.TryParse(dateArray[0], out day);
        bool isMonthNumber = int.TryParse(dateArray[1], out month);
        bool isYearNumber = int.TryParse(dateArray[2], out year);
        bool areNumbers = isDayNumber && isMonthNumber && isYearNumber;
        
        if (!areNumbers)
            return false;
        
        bool areNumbersValid = AreNumbersValid(day,month,year);
        
        return areNumbersValid;
    }
    
    public bool IsStringPanelIdValid(string panelId)
    {
        return int.TryParse(panelId, out int _);
    }

    public bool IsStringExpectedEffortValid(string expectedEffort)
    {
        return int.TryParse(expectedEffort, out int _);
    }
    
    public bool IsStringEpicIdValid(string epicId)
    {
        return int.TryParse(epicId, out int _);
    }
    
    public bool AreNumbersValid(int day, int month, int year)
    {
        bool isDayValid = day >= 1 && day <= 31;
        bool isMonthValid = month >= 1 && month <= 12;
        bool isYearValid = year >= 2000 && year <= DateTime.Now.Year + 25;
        
        return isDayValid && isMonthValid && isYearValid;
    }

}