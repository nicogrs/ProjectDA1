using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Dominio;
using Dominio.Services;

namespace Test.ServicesTests;

using Task = Dominio.Task;

[TestClass]
public class TaskImportServiceTest
{
    private TaskImportService _taskImportService;
    private List<string> filesToTest;
    private List<string> xlsxFilesToTest;
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
        xlsxFilesToTest = new List<string>()
        {
            "../../../Data/tareas.xlsx"
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
        _taskImportService = new TaskImportService();
    }
    

    

    [TestMethod]
    public void ReadTasksFromContentTest()
    {
        string content = $"{referenceTasks[0].Title},{referenceTasks[0].Description},{referenceTasks[0].ExpirationDate.ToShortDateString()},1\n" +
                         $"{referenceTasks[1].Title},{referenceTasks[1].Description},{referenceTasks[1].ExpirationDate.ToShortDateString()},1\n" +
                         $"{referenceTasks[2].Title},{referenceTasks[2].Description},{referenceTasks[2].ExpirationDate.ToShortDateString()},1";
        
        List<Task> tasks = _taskImportService.ReadTasksFromContent(content,new User(){Name = "User 1"});

        int taskListElementCount = tasks.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(tasks[i].Title, referenceTasks[i].Title);
            Assert.AreEqual(tasks[i].Description, referenceTasks[i].Description);
            Assert.AreEqual(tasks[i].ExpirationDate, referenceTasks[i].ExpirationDate);
        }
    }

    [TestMethod]
    public void ReadXlsxTest()
    {
        XlsxToCsvAdapter xslxAdapter = new XlsxToCsvAdapter();
        string expectedResult =
            "Título,Descripción,Fecha de vencimiento,ID de panel,ID de epica\r\n" +
            "Contactar al cliente,Contactar al cliente para actualizar el estado del proyecto.,10/09/2025 0:00:00,1,1\r\n" +
            "Pagar proveedores,Revisar planilla de proveedores y pagar.,19/09/2025 0:00:00,1,1\r\n" +
            "Terminar obligatorio,Terminar el obligatorio 2 de DA.,20/11/2025 0:00:00,1,2\r\n" +
            "Comprar mesa ping pong,Comprar mesa para la sala de espera.,24/12/2025 0:00:00,2,\r\n";
        List<string> expectedList = expectedResult.Split("\r\n").ToList();
        
        string result = xslxAdapter.TranslateXlsxToCsv(xlsxFilesToTest[0]);
        List<string> actualList = result.Split("\r\n").ToList();

        for (int i = 0; i < actualList.Count; i++)
        {
            Assert.AreEqual(expectedList[i], actualList[i]);
        }
    }

    [TestMethod]
    public void SplitLinesTest()
    {
        string combinedString = "1,2,3,4,5,6,7,8,9,10";
        List<string> expectedResult = new List<string>();
        for (int i = 1; i <= 10; i++)
        {
            expectedResult.Add(i.ToString());
        }

        List<string> result = TaskImportService.SplitLine(combinedString);
        
        Assert.AreEqual(expectedResult.Count, result.Count);
        for (int i = 0; i < result.Count; i++)
        {
            Assert.AreEqual(expectedResult[i], result[i]);
        }
    }

    [TestMethod]
    public void LogErrorTest()
    {
        _taskImportService.errors = new List<string>();
        DateTime errorTime = DateTime.Now;
        string expectedResult = $"test line --- error message --- {errorTime:yyyy-mm-dd HH:mm:ss}";
        
        _taskImportService.LogError("test line", "error message");
        
        Assert.AreEqual(expectedResult, _taskImportService.errors[0]);
        Assert.AreEqual(1, _taskImportService.errors.Count);
    }
}