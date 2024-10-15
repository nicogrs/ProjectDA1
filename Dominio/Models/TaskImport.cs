namespace Dominio;

public class TaskImport
{
    private StreamReader reader;
    private StreamWriter writer;
    private List<Task> tasks;
    private List<string> errors;
    
    
    /*public List<Task> ReadTasksFromFile(User user)
    {
        tasks = new List<Task>();
        errors = new List<string>();
        
        List<string> linesOfLoadedFile = ListLinesOfLoadedFile();
        
        processLines(linesOfLoadedFile);

        MakeErrorFile($"../../../Data/Out/ErroresImport-{user.Name}.txt");
        return tasks;
    }*/

    private void ProcessLines(List<string> linesOfLoadedFile)
    {
        foreach (string line in linesOfLoadedFile)
        {
            if (IsLineValid(line))
            {
                List<string> splitLine = SplitString(line);
                Task newTask = TaskFromStringList(splitLine);
                tasks.Add(newTask);
            }
            else
            {
                ProcessError(line);
            }
        }
    }

    private void MakeErrorFile(string errorFileName)
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
        List<string> linesOfContent = content.Split('\n').ToList();

        ProcessLines(linesOfContent);
        
        MakeErrorFile($"ErroresImport-{user.Name}.txt");
        return tasks;
    }
    
    private void ProcessError(string line)
    {
        List<string> separatedLine = SplitString(line);
        bool correctColumnAmount = separatedLine.Count == 4;

        if (!correctColumnAmount)
        {
            LogError(line,"Cantidad incorrecta de columnas.");
            return;
        }
        
        if (!StringIsValidDate(separatedLine[2]))
        {
            LogError(line,"Formato de fecha incorrecto.");
            return;
        }
        
        if (!IsPanelIdValid(separatedLine[3]))
        {
            LogError(line,"Id de panel incorrecto.");
            return;
        }
    }

    private void LogError(string line, string errorMessage)
    {
        string currentTime = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");
        string error = $"{line} --- {errorMessage} --- {currentTime}";
        errors.Add(error);
    }
    
    private List<string> SplitString(string str)
    {
        string[] strArr = str.Split(",");
        
        List<string> toReturn = strArr.ToList();
        
        return toReturn;
    }
    private Task TaskFromStringList(List<string> strList)
    {
        Task task = new Task();
        task.Title = strList[0];
        task.Description = strList[1];
        
        string strDate = strList[2];
        
        task.ExpirationDate = StringToDate(strDate);

        return task;
    }
    private DateTime StringToDate(string strDate)
    {
        string[] dateArray = strDate.Split("/");
        DateTime date = new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[1]), int.Parse(dateArray[0]));
        
        return date;
    }


    private bool IsLineValid(string line)
    {
        List<string> elements = SplitString(line);
        if (elements.Count != 4)
            return false;
        
        bool isDateValid = StringIsValidDate(elements[2]);
        bool isPanelIdValid = IsPanelIdValid(elements[3]);
        
        return isDateValid && isPanelIdValid;
    }
    private bool IsPanelIdValid(string panelId)
    {
        return int.TryParse(panelId, out int _);
    }
    private bool StringIsValidDate(string str)
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
    private bool AreNumbersValid(int day, int month, int year)
    {
        bool isDayValid = day >= 1 && day <= 31;
        bool isMonthValid = month >= 1 && month <= 12;
        bool isYearValid = year >= 2000 && year <= DateTime.Now.Year;
        
        return isDayValid && isMonthValid && isYearValid;
    }

}