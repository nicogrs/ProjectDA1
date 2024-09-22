namespace Dominio;

public class User
{
    public string Nickname { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }

    public User getUserByNickname(string nickname)
    {
        return this;
    }
    
}