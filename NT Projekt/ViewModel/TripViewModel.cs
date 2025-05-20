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
            Console.Clear();

            Console.WriteLine("Alle registrerede ture:\n\n");

            var trips = repoManager.TripRepository.GetAllTrips();
            var routes = repoManager.RouteRepository.GetAllRoutes();

            bool first = true;

            foreach (var trip in trips)
            {
                // Finder den tilhørende rute via trip's RouteID
                // Det gør vi ved hver trip, at dens rute hentes, så værdierne kan sættes ind i tabellen til den passende trip
                var route = routes.FirstOrDefault(r => r.RouteID == trip.RouteID);

                if (route != null)
                {
                    PrintTripDetails(trip, route, first);
                    first = false;
                }
            }

            Console.Write("\nTryk en tast for at vende tilbage til menuen...");
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
            Console.Clear();
            Console.WriteLine("Ture sorteret efter mest forsinkede: \n\n");

            // Hent alle ture og ruter
            var trips = repoManager.TripRepository.GetAllTrips();
            var routes = repoManager.RouteRepository.GetAllRoutes();

            // Finder trips med any, hvor der matcher samme trip id som rute id, hvor at trip duration var størrere end estimated duration.
            // Derefter sortere vi i rækkefølge fra højeste afvigelse og ned  ved at udregne afvigelsen direkte.
            var delayedTrips = trips.Where(t => routes.Any(r => r.RouteID == t.RouteID && t.Duration > r.EstimatedDuration))
                .OrderByDescending(t => t.Duration - routes.First(r => r.RouteID == t.RouteID).EstimatedDuration);

            bool first = true;
            foreach (var trip in delayedTrips)
            {
                // Finder den tilhørende rute via trip's RouteID
                // Det gør vi ved hver trip, at dens rute hentes, så værdierne kan sættes ind i tabellen til den passende trip
                var route = routes.First(r => r.RouteID == trip.RouteID);
               
                PrintTripDetails(trip, route, first);
                first = false;
            }

            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }

        public void ShowTripsByUserID(RepositoryManager repoManager)
        {
            Console.WriteLine("Indtast bruger ID: ");
            string userID = Console.ReadLine();

            //Henter trips hvor det indtastede user ID søger efter trips med matchende User ID i databasen
            //Henter så alle ruter
            var trips = repoManager.TripRepository.GetAllTrips().Where(t => t.UserID == userID);
            var routes = repoManager.RouteRepository.GetAllRoutes();

            if (trips == null)
            {
                Console.WriteLine("Ingen ture blev fundet.");
                return;
            }

            Console.WriteLine($"Ture kørt af brugeren {userID}: ");

            bool userFound = false;
            bool first = true;

            foreach (var trip in trips)
            {
                // Finder den tilhørende rute via trip's RouteID
                // Det gør vi ved hver trip, at dens rute hentes, så værdierne kan sættes ind i tabellen til den passende trip
                var route = routes.FirstOrDefault(r => r.RouteID == trip.RouteID);
                
                if (route == null) continue;

                if (trip.UserID == userID)
                {
                    PrintTripDetails(trip, route, first);
                    userFound = true;
                    first = false;
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
            Console.Clear();

            Console.WriteLine("Indtast rute ID: ");
            string routeID = Console.ReadLine();

            //Henter ruten baseret på det indtastede rutenummer
            //Henter så alle trips der passer under det ruteID.
            Route route = repoManager.RouteRepository.GetRoute(routeID);
            var trips = repoManager.TripRepository.GetAllTrips().Where(t => t.RouteID == routeID);

            if (trips.Any())
            {
                Console.WriteLine($"Ture på følgende rute: {routeID}");

                bool routeFound = false;
                bool first = true;

                foreach (var trip in trips)
                {
                    if (route != null)
                    {
                        PrintTripDetails(trip, route, first);
                        routeFound = true;
                        first = false;
                    }
                }

            }
            
            // Hvis ruten ikke blev fundet, kommer der en fejlbesked
            if (!trips.Any())
            {
                Console.WriteLine("Ingen ture fundet for den indtastede rute.");
            }

            Console.Write("Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }

        public void PrintTripDetails(Trip trip, Route route, bool first = false)
        {
            string infoHeader = string.Format("{0,-13} {1,-10} {2,-22} {3,-10} {4,-10} {5,-10} {6,-10} {7,-15} {8,-10}",
                "Nummerplade", "Bruger", "Dato & Tid", "Varighed", "Estimat", "Forsink.", "Energi", "Est. Energi" ,"Energiafv.");
            string line = new string('-', 115);

            if (first)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(infoHeader);
                Console.ResetColor();
                Console.WriteLine(line);
            }

            TimeSpan delay = trip.Duration - route.EstimatedDuration;
            double energyVariation = trip.EnergyUsage - route.EstimatedEnergyUsage;

            Console.ForegroundColor = ConsoleColor.White;
            
            if (delay.TotalMinutes < 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (delay.TotalMinutes > 3)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (delay.TotalMinutes > 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            string tripDetails = string.Format("{0,-13} {1,-10} {2,-22} {3,-10} {4,-10} {5,-10} {6,-10:F2} {7,-15:F2} {8,-10:F2}",
                trip.LicensePlate,
                trip.UserID,
                trip.DateTime.ToString("dd-MM-yyyy HH:mm"),
                trip.Duration.ToString(@"hh\:mm\:ss"),
                route.EstimatedDuration.ToString(@"hh\:mm\:ss"),
                delay.ToString(@"hh\:mm\:ss"),
                trip.EnergyUsage,
                route.EstimatedEnergyUsage,
                energyVariation);

            Console.WriteLine(tripDetails);

            Console.ResetColor();
        }
    }
}
