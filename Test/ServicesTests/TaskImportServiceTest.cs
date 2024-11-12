using Dominio;
using Dominio.Services;

namespace Test.ServicesTests;

using Task = Dominio.Task;

[TestClass]
public class TaskImportServiceTest
{
    private CsvReader _csvReader;
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
            "tareas.xlsx"
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

    [TestMethod]
    public void ReadXlsxTest()
    {
        XlsReader xlsxReader = new XlsReader();
        string expectedResult =
            "Título,Descripción,Fecha de vencimiento,ID de panel,ID de epica\n" +
            "Contactar al cliente,Contactar al cliente para actualizar el estado del proyecto.,10/09/2024,1,1\n" +
            "Pagar proveedores,Revisar planilla de proveedores y pagar.,19/09/2024,1,1\n" +
            "Terminar obligatorio,Terminar el obligatorio 2 de DA.,20/11/2024,1,2\n" +
            "Comprar mesa ping pong,Comprar mesa para la sala de espera.,24/12/2024,2";
        
        string result = xlsxReader.ConvertXlsFileContentToCsv(xlsxFilesToTest[0]);
        
        Assert.AreEqual(expectedResult, result);
    }
}