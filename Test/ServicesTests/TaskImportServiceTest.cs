using Dominio;
using Services;

namespace Test.ServicesTests;

using Task = Dominio.Task;

[TestClass]
public class TaskImportServiceTest
{
    private TaskImportService _taskImportService;
    private List<string> filesToTest;
    private List<string> xlsxFilesToTest;
    private List<Task> referenceTasks1;
    private List<Task> referenceTasks2;
    private List<string> errorLinesToProcess;
    private string referenceContent;
    
    
    [TestInitialize]
    public void Setup()
    {
        errorLinesToProcess = new List<string>()
        {
            //Título,Descripción,Fecha de vencimiento,ID de panel,ID de epica,Esfuerzo Estimado
            "correcta,descripcion,31/12/2025,1,1,1",
            "incorrecta,menos columnas,31/12/2025",
            "incorrecta,columnas de mas,31/12/2025,1,1,1,2",
            "incorrecta,Fecha incorrecta,31/1/2/2025,1,1,5",
            "incorrecta,Id panel incorrecto,31/12/2025,one,1,3",
            "incorrecta,Formato de esfuerzo inc.,31/12/2025,1,1,cinco.",
            "incorrecta,Formato de Id epica inc.,31/12/2025,1,siete,5",
        };
        
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

        
        referenceTasks1 = new List<Task>()
        {
            new Task()
            {
                Name = "Tarea Valida 1",
                Description = "Datos validos.",
                ExpirationDate = new DateTime(2000, 01, 01),
                PanelId = 1
            },
            new Task()
            {
                Name = "Tarea Valida 2",
                Description = "Datos validos.",
                ExpirationDate = new DateTime(2024, 12, 12),
                PanelId = 1
            },
            new Task()
            {
                Name = "Tarea Valida 3",
                Description = "Datos validos - final.",
                ExpirationDate = new DateTime(2020, 06, 06),
                PanelId = 1
            }
        };

        referenceTasks2 = new List<Task>()
        {
            new Task()
            {
                Name = "Contactar al cliente",
                Description = "Contactar al cliente para actualizar el estado del proyecto.",
                ExpirationDate = new DateTime(2025, 09, 10),
                ExpectedEffort = 1
            },
            new Task()
            {
                Name = "Pagar proveedores",
                Description = "Revisar planilla de proveedores y pagar.",
                ExpirationDate = new DateTime(2025, 09, 19),
                ExpectedEffort = 2
            },
            new Task()
            {
                Name = "Terminar obligatorio",
                Description = "Terminar el obligatorio 2 de DA.",
                ExpirationDate = new DateTime(2025, 11, 20),
                ExpectedEffort = 3
            },
            new Task()
            {
                Name = "Comprar mesa ping pong",
                Description = "Comprar mesa para la sala de espera.",
                ExpirationDate = new DateTime(2025, 12, 24),
                ExpectedEffort = 2
            }
        };
        _taskImportService = new TaskImportService();
        
        referenceContent =
            "Título,Descripción,Fecha de vencimiento,ID de panel,ID de epica,Esfuerzo Estimado\r\n" +
            "Contactar al cliente,Contactar al cliente para actualizar el estado del proyecto.,10/09/2025,1,1,1\r\n" +
            "Pagar proveedores,Revisar planilla de proveedores y pagar.,19/09/2025,1,1,2\r\n" +
            "Terminar obligatorio,Terminar el obligatorio 2 de DA.,20/11/2025,1,2,3\r\n" +
            "Comprar mesa ping pong,Comprar mesa para la sala de espera.,24/12/2025,2,,2\r\n";
    }
    

    [TestMethod]
    public void ReadTasksFromContentTest1()
    {
        string content =
            $"{referenceTasks1[0].Name},{referenceTasks1[0].Description},{referenceTasks1[0].ExpirationDate.ToShortDateString()}," +
            $"{referenceTasks1[0].PanelId},,{referenceTasks1[0].ExpectedEffort}\n" +
            $"{referenceTasks1[1].Name},{referenceTasks1[1].Description},{referenceTasks1[1].ExpirationDate.ToShortDateString()}," +
            $"{referenceTasks1[1].PanelId},,{referenceTasks1[1].ExpectedEffort}\n" +
            $"{referenceTasks1[2].Name},{referenceTasks1[2].Description},{referenceTasks1[2].ExpirationDate.ToShortDateString()}," +
            $"{referenceTasks1[2].PanelId},,{referenceTasks1[2].ExpectedEffort}\n";
        
        List<TaskImportService.ImportedTask> tasks = _taskImportService.ReadTasksFromContent(content,new User(){Name = "User 1"});

        int taskListElementCount = tasks.Count;
        for (int i = 0; i < taskListElementCount; i++)
        {
            Assert.AreEqual(tasks[i].Name, referenceTasks1[i].Name);
            Assert.AreEqual(tasks[i].Description, referenceTasks1[i].Description);
            Assert.AreEqual(tasks[i].ExpirationDate, referenceTasks1[i].ExpirationDate);
            Assert.AreEqual(tasks[i].ExpectedEffort, referenceTasks1[i].ExpectedEffort);
        }
    }
    
    [TestMethod]
    public void ReadTasksFromContentTest2()
    {
        string content =
            $"{referenceTasks1[0].Name},{referenceTasks1[0].Description},{referenceTasks1[0].ExpirationDate.ToShortDateString()}," +
            $"{referenceTasks1[0].PanelId},1,{referenceTasks1[0].ExpectedEffort}";
        
        List<TaskImportService.ImportedTask> tasks = _taskImportService.ReadTasksFromContent(content,new User(){Name = "User 1"});

        Assert.AreEqual(tasks[0].epicId,1);
        Assert.IsTrue(tasks[0].IsInEpic);
    }

