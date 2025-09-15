using CLI.UI.ManageUsers;
using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepo; 
    private readonly IPostRepository postRepo;
    
    private ManagePostView managePostView;
    private ManageUsersView manageUsersView;
    public CliApp(IUserRepository userRepo, IPostRepository postRepo)
    {
        this.userRepo = userRepo;
        this.postRepo = postRepo;
     
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("1. Manage Users");
            Console.WriteLine("2. Mange Posts");
            Console.WriteLine("0. Exit");

            string choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    if (manageUsersView is null)
                    {
                        manageUsersView = new ManageUsersView(userRepo, this);
                    }
                    await manageUsersView.ShowUi()
                    ; break;
                case "2":
                    if (managePostView is null)
                    {
                        managePostView = new ManagePostView( postRepo, this);
                    }

                    await managePostView.ShowUi();
                    break;
                default:
                    break;
           
            }
        }
    }
    
}
    
