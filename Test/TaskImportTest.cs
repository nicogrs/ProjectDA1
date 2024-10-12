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

        referenceTasks = new List<Task>()
        {
            new Task()
            {
                Title = "Tarea Valida 1",
                Description = "Datos validos.",
                expDate = new DateTime(2000, 01, 01)
            },
            new Task()
            {
                Title = "Tarea Valida 2",
                Description = "Datos validos.",
                expDate = new DateTime(2024, 12, 12)
            },
            new Task()
            {
            Title = "Tarea Valida 3",
            Description = "Datos validos - final.",
            expDate = new DateTime(2020, 06, 06)
        }
        };
        taskImport = new TaskImport();
    }
    
    [TestMethod]
    public void LoadFileTest1()
    {
        taskImport.LoadFile(filesToTest[0]);

        Assert.AreEqual(filesToTest[0], taskImport.fileName);
    }

    [TestMethod]
    public void ReadTasksFromFileTest1()
    {
        taskImport.LoadFile(filesToTest[0]);

        List<Task> taskList = taskImport.ReadTasksFromFile(new User(){Name = "Testuser 1"});

        int taskListElementCount = taskList.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(taskList[i].Title, referenceTasks[i].Title);
            Assert.AreEqual(taskList[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(taskList[i].expDate, referenceTasks[i].expDate);
        }
    }

    [TestMethod]
    public void ReadTasksValidDateTests()
    {
        taskImport.LoadFile(filesToTest[2]);

        List<Task> taskList = taskImport.ReadTasksFromFile(new User(){Name = "Testuser 2"});
        
        int taskListElementCount = taskList.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(taskList[i].Title, referenceTasks[i].Title);
            Assert.AreEqual(taskList[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(taskList[i].expDate, referenceTasks[i].expDate);
        }
    }
    
    [TestMethod]
    public void TasksValidLineTests()
    {
        taskImport.LoadFile(filesToTest[3]);

        List<Task> taskList = taskImport.ReadTasksFromFile(new User(){Name = "Testuser 3"});
        
        int taskListElementCount = taskList.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(taskList[i].Title, referenceTasks[i].Title);
            Assert.AreEqual(taskList[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(taskList[i].expDate, referenceTasks[i].expDate);
        }
    }
}