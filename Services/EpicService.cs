using Dominio;
using Interfaces;

namespace Services;

public class EpicService
{
    private readonly IRepository<Epic> _epicDatabase;

    public EpicService(IRepository<Epic> epicDatabase)
    {
        _epicDatabase = epicDatabase;
    }
}