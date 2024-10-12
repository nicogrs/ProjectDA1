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
    private string fileName1;
    private string fileName2;
    private string directoryAdjustment;
    private string adjustedFileName1;
    private List<Task> tasks;
    
    
    [TestInitialize]
    public void Setup()
    {
        line0 = "Título,Descripción,Fecha de vencimiento,ID de panel";
        line1 = "Contactar al cliente,Contactar al cliente para actualizar el estado del proyecto.,10/09/2024,1";
        line2 = "Pagar proveedores,Revisar planilla de proveedores y pagar.,19/09/2024,1";
        fileName1 = "Data/tareasTestData.csv";
        fileName2 = "Data/tareas.csv";
        directoryAdjustment = "../../../";
        fileName1 = directoryAdjustment + fileName1;
        fileName2 = directoryAdjustment + fileName2;
        tasks = new List<Task>()
        {
            new Task()
            {
                Title = "Prueba correcta 1",
                Description = "Descripcion de prueba 1.",
                expDate = new DateTime(2024, 01, 01)
            },
            new Task()
            {
                Title = "Prueba correcta 2",
                Description = "Descripcion de prueba 2.",
                expDate = new DateTime(2024, 02, 02)
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
    public void ValidDateTest1()
    {
        string testDate = "01/10/2024";
        Assert.IsTrue(TaskImport.StringIsValidDate(testDate));
    }
    
    [TestMethod]
    public void ValidDateTest2()
    {
        string testDate = "32/10/2024";
        Assert.IsFalse(TaskImport.StringIsValidDate(testDate));
    }
    
    [TestMethod]
    public void ValidDateTest3()
    {
        string testDate = "01/13/2024";
        Assert.IsFalse(TaskImport.StringIsValidDate(testDate));
    }
    
    [TestMethod]
    public void ValidDateTest4()
    {
        string testDate = "01/10/-2024";
        Assert.IsFalse(TaskImport.StringIsValidDate(testDate));
    }
    
    [TestMethod]
    public void ValidDateTest5()
    {
        string testDate = "01/10/20/24";
        Assert.IsFalse(TaskImport.StringIsValidDate(testDate));
    }
    
    [TestMethod]
    public void ValidDateTest6()
    {
        string testDate = "01/asd/24";
        Assert.IsFalse(TaskImport.StringIsValidDate(testDate));
    }

    [TestMethod]
    public void IsLineValidTest1()
    {
        Assert.IsFalse(TaskImport.IsLineValid(line0));
        Assert.IsTrue(TaskImport.IsLineValid(line1));
        Assert.IsTrue(TaskImport.IsLineValid(line2));
    }
    
    [TestMethod]
    public void IsLineValidTest2()
    {
        string line = "Titulo X,Descripcion X.,10/09/2024,Manzana";
        Assert.IsFalse(TaskImport.IsLineValid(line));
    }
    
    [TestMethod]
    public void IsLineValidTest3()
    {
        string line = "Titulo X,Descripcion X.,fecha: 10/09/2024,1";
        Assert.IsFalse(TaskImport.IsLineValid(line));
    }
    
    [TestMethod]
    public void IsLineValidTest4()
    {
        string line = "Titulo X,Descripcion X.,1,10/09/2024";
        Assert.IsFalse(TaskImport.IsLineValid(line));
    }

    [TestMethod]
    public void LoadFileTest1()
    {
        string fileName = directoryAdjustment + fileName1;
        
        taskImport.LoadFile(fileName);

        Assert.AreEqual(fileName, taskImport.fileName);
    }

    [TestMethod]
    public void ReadFileTest1()
    {
        taskImport.LoadFile(fileName1);
        List<string> lines = taskImport.ListLinesOfLoadedFile();
        
        Assert.AreEqual("Título,Descripción,Fecha de vencimiento,ID de panel",lines[0]);
        Assert.AreEqual("Prueba correcta 1,Descripcion de prueba 1.,01/01/2024,1",lines[1]);
        Assert.AreEqual("Prueba correcta 2,Descripcion de prueba 2.,02/02/2024,1",lines[2]);
        Assert.AreEqual("Prueba incorrecta 1,Tiene una columna sobrante,Columna sobrante,03/03/2024,1",lines[3]);
        Assert.AreEqual("Prueba incorrecta 2,La fecha es incorrecta.,13/04/2024,1",lines[4]);
        Assert.AreEqual("Prueba incorrecta 3,No hay columna ID de panel,04/04/2024",lines[5]);
    }

    [TestMethod]
    public void ReadTasksFromFileTest1()
    {
        taskImport.LoadFile(fileName1);

        List<Task> taskList = taskImport.ReadTasksFromFile(new User());

        int taskListElementCount = taskList.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(taskList[i].Title, tasks[i].Title);
            Assert.AreEqual(taskList[i].Description, tasks[i].Description);
            Assert.AreEqual(taskList[i].expDate, tasks[i].expDate);
        }
    }
}