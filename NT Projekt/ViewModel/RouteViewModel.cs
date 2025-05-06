using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model.Repositories;
using NT_Projekt.Model;
using NT_Projekt.View;

namespace NT_Projekt.ViewModel {
    public class RouteViewModel {
        /*
         * Håndtere oprettelse af ny rute
         * Prompter brugeren om information som bliver læst og gemt i nogle variable
         * Opretter et ny rute objekt
         * Det nye rute objekt tilføjes til RouteRepository gennem repoManager
         * Sender en bekræftigelse til brugeren om at ruten blev tilføjet
         */
        public static void AddRoute(RepositoryManager repoManager) {
            Console.WriteLine("<<< Tilføj Rute >>>");
            Console.Write("Start punkt: ");
            string routeStartPoint = Console.ReadLine();
            Console.Write("Slut punkt: ");
            string routeEndPoint = Console.ReadLine();
            Console.Write("Estimeret varighed (hh:mm:ss): ");
            TimeSpan routeEstimatedDuration = TimeSpan.Parse(Console.ReadLine());
            Console.Write("Estimeret energiforbrug: ");
            double routeEstimatedEnergy = double.Parse(Console.ReadLine());
            Console.Write("Distance: ");
            double routeDistance = double.Parse(Console.ReadLine());
            Console.Write("Rute ID: ");
            string routeID = Console.ReadLine();

            Route newRoute = new Route(routeStartPoint, routeEndPoint, routeEstimatedDuration, routeEstimatedEnergy, routeDistance, routeID);

            repoManager.RouteRepository.AddRoute(newRoute);

            Console.Write($"Ruten fra {newRoute.StartPoint} til {newRoute.EndPoint}, blev tilføjet. Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            RouteView.RouteMenu(repoManager);
        }

        /* 
         * Håndtere at slette en rute
         * Prompter brugeren om et ID for ruten
         * Kalder DeleteRoute fra RouteRepository gennem repoManager til at fjerne ruten
         */
        public static void DeleteRoute(RepositoryManager repoManager) {
            Console.WriteLine("<<< Slet Bus >>>");
            Console.Write("Indtast rute ID: ");
            string routeID = Console.ReadLine();

            repoManager.RouteRepository.DeleteRoute(routeID);
            Console.ReadKey();
            RouteView.RouteMenu(repoManager);
        }

        /*
         * Viser en liste over alle eksisterende ruter
         * Opretter en variable routes og tildeler den en liste af ruter fra GetAllRoutes() i RouteRepository
         * Laver et foreach loop til at itererer gennem listen 
         * Den printer til konsol hver rute i listen med tilhørende ruteinformation
         */
        public static void ShowAllRoutes(RepositoryManager repoManager) {
            Console.WriteLine("<<< Alle Ruter >>>");
            var routes = repoManager.RouteRepository.GetAllRoutes();
            foreach (var route in routes) {
                Console.WriteLine($"{route.StartPoint} {route.EndPoint} {route.EstimatedDuration} {route.EstimatedEnergyUsage} {route.Distance} {route.RouteID}");
            }
            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            RouteView.RouteMenu(repoManager);
        }
    }
}
