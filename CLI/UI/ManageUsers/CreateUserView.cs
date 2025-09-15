using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private IUserRepository userRepository;
    private ManageUsersView manageUsersView;

    public CreateUserView(IUserRepository userRepository, ManageUsersView manageUsersView)
    {
        this.userRepository = userRepository;
        this.manageUsersView = manageUsersView;
    }


    public async Task CreateUserAsync()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine()!;
        Console.Write("Enter password: ");
        string password = Console.ReadLine()!;
        await userRepository.AddAsync(new User { username = username, password = password });
        Console.WriteLine($"User created with id {userRepository.GetMany().Last().Id}");
        await manageUsersView.ShowUi();
    }
}