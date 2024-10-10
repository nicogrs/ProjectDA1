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
        return null;
    }
}