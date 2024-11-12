using System.IO.Enumeration;
using Dominio;
using Dominio.Services;
using Microsoft.VisualBasic;

namespace Test;
using Dominio;
using Task = Dominio.Task;

[TestClass]
public class TaskImportServiceTest
{
    private CsvReader _csvReader;
    private List<string> filesToTest;
    private List<Task> referenceTasks;
    
    
    [TestInitialize]
    public void Setup()
    {
        string directory = Directory.GetCurrentDirectory();
        directory = $"../Data/{directory}";
        
        filesToTest = new List<string>()
        {
            "tareasTestData.csv",
            "Data/tareas.csv",
            "tareasValidDateTest.csv",
            "tareasValidLineTest.csv"
        };

        for (int i = 0; i < filesToTest.Count; i++)
        {
            filesToTest[i] = Path.Combine(directory, filesToTest[i]);
        }
        
        referenceTasks = new List<Task>()
        {
            new Task()
            {
                Title = "Tarea Valida 1",
                Description = "Datos validos.",
                ExpirationDate = new DateTime(2000, 01, 01)
            },
            new Task()
            {
                Title = "Tarea Valida 2",
                Description = "Datos validos.",
                ExpirationDate = new DateTime(2024, 12, 12)
            },
            new Task()
            {
            Title = "Tarea Valida 3",
            Description = "Datos validos - final.",
            ExpirationDate = new DateTime(2020, 06, 06)
        }
        };
        _csvReader = new CsvReader();
    }
    

    

    [TestMethod]
    public void ReadTasksFromContentTest()
    {
        string content = $"{referenceTasks[0].Title},{referenceTasks[0].Description},{referenceTasks[0].ExpirationDate.ToShortDateString()},1\n" +
                         $"{referenceTasks[1].Title},{referenceTasks[1].Description},{referenceTasks[1].ExpirationDate.ToShortDateString()},1\n" +
                         $"{referenceTasks[2].Title},{referenceTasks[2].Description},{referenceTasks[2].ExpirationDate.ToShortDateString()},1";
        
        List<Task> tasks = _csvReader.ReadTasksFromContent(content,new User(){Name = "User 1"});

        int taskListElementCount = tasks.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(tasks[i].Title, referenceTasks[i].Title);
            Assert.AreEqual(tasks[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(tasks[i].ExpirationDate, referenceTasks[i].ExpirationDate);
        }
    }
}