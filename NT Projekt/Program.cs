using NT_Projekt.Model.Repositories;
using NT_Projekt.View;

namespace NT_Projekt {
    internal class Program {
        static void Main(string[] args) {
            /*
             * Deklarerer variable (variable navn)Repository af typen I(variable navn)Repository
             * Hver variable bliver tildelt en instans af deres tilhørende File(variable navn)Repository
             * Her anvendes polymorfi, da vores variable indeholder en instans af et Repository
             * 
             * RepositoryManager samler funktionaliteten fra Repository instanserne
             * Dette tillader dataadgang fra et centralt objekt
             * Der anvendes Dependency Injection da RepositoryManager modtager sine afhængigheder(parameter) gennem sin constructor
             */
            string projectRoot = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string dataPath = Path.Combine(projectRoot, "Data");

            IUserRepository userRepository = new FileUserRepository(Path.Combine(dataPath, "users.txt"));
            IBusRepository busRepository = new FileBusRepository(Path.Combine(dataPath, "buses.txt"));
            IRouteRepository routeRepository = new FileRouteRepository(Path.Combine(dataPath, "routes.txt"));


            RepositoryManager repoManager = new RepositoryManager(userRepository, busRepository, routeRepository);

            Console.Title = "Nordjyllands Trafikselskab";
            StartMenu.StartingMenu(repoManager);
        }
    }
}
