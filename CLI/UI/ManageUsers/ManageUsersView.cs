using CLI.UI.ManagePosts;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly IUserRepository userRepository;
    private readonly CliApp cliApp;
    private bool running;

    private ListUserView listUserView;
    private CreateUserView createUserView;

    public ManageUsersView(IUserRepository userRepository, CliApp cliApp)
    {
        this.userRepository = userRepository;
        this.cliApp = cliApp;
    }

    public async Task ShowUi()
    {
        running = true;
        while (running)
        {
            Console.WriteLine(
                "Please enter a number corresponding with your selection:");
            Console.WriteLine("(1) View users \n" +
                              "(2) Create user \n" +
                              "(0) Go back");
            int? selection = int.Parse(Console.ReadLine());
            switch (selection)
            {
                case 1:
                    if (listUserView is null)
                    {
                        listUserView = new ListUserView(userRepository, this);
                    }

                    await listUserView.ListUsers();
                    break;
                case 2:
                    if (createUserView is null)
                    {
                        createUserView =
                            new CreateUserView(userRepository, this);
                    }

                    await createUserView.CreateUserAsync();
                    break;
                default:
                    running = false;
                    break;
            }
        }

        cliApp.StartAsync();
    }
}