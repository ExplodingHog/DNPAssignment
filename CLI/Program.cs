using CLI.UI;

using InMemoryRepository;
using RepositoryContracts;

Console.WriteLine("Starting CLI app ... ");

IUserRepository userRepository = new UserInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();

CliApp cliApp = new CliApp(userRepository, postRepository);

await cliApp.StartAsync();