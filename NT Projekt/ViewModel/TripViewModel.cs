using NT_Projekt.Model.Repositories;
using NT_Projekt.Model;
using NT_Projekt.View;

namespace NT_Projekt.ViewModel
{
    public class TripViewModel
    {
        public void AddTrip(RepositoryManager repoManager)
        {
            Console.WriteLine("Tilføj ny tur");

            Console.Write("Indtast dato og tid (fx 13-05-2025 14:30): ");
            DateTime dateTime = Convert.ToDateTime(Console.ReadLine());
            
            Console.Write("Indtast energiforbrug (fx 22.5): ");
            double energyUsage = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Indtast varighed i minutter (fx 45): ");
            TimeSpan duration = TimeSpan.FromMinutes(Convert.ToDouble(Console.ReadLine()));

            Console.Write("Indtast rute-ID: ");
            string routeID = Console.ReadLine();

            Console.Write("Indtast busnummer (nummerplade): ");
            string licensePlate = Console.ReadLine();

            Console.WriteLine("Indtast bruger-ID: ");
            string userID = Console.ReadLine();

            Trip newTrip = new Trip(dateTime, energyUsage, duration, routeID, licensePlate, userID, null);

            repoManager.TripRepository.AddTrip(newTrip);

            Console.WriteLine("Turen er tilføjet!");

            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }

        public void DeleteTrip(RepositoryManager repoManager)
        {
            Console.WriteLine("Indtast TripID for den tur du vil slette: ");
            string tripID = Console.ReadLine();

            repoManager.TripRepository.DeleteTrip(tripID);

            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }

        public void ShowAllTrips(RepositoryManager repoManager)
        {
            Console.WriteLine("Alle registrerede ture: ");

            var trips = repoManager.TripRepository.GetAllTrips();

            foreach (var trip in trips)
            {
                Console.WriteLine(trip);
            }

            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }

        public void ShowTripsByPeriod(RepositoryManager repoManager)
        {
            Console.WriteLine("Indtast startdato (yyyy-mm-dd): ");
            DateTime startDate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Indtast slutdato (yyyy-mm-dd): ");
            DateTime endDate = Convert.ToDateTime(Console.ReadLine());

            var trips = repoManager.TripRepository.GetAllTrips();

            Console.WriteLine("Ture i perioden: ");

            foreach (var trip in trips)
            {
                // Dato kontrol
                if (trip.DateTime >= startDate && trip.DateTime <= endDate)
                {
                    Console.WriteLine(trip);
                }
            }

            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }

        public void ShowTripsByLeastFuelEfficient(RepositoryManager repoManager)
        {
            var trips = repoManager.TripRepository.GetAllTrips();

            Console.WriteLine("Ture sorteret efter laveste energiforbrug: ");

            // Laver en ny liste med trips og sorterer dem i stigende rækkefølge for EnergyUsage
            var sortedTrips = trips.OrderBy(t => t.EnergyUsage);

            foreach (var trip in sortedTrips)
            {
                Console.WriteLine(trip);
            }

            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }

        public void ShowTripsByMostFuelEfficient(RepositoryManager repoManager)
        {
            var trips = repoManager.TripRepository.GetAllTrips();

            Console.WriteLine("Ture sorteret efter højeste energiforbrug: ");

            // Laver en ny liste med trips og sorterer dem i faldende rækkefølge for EnergyUsage
            var sortedTrips = trips.OrderByDescending(t => t.EnergyUsage);

            foreach (var trip in sortedTrips)
            {
                Console.WriteLine(trip);
            }

            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }

        public void ShowTripsByMostDelayed(RepositoryManager repoManager)
        {
            // Jeg kan ikke få denne metode til at virke
        }

        public void ShowTripsByUserID(RepositoryManager repoManager)
        {
            Console.WriteLine("Indtast bruger ID: ");
            string userID = Console.ReadLine();

            // Null check på repositoriet
            var trips = repoManager.TripRepository.GetAllTrips();
            if (trips == null)
            {
                Console.WriteLine("Ingen ture blev fundet.");
                return;
            }

            Console.WriteLine($"Ture kørt af brugeren {userID}: ");

            bool userFound = false;

            foreach (var trip in trips)
            {
                // Null check på hver tur
                if (trip != null && trip.UserID == userID)
                {
                    Console.WriteLine(trip);
                    userFound = true;
                }
            }

            // Hvis brugeren ikke blev fundet, kommer der en fejlbesked
            if (!userFound)
            {
                Console.WriteLine("Ingen ture fundet for den indtastede bruger.");
            }

            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }

        public void ShowTripsByRouteID(RepositoryManager repoManager)
        {
            Console.WriteLine("Indtast rute ID: ");
            string routeID = Console.ReadLine();

            var trips = repoManager.TripRepository.GetAllTrips();

            Console.WriteLine($"Ture på følgende rute: {routeID}");

            bool routeFound = false;

            foreach (var trip in trips)
            {
                if (trip.RouteID == routeID)
                {
                    Console.WriteLine(trip);
                    routeFound = true;
                }
            }
            // Hvis ruten ikke blev fundet, kommer der en fejlbesked
            if (!routeFound)
            {
                Console.WriteLine("Ingen ture fundet for den indtastede rute.");
            }

            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }
    }
}
