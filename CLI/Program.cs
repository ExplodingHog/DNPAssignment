using CLI.UI;
using Filerepo;
//using InMemoryRepository;
using RepositoryContracts;

Console.WriteLine("Starting CLI app ... ");

IUserRepository userRepository = new UserFileRepo();
ICommentRepository commentRepository = new CommentFileRepo();
IPostRepository postRepository = new PostFileRepo();

CliApp cliApp = new CliApp(userRepository, postRepository);

await cliApp.StartAsync();