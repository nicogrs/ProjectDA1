namespace Dominio;

public class User
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
    public bool Admin { get; set; }
    public Trash PaperBin { get; set; }

    public User()
    {
        PaperBin = new Trash();
    }
    
    public bool IsNameValid()
    {
        return Name != null && Name.Length > 2 && Name.Length < 100;
    }

    
    public bool IsSurnameValid()
    {
        return Surname != null && Surname.Length > 2 && Surname.Length < 100;
    }

    public bool IsEmailValid()
    {
        return Email != null && Email.Contains('@') && Email.Length > 2 && Email.Length < 100;
    }

    public bool IsBirthDateValid()
    {
        return BirthDate != null && BirthDate < DateTime.Today && BirthDate.Year < DateTime.Now.Year;
    }
    
    public bool IsUserValid()
    {
        return IsNameValid() && IsSurnameValid() && IsEmailValid() && IsBirthDateValid();
    }
    public bool IsUserAdmin()
    {
        return Admin;
    }
    
}