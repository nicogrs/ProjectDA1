namespace Dominio;

public class TaskImport
{
    public string fileName;
    private StreamReader reader;

    public void LoadFile(string _fileName)
    {
        fileName = _fileName;
        reader = new StreamReader(fileName);
    }
    public List<string> ListLinesOfLoadedFile()
    {
        List<string> result = new List<string>();
        string line;
        
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            result.Add(line);
        }
        
        return result;
    }

    public List<Task> ReadTasksFromFile(User user)
    {
        List<Task> result = new List<Task>();
        
        List<string> linesOfLoadedFile = ListLinesOfLoadedFile();
        
        foreach (string line in linesOfLoadedFile)
        {
            if (IsLineValid(line))
            {
                List<string> splitLine = SplitString(line);
                Task newTask = TaskFromStringList(splitLine);
                result.Add(newTask);
            }
        }
        
        return result;
    }
    
    private static List<string> SplitString(string str)
    {
        string[] strArr = str.Split(",");
        
        List<string> toReturn = strArr.ToList();
        
        return toReturn;
    }
    public Task TaskFromStringList(List<string> strList)
    {
        Task task = new Task();
        task.Title = strList[0];
        task.Description = strList[1];
        
        string strDate = strList[2];
        
        task.expDate = StringToDate(strDate);

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