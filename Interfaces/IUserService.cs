using Dominio;
using Task = Dominio.Task;

namespace Interfaces;

public interface IUserService
{
    public User GetUserByEmail(string email);
    public bool UpdateUser(User user);
    public string ResetUserPassword(string email);
    public List<User> GetAllUsers();
    public bool CreateUser(User user);
    public bool DeleteUser(string email);
    public List<IDeleteable> GetDeletedElements(string userEmail);
    public void AddElementToPaperBin(string email, IDeleteable element);
    public void DeleteElementFromPaperbin(string email, IDeleteable element);
    public void RestoreElement(int elementId, string email);
}