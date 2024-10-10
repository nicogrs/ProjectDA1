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
        string[] dateArray = str.Split("/");
        
        bool isDayValid = int.TryParse(dateArray[0], out int day);
        bool isMonthValid = int.TryParse(dateArray[1], out int month);
        bool isYearValid = int.TryParse(dateArray[2], out int year);
        
        return isDayValid && isMonthValid && isYearValid;
    }
}