    [TestMethod]
    public void ReadXlsxTest()
    {
        XlsxToCsvAdapter xslxAdapter = new XlsxToCsvAdapter();
        List<string> expectedList = referenceContent.Split("\r\n").ToList();
        
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

    [TestMethod]
    public void IsStringValidDateTest1()
    {
        List<string> validDates = new List<string>()
        {
            "01/01/2024",
            "01/01/2000",
            "31/12/2024",
            "07/08/2024"
        };

        foreach (string date in validDates)
        {
            Assert.IsTrue(_taskImportService.IsStringValidDate(date));
        }
    }

    [TestMethod]
    public void IsStringValidDateTest2()
    {
        List<string> invalidDates = new List<string>()
        {
            "00/01/2024",
            "32/01/2024",
            "31/12/1999",
            "01/00/2000",
            "01/13/2000",
            "-01/01/2024"
        };
        
        foreach (string date in invalidDates)
        {
            Assert.IsFalse(_taskImportService.IsStringValidDate(date));
        }
    }
    
    [TestMethod]
    public void IsStringValidDateTest3()
    {
        List<string> invalidDates = new List<string>()
        {
            "01/01/01/2024",
            "01/02",
            "01",
            "01-01-2024",
            "one/01/2024",
            "01/january/2024",
            "01/01/twenty-twenty-two"
        };
        
        foreach (string date in invalidDates)
        {
            Assert.IsFalse(_taskImportService.IsStringValidDate(date));
        }
    }

    [TestMethod]
    public void ProcessErrorTest1()
    {
        _taskImportService.errors = new List<string>();
        
        _taskImportService.ProcessError(errorLinesToProcess[0]);
        
        Assert.AreEqual(0, _taskImportService.errors.Count);
    }
    
    [TestMethod]
    public void ProcessErrorTest2()
    {
        _taskImportService.errors = new List<string>();
        string expectedErrorMessage = "Cantidad incorrecta de columnas (3).";
        
        _taskImportService.ProcessError(errorLinesToProcess[1]);
        
        Assert.AreEqual(1, _taskImportService.errors.Count);
        bool contieneError = _taskImportService.errors[0].Contains(expectedErrorMessage);
        Assert.IsTrue(contieneError);
    }
    
    [TestMethod]
    public void ProcessErrorTest3()
    {
        _taskImportService.errors = new List<string>();
        string expectedErrorMessage = "Cantidad incorrecta de columnas (7).";
        
        _taskImportService.ProcessError(errorLinesToProcess[2]);
        
        Assert.AreEqual(1, _taskImportService.errors.Count);
        bool contieneError = _taskImportService.errors[0].Contains(expectedErrorMessage);
        Assert.IsTrue(contieneError);
    }
    
    [TestMethod]
    public void ProcessErrorTest4()
    {
        _taskImportService.errors = new List<string>();
        string expectedErrorMessage = "Formato de fecha incorrecto.";
        
        _taskImportService.ProcessError(errorLinesToProcess[3]);
        
        Assert.AreEqual(1, _taskImportService.errors.Count);
        bool contieneError = _taskImportService.errors[0].Contains(expectedErrorMessage);
        Assert.IsTrue(contieneError);
    }
    
    [TestMethod]
    public void ProcessErrorTest5()
    {
        _taskImportService.errors = new List<string>();
        string expectedErrorMessage = "Id de panel incorrecto.";
        
        _taskImportService.ProcessError(errorLinesToProcess[4]);
        
        Assert.AreEqual(1, _taskImportService.errors.Count);
        bool contieneError = _taskImportService.errors[0].Contains(expectedErrorMessage);
        Assert.IsTrue(contieneError);
    }
    
    [TestMethod]
    public void ProcessErrorTest6()
    {
        _taskImportService.errors = new List<string>();
        string expectedErrorMessage = "Error en formato de esfuerzo esperado.";
        
        _taskImportService.ProcessError(errorLinesToProcess[5]);
        
        Assert.AreEqual(1, _taskImportService.errors.Count);
        bool contieneError = _taskImportService.errors[0].Contains(expectedErrorMessage);
        Assert.IsTrue(contieneError);
    }
    
    [TestMethod]
    public void ProcessErrorTest7()
    {
        _taskImportService.errors = new List<string>();
        string expectedErrorMessage = "Error en formato de Id de Epica";
        
        _taskImportService.ProcessError(errorLinesToProcess[6]);
        
        Assert.AreEqual(1, _taskImportService.errors.Count);
        bool contieneError = _taskImportService.errors[0].Contains(expectedErrorMessage);
        Assert.IsTrue(contieneError);
    }

    [TestMethod]
    public void RemoveTimeFromDateTimeTest()
    {
        XlsxToCsvAdapter adapter = new XlsxToCsvAdapter();
        string originalDateTime = "01/01/2020 0:00:00";
        
        string expectedDate = adapter.RemoveTimeFromDateTime(originalDateTime);
        
        Assert.AreEqual(expectedDate, "01/01/2020");
    }

    [TestMethod]
    public void ImportedTaskTest()
    {
        TaskImportService.ImportedTask t = new TaskImportService.ImportedTask()
        {
            epicId = 0
        };
        
        Assert.AreEqual(t.epicId,0);
    }
}