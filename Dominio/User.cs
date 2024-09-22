namespace Dominio;

public class User
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }

    public bool isNameValid()
    {
        return false;
    }
    
}