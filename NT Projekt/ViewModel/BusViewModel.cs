using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model.Repositories;
using NT_Projekt.Model;
using NT_Projekt.View;

namespace NT_Projekt.ViewModel {
    public class BusViewModel {
        /* 
         * Håndtere oprettelse af ny bus
         * Prompter brugeren om information som bliver læst og gemt i nogle variable
         * Opretter et nyt bus objekt
         * Det nye bus objekt tilføjes til BusRepository gennem repoManager
         * Sender en bekræftigelse til brugeren om at den nye bus er gemt
         */
        public static void AddBus(RepositoryManager repoManager) {
            Console.WriteLine("<<< Tilføj Bus >>>");
            Console.Write("Fabrikant: ");
            string busBrand = Console.ReadLine();
            Console.Write("Model: ");
            string busModel = Console.ReadLine();
            Console.Write("Nummerplade: ");
            string busLicensePlate = Console.ReadLine();
            Console.Write("Energi type: ");
            string busEnergyType = Console.ReadLine();

            Bus newBus = new Bus(busBrand, busModel, busLicensePlate, busEnergyType);

            repoManager.BusRepository.AddBus(newBus);

            Console.Write($"Bussen {newBus.Brand} {newBus.Model} {newBus.LicensePlate}, blev tilføjet. Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            BusView.BusMenu(repoManager);
        }

        /*
         * Håndtere at slette en bus
         * Prompter brugeren om en nummerplade
         * Kalder DeleteBus fra BusRepository gennem repoManager til at slette bussen
         */
        public static void DeleteBus(RepositoryManager repoManager) {
            Console.WriteLine("<<< Slet Bus >>>");
            Console.Write("Indtast nummerplade: ");
            string licensePlate = Console.ReadLine();

            repoManager.BusRepository.DeleteBus(licensePlate);
            Console.ReadKey();
            BusView.BusMenu(repoManager);
        }

        /*
         * Viser en liste over alle eksisterende busser
         * Opretter en variable buses og tildeler den en liste af busser fra GetAllBuses() i BusRepository
         * Laver et foreach loop til at itererer gennem listen 
         * Den printer til konsol hver bus i listen med tilhørende businformation
         */
        public static void ShowAllBuses(RepositoryManager repoManager) {
            Console.Clear();
            
            Console.WriteLine("<<< Alle Busser >>>\n");
            var buses = repoManager.BusRepository.GetAllBuses();

            if (buses.Count == 0)
            {
                Console.WriteLine("Ingen busser blev fundet");
                Console.Write("\nTryk en tast for at vende tilbage til menuen...");
                Console.ReadKey();
                BusView.BusMenu(repoManager);
                return;
            }
            
            bool isFirst = true;
            foreach (var bus in buses) {
                PrintBusDetails(bus, isFirst);
                isFirst = false;

            }
            Console.Write("\nTryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            BusView.BusMenu(repoManager);
        }

        public static void PrintBusDetails(Bus bus, bool first = false)
        {
            string infoHeader = string.Format("{0,-15} {1,-15} {2,-15} {3,-15}", "Brand", "Model", "Nummerplade", "Energitype");

            string line = new string('-', 58);

            if (first)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(infoHeader);
                Console.ResetColor();
                Console.WriteLine(line);               
            }

            Console.ForegroundColor = ConsoleColor.White;
            string busDetails = string.Format("{0,-15} {1,-15} {2,-15} {3,-15}", bus.Brand, bus.Model, bus.LicensePlate, bus.EnergyType);
            Console.WriteLine(busDetails);
            Console.ResetColor();
        }
    }
}
