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
    
    [TestInitialize]
    public void Setup()
    {
        line0 = "Título,Descripción,Fecha de vencimiento,ID de panel";
        line1 = "Contactar al cliente,Contactar al cliente para actualizar el estado del proyecto.,10/09/2024,1";
        line2 = "Pagar proveedores,Revisar planilla de proveedores y pagar.,19/09/2024,1";
        fileName1 = "Data/tareasTestData.csv";
        fileName2 = "Data/tareas.csv";
        directoryAdjustment = "../../../";
        
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
        taskImport = new TaskImport();
        string fileName = directoryAdjustment + fileName1;
        
        taskImport.LoadFile(fileName);

        Assert.AreEqual(fileName, taskImport.fileName);
    }
}