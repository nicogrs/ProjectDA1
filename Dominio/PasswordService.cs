using System.Security.Cryptography;
using System.Text;

namespace Dominio;

public class PasswordService
{
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Digits = "0123456789";
    private const string SymbolChars = "@#$%^&*()_+!-=[]{};':\"\\|,.<>/?";
    private const int MinimumLength = 8;
    
    public bool IsPasswordValid(string password)
    {
        bool validLength = password.Length >= MinimumLength;
        bool hasUpperCase = password.Any(x => Uppercase.Contains(x));
        bool hasLowerCase = password.Any(x => Lowercase.Contains(x));
        bool hasDigit = password.Any(x => Digits.Contains(x));
        bool hasSymbol = password.Any(x => SymbolChars.Contains(x));
        return validLength && hasUpperCase && hasLowerCase && hasDigit && hasSymbol;
    }

    public string GenerateRandomPassword()
    {
        StringBuilder password = new StringBuilder();
        password.Append(GetRandomChar(Uppercase));
        password.Append(GetRandomChar(Digits));
        password.Append(GetRandomChar(SymbolChars));
        for (int i = 0; i < 5; i++)
        {
            password.Append(GetRandomChar(Lowercase));
        }
        return password.ToString();
    }

    private char GetRandomChar(string chars)
    {
        return chars[RandomNumberGenerator.GetInt32(0, chars.Length - 1)];
    }
}