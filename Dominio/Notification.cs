namespace Dominio;

public class Notification
{
    public int Id {get; set;}
    public string Text {get; set;}
    public int FromUserId {get; set;}
    public User FromUser {get; set;}
    public int ToUserId {get; set;}
    public User ToUser {get; set;}
}