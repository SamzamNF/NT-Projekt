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

            try
            {
                Console.Write("Indtast dato og tid (fx 13-05-2025 14:30): ");
                DateTime dateTime = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Indtast energiforbrug (fx 22.5): ");
                double energyUsage = Convert.ToDouble(Console.ReadLine());

                Console.Write("Indtast varighed i minutter (fx 45): ");
                TimeSpan duration = TimeSpan.FromMinutes(Convert.ToDouble(Console.ReadLine()));

                Console.Clear();
                RouteViewModel.RouteList(repoManager);
                Console.Write("\nIndtast rute-ID: ");
                string routeID = Console.ReadLine();

                Console.Clear();
                BusViewModel.BusList(repoManager);
                Console.Write("\nIndtast busnummer (nummerplade): ");
                string licensePlate = Console.ReadLine();

                Console.Clear();
                UserViewModel.UserList(repoManager);
                Console.Write("\nIndtast bruger-ID: ");
                string userID = Console.ReadLine();

                Console.Clear();
                string comment;
                Console.WriteLine("Har du en kommentar? (1. Ja | 2. Nej");
                ConsoleKeyInfo commentChoice = Console.ReadKey();
                if (commentChoice.KeyChar == 1)
                {
                    Console.Write("Skriv din kommentar: ");
                    comment = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Ingen kommentar givet\n");
                    comment = "";
                }


                Trip newTrip = new Trip(dateTime, energyUsage, duration, routeID, licensePlate, userID, null, comment);

                repoManager.TripRepository.AddTrip(newTrip);

                Console.WriteLine("Turen er tilføjet!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFejl i indtastning af data: {ex.Message}");
            }
           

            Console.Write("\nTryk en tast for at vende tilbage til menuen...");
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

            Console.WriteLine("Alle registrerede ture:\n");

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
            Console.WriteLine("Ture sorteret efter mest forsinkede: \n");

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
            Console.Clear();

            UserViewModel.UserList(repoManager);
            Console.Write("\nIndtast bruger ID: ");
            string userID = Console.ReadLine();
            Console.Clear();

            //Henter trips hvor det indtastede user ID søger efter trips med matchende User ID i databasen
            //Henter så alle ruter
            var trips = repoManager.TripRepository.GetAllTrips().Where(t => t.UserID == userID);
            var routes = repoManager.RouteRepository.GetAllRoutes();

            if (!trips.Any())
            {
                Console.WriteLine("Ingen ture blev fundet for den indtastede bruger.");
                Console.Write("\nTryk en tast for at vende tilbage til menuen...");
                Console.ReadKey();
                StartMenu.StartingMenu(repoManager);
                return;
            }

            else
            {
                Console.WriteLine($"\nTure kørt af brugeren {userID}:\n");

                bool first = true;

                foreach (var trip in trips)
                {
                    // Finder den tilhørende rute via trip's RouteID
                    // Det gør vi ved hver trip, at dens rute hentes, så værdierne kan sættes ind i tabellen til den passende trip
                    var route = routes.FirstOrDefault(r => r.RouteID == trip.RouteID);

                    // Program crashede hele tiden uden denne, skal nok fjernes og laves ordenligt?
                    if (route == null) continue;

                    if (trip.UserID == userID)
                    {
                        PrintTripDetails(trip, route, first);
                        first = false;
                    }
                }
            }
            


            Console.Write("\nTryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            StartMenu.StartingMenu(repoManager);
        }

        public void ShowTripsByRouteID(RepositoryManager repoManager)
        {
            Console.Clear();

            RouteViewModel.RouteList(repoManager);
            Console.Write("\nIndtast rute ID: ");
            string routeID = Console.ReadLine();

            //Henter ruten baseret på det indtastede rutenummer
            //Henter så alle trips der passer under det ruteID.
            Route route = repoManager.RouteRepository.GetRoute(routeID);
            var trips = repoManager.TripRepository.GetAllTrips().Where(t => t.RouteID == routeID);


            Console.Clear();
            if (trips.Any())
            {
                Console.Clear();
                Console.WriteLine($"\nTure på følgende rute: {routeID}\n");

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

            Console.Write("\nTryk en tast for at vende tilbage til menuen...");
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("■");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" Hvis bus ankommer før tid");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("■");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" Hvis bus er mellem 3 og 6 minutter forsinket");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("■");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" Hvis bus er mere end 6 minutter forsinket\n");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(infoHeader);
                Console.ResetColor();
                Console.WriteLine(line);
            }

            TimeSpan delay = trip.Duration - route.EstimatedDuration;
            double energyVariation = trip.EnergyUsage - route.EstimatedEnergyUsage;

            if (delay.TotalMinutes < 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (delay.TotalMinutes <= 3)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (delay.TotalMinutes <= 6)
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
