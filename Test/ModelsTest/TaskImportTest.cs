using System.IO.Enumeration;
using Dominio;
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
        filesToTest = new List<string>()
        {
            "../../../Data/tareasTestData.csv",
            "../../../Data/tareas.csv",
            "../../../Data/tareasValidDateTest.csv",
            "../../../Data/tareasValidLineTest.csv"
        };
        
        Directory.CreateDirectory("../../../Data/Out/");

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
        taskImport = new TaskImport();
    }
    
    [TestMethod]
    public void LoadFileTest1()
    {
        taskImport.LoadFileFromPath(filesToTest[0]);

        Assert.AreEqual(filesToTest[0], taskImport.fileName);
    }

    [TestMethod]
    public void ReadTasksFromFileTest1()
    {
        taskImport.LoadFileFromPath(filesToTest[0]);

        List<Task> taskList = taskImport.ReadTasksFromFile(new User(){Name = "Testuser 1"});

        int taskListElementCount = taskList.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(taskList[i].Title, referenceTasks[i].Title);
            Assert.AreEqual(taskList[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(taskList[i].ExpirationDate, referenceTasks[i].ExpirationDate);
        }
    }

    [TestMethod]
    public void ReadTasksValidDateTests()
    {
        taskImport.LoadFileFromPath(filesToTest[2]);

        List<Task> taskList = taskImport.ReadTasksFromFile(new User(){Name = "Testuser 2"});
        
        int taskListElementCount = taskList.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(taskList[i].Title, referenceTasks[i].Title);
            Assert.AreEqual(taskList[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(taskList[i].ExpirationDate, referenceTasks[i].ExpirationDate);
        }
    }
    
    [TestMethod]
    public void TasksValidLineTests()
    {
        taskImport.LoadFileFromPath(filesToTest[3]);

        List<Task> taskList = taskImport.ReadTasksFromFile(new User(){Name = "Testuser 3"});
        
        int taskListElementCount = taskList.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(taskList[i].Title, referenceTasks[i].Title);
            Assert.AreEqual(taskList[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(taskList[i].ExpirationDate, referenceTasks[i].ExpirationDate);
        }
    }

    [TestMethod]
    public void ReadTasksFromContentTest()
    {
        string content = $"{referenceTasks[0].Title},{referenceTasks[0].Description},{referenceTasks[0].ExpirationDate.ToShortDateString()},1\n" +
                         $"{referenceTasks[1].Title},{referenceTasks[1].Description},{referenceTasks[1].ExpirationDate.ToShortDateString()},1\n" +
                         $"{referenceTasks[2].Title},{referenceTasks[2].Description},{referenceTasks[2].ExpirationDate.ToShortDateString()},1";
        
        List<Task> tasks = taskImport.ReadTasksFromContent(content,new User(){Name = "User 1"});

        int taskListElementCount = tasks.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(tasks[i].Title, referenceTasks[i].Title);
            Assert.AreEqual(tasks[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(tasks[i].ExpirationDate, referenceTasks[i].ExpirationDate);
        }
    }
}