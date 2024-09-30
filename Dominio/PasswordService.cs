namespace Dominio;

public class PasswordService
{
    public bool IsPasswordValid(string password)
    {
        bool validLength = password.Length >= 8;
        bool hasUpperCase = password.Any(x => char.IsUpper(x));
        bool hasLowerCase = password.Any(x => char.IsLower(x));
        bool hasDigit = password.Any(x => char.IsDigit(x));
        bool hasSymbol = password.Any(x => char.IsSymbol(x));
        return validLength && hasUpperCase && hasLowerCase && hasDigit && hasSymbol;
    }

    public string GenerateRandomPassword()
    {
        return "";
    }
}