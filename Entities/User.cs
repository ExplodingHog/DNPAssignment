namespace Entities;

public class User
{
    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

    public int Id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
}