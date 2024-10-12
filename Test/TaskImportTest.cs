using Microsoft.VisualBasic;

namespace Test;
using Dominio;
using Task = Dominio.Task;

[TestClass]
public class TaskImportTest
{
    private string line0;
    private string line1;
    private string line2;
    private TaskImport taskImport;
    private List<string> filesToTest;
    private List<Task> referenceTasks;
    
    
    [TestInitialize]
    public void Setup()
    {
        line0 = "Título,Descripción,Fecha de vencimiento,ID de panel";
        line1 = "Contactar al cliente,Contactar al cliente para actualizar el estado del proyecto.,10/09/2024,1";
        line2 = "Pagar proveedores,Revisar planilla de proveedores y pagar.,19/09/2024,1";
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
            Description = "Datos validos, final.",
            expDate = new DateTime(2020, 06, 06)
        }
        };
        taskImport = new TaskImport();
    }

    [TestMethod]
    public void SplitStringTest1()
    {
        List<string> splitLine = TaskImport.SplitString(line0);
        
        Assert.AreEqual(splitLine.Count, 4);
        Assert.AreEqual(splitLine[0], "Título");
        Assert.AreEqual(splitLine[1], "Descripción");
        Assert.AreEqual(splitLine[2], "Fecha de vencimiento");
        Assert.AreEqual(splitLine[3], "ID de panel");
    }
    
    [TestMethod]
    public void SplitStringTest2()
    {
        List<string> splitLine = TaskImport.SplitString(line1);
        
        Assert.AreEqual(splitLine.Count, 4);
        Assert.AreEqual(splitLine[0], "Contactar al cliente");
        Assert.AreEqual(splitLine[1], "Contactar al cliente para actualizar el estado del proyecto.");
        Assert.AreEqual(splitLine[2], "10/09/2024");
        Assert.AreEqual(splitLine[3], "1");
    }
    
    [TestMethod]
    public void SplitStringTest3()
    {
        List<string> splitLine = TaskImport.SplitString(line2);
        
        Assert.AreEqual(splitLine.Count, 4);
        Assert.AreEqual(splitLine[0], "Pagar proveedores");
        Assert.AreEqual(splitLine[1], "Revisar planilla de proveedores y pagar.");
        Assert.AreEqual(splitLine[2], "19/09/2024");
        Assert.AreEqual(splitLine[3], "1");
    }

    [TestMethod]
    public void TaskFromStringListTest()
    {
        Task t = new Task()
        {
            Title = "Titulo 1",
            Description = "Descripcion 1",
            expDate = new DateTime(2024, 10, 01),
        };
        List<string> strList = new List<string>()
        {
            "Titulo 1",
            "Descripcion 1",
            "01/10/2024",
            "1"
        };


        Task parsedTask = TaskImport.TaskFromStringList(strList);
        
        Assert.AreEqual(t.Title, parsedTask.Title);
        Assert.AreEqual(t.Description, parsedTask.Description);
        Assert.AreEqual(t.expDate, parsedTask.expDate);
    }
    
    [TestMethod]
    public void LoadFileTest1()
    {
        taskImport.LoadFile(filesToTest[0]);

        Assert.AreEqual(filesToTest[0], taskImport.fileName);
    }

    [TestMethod]
    public void ReadFileTest1()
    {
        taskImport.LoadFile(filesToTest[0]);
        List<string> lines = taskImport.ListLinesOfLoadedFile();
        
        Assert.AreEqual("Título,Descripción,Fecha de vencimiento,ID de panel",lines[0]);
        Assert.AreEqual("Tarea Valida 1,Datos validos.,01/01/2000,1",lines[1]);
        Assert.AreEqual("Tarea Valida 2,Datos validos.,12/12/2024,1",lines[2]);
        Assert.AreEqual("Prueba incorrecta 1,Tiene una columna sobrante,Columna sobrante,03/03/2024,1",lines[3]);
        Assert.AreEqual("Prueba incorrecta 2,La fecha es incorrecta.,04/13/2024,1",lines[4]);
        Assert.AreEqual("Prueba incorrecta 3,No hay columna ID de panel,04/04/2024",lines[5]);
        Assert.AreEqual("Tarea Valida 3,Datos validos, final.,06/06/2020,1",lines[6]);
    }

    [TestMethod]
    public void ReadTasksFromFileTest1()
    {
        taskImport.LoadFile(filesToTest[0]);

        List<Task> taskList = taskImport.ReadTasksFromFile(new User());

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

        List<Task> taskList = taskImport.ReadTasksFromFile(new User());
        
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

        List<Task> taskList = taskImport.ReadTasksFromFile(new User());
        
        int taskListElementCount = taskList.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(taskList[i].Title, referenceTasks[i].Title);
            Assert.AreEqual(taskList[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(taskList[i].expDate, referenceTasks[i].expDate);
        }
    }
}