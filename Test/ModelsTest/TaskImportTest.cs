using System.IO.Enumeration;
using Microsoft.VisualBasic;

namespace Test;
using Dominio;
using Task = Dominio.Task;

[TestClass]
public class TaskImportTest
{
    private TaskImport taskImport;
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
                Name = "Tarea Valida 1",
                Description = "Datos validos.",
                ExpirationDate = new DateTime(2000, 01, 01)
            },
            new Task()
            {
                Name = "Tarea Valida 2",
                Description = "Datos validos.",
                ExpirationDate = new DateTime(2024, 12, 12)
            },
            new Task()
            {
            Name = "Tarea Valida 3",
            Description = "Datos validos - final.",
            ExpirationDate = new DateTime(2020, 06, 06)
        }
        };
        taskImport = new TaskImport();
    }
    

    

    [TestMethod]
    public void ReadTasksFromContentTest()
    {
        string content = $"{referenceTasks[0].Name},{referenceTasks[0].Description},{referenceTasks[0].ExpirationDate.ToShortDateString()},1\n" +
                         $"{referenceTasks[1].Name},{referenceTasks[1].Description},{referenceTasks[1].ExpirationDate.ToShortDateString()},1\n" +
                         $"{referenceTasks[2].Name},{referenceTasks[2].Description},{referenceTasks[2].ExpirationDate.ToShortDateString()},1";
        
        List<Task> tasks = taskImport.ReadTasksFromContent(content,new User(){Name = "User 1"});

        int taskListElementCount = tasks.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(tasks[i].Name, referenceTasks[i].Name);
            Assert.AreEqual(tasks[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(tasks[i].ExpirationDate, referenceTasks[i].ExpirationDate);
        }
    }
}