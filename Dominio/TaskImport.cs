namespace Dominio;

public class TaskImport
{
    public static List<string> SplitString(string str)
    {
        string[] strArr = str.Split(",");
        
        List<string> toReturn = strArr.ToList();
        
        return toReturn;
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
        int day, month, year;
        string[] dateArray = str.Split("/");
        
        bool isDayNumber = int.TryParse(dateArray[0], out day);
        bool isMonthNumber = int.TryParse(dateArray[1], out month);
        bool isYearNumber = int.TryParse(dateArray[2], out year);
        bool areNumbers = isDayNumber && isMonthNumber && isYearNumber;
        
        if (!areNumbers)
            return false;
        
        bool isDayValid = day >= 1 && day <= 31;
        bool isMonthValid = month >= 1 && month <= 12;
        bool isYearValid = year >= 2000 && year <= DateTime.Now.Year;
        bool areNumbersValid = isDayValid && isMonthValid && isYearValid;
        
        return areNumbers && areNumbersValid;
    }
}