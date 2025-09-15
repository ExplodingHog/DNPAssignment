using Entities;

namespace CLI.UI.ManageUsers;

public class SingleUserView
{
    private readonly Task<User> user;
    private readonly ListUserView listUserView;

    public SingleUserView(Task<User> user,
        ListUserView listUserView)
    {
        this.user = user;
        this.listUserView = listUserView;
    }

    public async Task ShowUser()
    {
        Console.WriteLine($"Username: {user.Result.username} \n" +
                          $"ID: {user.Result.Id} \n " +
                          $"Password: {user.Result.password} \n");
        Console.WriteLine(
            "Please enter a number corresponding with your selection:");
        Console.WriteLine("(1) Edit user \n" +
                          "(2) Delete user \n" +
                          "(0) Go back");
        int? selection = int.Parse(Console.ReadLine());
        switch (selection)
        {
            case 1: throw new NotImplementedException(); break;
            case 2: throw new NotImplementedException(); break;
            default: await listUserView.ListUsers(); break;
        }
    }
}