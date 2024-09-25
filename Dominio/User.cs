namespace Dominio;

public class User
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
    public bool Admin { get; set; }

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
    
    public bool IsPasswordValid()
    {
        bool validLength = Password.Length >= 8;
        bool hasUpperCase = Password.Any(x => char.IsUpper(x));
        bool hasLowerCase = Password.Any(x => char.IsLower(x));
        bool hasDigit = Password.Any(x => char.IsDigit(x));
        bool hasSymbol = Password.Any(x => char.IsSymbol(x));
        return validLength && hasUpperCase && hasLowerCase && hasDigit && hasSymbol;
    }
    
    public bool IsUserValid()
    {
        return IsNameValid() && IsSurnameValid() && IsEmailValid() && IsBirthDateValid() && IsPasswordValid();
    }
    public bool IsUserAdmin()
    {
        return Admin;
    }

}