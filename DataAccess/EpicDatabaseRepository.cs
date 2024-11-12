using Dominio;
using Interfaces;

namespace DataAccess;

public class EpicDatabaseRepository : IRepository<Epic>
{
    private SqlContext _context;
    public EpicDatabaseRepository(SqlContext context)
    {
        _context = context;
    }

    public Epic Add(Epic epic)
    {
       _context.Epic.Add(epic);
       _context.SaveChanges();
       return epic;
    }

    public Epic? Find(Func<Epic, bool> filter)
    {
        return _context.Epic.Where(filter).FirstOrDefault();
    }

    public IList<Epic> FindAll()
    {
        throw new NotImplementedException();
    }

    public Epic? Update(Epic updatedEntity)
    {
        _context.Epic.Update(updatedEntity);
        _context.SaveChanges();
        return updatedEntity;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}