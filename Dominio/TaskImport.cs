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
        
        string[] dateArray = strList[2].Split("/");
        
        task.expDate = new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[1]), int.Parse(dateArray[0]));

        return task;
    }
}