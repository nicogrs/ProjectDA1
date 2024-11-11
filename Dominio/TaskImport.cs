namespace Dominio;

public class TaskImport
{
    private List<string> errors;
    private StreamReader reader;
    private List<Task> tasks;
    private StreamWriter writer;


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
        foreach (var line in linesOfLoadedFile)
            if (IsLineValid(line))
            {
                var splitLine = SplitString(line);
                var newTask = TaskFromStringList(splitLine);
                tasks.Add(newTask);
            }
            else
            {
                ProcessError(line);
            }
    }

    private void MakeErrorFile(string errorFileName)
    {
        //directorio interfaz 
        var directory = Directory.GetCurrentDirectory();
        directory = Directory.GetParent(directory).FullName;

        var filePath = Path.Combine(directory, errorFileName);

        /*if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }*/

        writer = new StreamWriter(filePath);
        foreach (var line in errors) writer.WriteLine(line);
        writer.Close();
    }

    public List<Task> ReadTasksFromContent(string content, User user)
    {
        tasks = new List<Task>();
        errors = new List<string>();
        var linesOfContent = content.Split('\n').ToList();

        ProcessLines(linesOfContent);

        MakeErrorFile($"ErroresImport-{user.Name}.txt");
        return tasks;
    }

    private void ProcessError(string line)
    {
        var separatedLine = SplitString(line);
        var correctColumnAmount = separatedLine.Count == 4;

        if (!correctColumnAmount)
        {
            LogError(line, "Cantidad incorrecta de columnas.");
            return;
        }

        if (!StringIsValidDate(separatedLine[2]))
        {
            LogError(line, "Formato de fecha incorrecto.");
            return;
        }

        if (!IsPanelIdValid(separatedLine[3])) LogError(line, "Id de panel incorrecto.");
    }

    private void LogError(string line, string errorMessage)
    {
        var currentTime = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");
        var error = $"{line} --- {errorMessage} --- {currentTime}";
        errors.Add(error);
    }

    private List<string> SplitString(string str)
    {
        var strArr = str.Split(",");

        var toReturn = strArr.ToList();

        return toReturn;
    }

    private Task TaskFromStringList(List<string> strList)
    {
        var task = new Task();
        task.Name = strList[0];
        task.Description = strList[1];

        var strDate = strList[2];

        task.ExpirationDate = StringToDate(strDate);

        return task;
    }

    private DateTime StringToDate(string strDate)
    {
        var dateArray = strDate.Split("/");
        var date = new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[1]), int.Parse(dateArray[0]));

        return date;
    }


    private bool IsLineValid(string line)
    {
        var elements = SplitString(line);
        if (elements.Count != 4)
            return false;

        var isDateValid = StringIsValidDate(elements[2]);
        var isPanelIdValid = IsPanelIdValid(elements[3]);

        return isDateValid && isPanelIdValid;
    }

    private bool IsPanelIdValid(string panelId)
    {
        return int.TryParse(panelId, out var _);
    }

    private bool StringIsValidDate(string str)
    {
        if (!str.Contains('/'))
            return false;

        var dateArray = str.Split("/");

        if (dateArray.Length != 3)
            return false;

        int day, month, year;
        var isDayNumber = int.TryParse(dateArray[0], out day);
        var isMonthNumber = int.TryParse(dateArray[1], out month);
        var isYearNumber = int.TryParse(dateArray[2], out year);
        var areNumbers = isDayNumber && isMonthNumber && isYearNumber;

        if (!areNumbers)
            return false;

        var areNumbersValid = AreNumbersValid(day, month, year);

        return areNumbersValid;
    }

    private bool AreNumbersValid(int day, int month, int year)
    {
        var isDayValid = day >= 1 && day <= 31;
        var isMonthValid = month >= 1 && month <= 12;
        var isYearValid = year >= 2000 && year <= DateTime.Now.Year;

        return isDayValid && isMonthValid && isYearValid;
    }
}