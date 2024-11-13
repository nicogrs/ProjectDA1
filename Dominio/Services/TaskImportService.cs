namespace Dominio.Services;

public abstract class TaskImportService
{
    private StreamReader reader;
    private StreamWriter writer;
    private List<Task> tasks;
    private List<string> errors;
    

    public void MakeErrorFile(string errorFileName)
    {
        //directorio interfaz 
        string directory = Directory.GetCurrentDirectory();
        directory = Directory.GetParent(directory).FullName;
        
        string filePath = Path.Combine(directory, errorFileName);
        
        /*if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }*/
        
        writer = new StreamWriter(filePath);
        foreach (string line in errors)
        {
            writer.WriteLine(line);
        }
        writer.Close();
    }

    public List<Task> ReadTasksFromContent(string content, User user)
    {
        tasks = new List<Task>();
        errors = new List<string>();

        List<string> linesOfContent = MakeLineListFromContent(content);

        ProcessLines(linesOfContent);
        
        MakeErrorFile($"ErroresImport-{user.Name}.txt");
        return tasks;
    }

    public abstract List<string> MakeLineListFromContent(string content);
    
    public void ProcessLines(List<string> linesOfLoadedFile)
    {
        foreach (string line in linesOfLoadedFile)
        {
            if (IsLineValid(line))
            {
                List<string> splitLine = SplitLine(line);
                Task newTask = TaskFromStringList(splitLine);
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
        bool correctColumnAmount = separatedLine.Count is 4 or 5;
        
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
    }

    public void LogError(string line, string errorMessage)
    {
        string currentTime = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");
        string error = $"{line} --- {errorMessage} --- {currentTime}";
        errors.Add(error);
    }

    public abstract List<string> SplitLine(string str);
    public Task TaskFromStringList(List<string> strList)
    {
        Task task = new Task();
        task.Title = strList[0];
        task.Description = strList[1];
        
        string strDate = strList[2];
        
        task.ExpirationDate = StringToDate(strDate);

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
        
        return isDateValid && isPanelIdValid;
    }

    public bool HasCorrectElementCount(List<string> elements)
    {
        return (elements.Count <= 5 && elements.Count >= 4);
    }
    
    public bool IsStringPanelIdValid(string panelId)
    {
        return int.TryParse(panelId, out int _);
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
    public bool AreNumbersValid(int day, int month, int year)
    {
        bool isDayValid = day >= 1 && day <= 31;
        bool isMonthValid = month >= 1 && month <= 12;
        bool isYearValid = year >= 2000 && year <= DateTime.Now.Year;
        
        return isDayValid && isMonthValid && isYearValid;
    }

}