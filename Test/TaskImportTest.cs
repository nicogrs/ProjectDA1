namespace Test;
using Dominio;
using Task = Dominio.Task;

[TestClass]
public class TaskImportTest
{
    private string line0;
    private string line1;
    private string line2;
    
    [TestInitialize]
    public void Setup()
    {
        line0 = "Título,Descripción,Fecha de vencimiento,ID de panel";
        line1 = "Contactar al cliente,Contactar al cliente para actualizar el estado del proyecto.,10/09/2024,1";
        line2 = "Pagar proveedores,Revisar planilla de proveedores y pagar.,19/09/2024,1";
    }

    [TestMethod]
    public void splitStringTest()
    {
        List<string> splitLine = TaskImport.SplitString(line0);
        
        Assert.AreEqual(splitLine.Count, 4);
        Assert.AreEqual(splitLine[0], "Título");
        Assert.AreEqual(splitLine[1], "Descripción");
        Assert.AreEqual(splitLine[2], "Fecha de vencimiento");
        Assert.AreEqual(splitLine[3], "ID de panel");
    }
}