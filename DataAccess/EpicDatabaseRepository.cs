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

    public Epic Add(Epic oneElement)
    {
        throw new NotImplementedException();
    }

    public Epic? Find(Func<Epic, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IList<Epic> FindAll()
    {
        throw new NotImplementedException();
    }

    public Epic? Update(Epic updatedEntity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}