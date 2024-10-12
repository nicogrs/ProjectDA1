namespace Dominio;

public class TaskImport
{
    public string fileName;
    public StreamReader reader;

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
    public static List<string> SplitString(string str)
    {
        string[] strArr = str.Split(",");
        
        List<string> toReturn = strArr.ToList();
        
        return toReturn;
    }

    public static bool IsLineValid(string line)
    {
        List<string> elements = SplitString(line);
        
        bool isDateValid = StringIsValidDate(elements[2]);
        bool isPanelIdValid = IsPanelIdValid(elements[3]);
        
        return isDateValid && isPanelIdValid;
    }

    private static bool IsPanelIdValid(string panelId)
    {
        return int.TryParse(panelId, out int _);
    }
    public static Task TaskFromStringList(List<string> strList)
    {
        Task task = new Task();
        task.Title = strList[0];
        task.Description = strList[1];
        
        string strDate = strList[2];
        
        task.expDate = StringToDate(strDate);

        return task;
    }

    private static DateTime StringToDate(string strDate)
    {
        string[] dateArray = strDate.Split("/");
        DateTime date = new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[1]), int.Parse(dateArray[0]));
        
        return date;
    }

    public static bool StringIsValidDate(string str)
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

    private static bool AreNumbersValid(int day, int month, int year)
    {
        bool isDayValid = day >= 1 && day <= 31;
        bool isMonthValid = month >= 1 && month <= 12;
        bool isYearValid = year >= 2000 && year <= DateTime.Now.Year;
        
        return isDayValid && isMonthValid && isYearValid;
    }
}