﻿namespace Dominio;

public class User
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }

    public bool IsNameValid()
    {
        return Name != null && Name.Length > 2 && Name.Length < 100;
    }

    public bool IsSurnameValid()
    {
        return Surname != null && Surname.Length > 2 && Surname.Length < 100;
    }
    
}