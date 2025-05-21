using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model.Repositories;
using NT_Projekt.Model;
using NT_Projekt.View;
using System.Data;

namespace NT_Projekt.ViewModel {
    public class UserViewModel {
        /* 
         * Håndtere oprettelse af ny bruger
         * Prompter brugeren om en række informationer som bliver læst og gemt i nogle variable
         * Opretter et nyt objekt af User, med de givne informationer
         * Kalder AddUser fra UserRepository gennem repoManager og gemmer bruger objektet
         * Sender en bekræftigelse til brugeren om at en ny bruger er gemt
         */
        public static void AddUser(RepositoryManager repoManager) {
            Console.WriteLine("<<< Tilføj Bruger >>>");
            Console.Write("Fornavn: ");
            string userFirstName = Console.ReadLine();
            Console.Write("Efternavn: ");
            string userLastName = Console.ReadLine();
            Console.Write("ID: ");
            string userID = Console.ReadLine();

            User newUser = new User(userFirstName, userLastName, userID);

            repoManager.UserRepository.AddUser(newUser);

            Console.Write($"Bruger {newUser.FirstName} {newUser.LastName}, blev tilføjet. Tryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            UserView.UserMenu(repoManager);
        }

        /* 
         * Håndtere at slette en bruger
         * Prompter brugeren om et ID, som bliver læst og gemt i en variabel
         * Kalder DeleteUser metoden fra UserRepository gennem repoManager og sletter brugeren med de tilsvarende ID
         */
        public static void DeleteUser(RepositoryManager repoManager) {
            Console.WriteLine("<<< Slet Bruger >>>");
            Console.Write("Indtast bruger ID: ");
            string userID = Console.ReadLine();

            repoManager.UserRepository.DeleteUser(userID);
            Console.ReadKey();
            UserView.UserMenu(repoManager);
        }

        /*
         * Viser en liste over alle eksisterende brugere
         * Opretter en variable users og tildeler den en liste af brugere fra GetAllUsers() i UserRepository
         * Laver et foreach loop til at itererer gennem listen 
         * Den printer til konsol hver user i listen, med deres tilhørende navn, efternavn og ID
         */
        public static void ShowAllUsers(RepositoryManager repoManager) {
            Console.Clear();
            Console.WriteLine("<<< Alle Brugere >>>");
            var users = repoManager.UserRepository.GetAllUsers();
            
            if (users.Count <= 0)
            {
                Console.WriteLine("Ingen brugere fundet i systemet");
                Console.Write("Tryk en tast for at vende tilbage til menuen...");
                Console.ReadKey();
                UserView.UserMenu(repoManager);
                return;
            }

            bool first = true;
            foreach (var user in users) {
                PrintUserDetails(user, first);
                first = false;
            }
            Console.Write("\nTryk en tast for at vende tilbage til menuen...");
            Console.ReadKey();
            UserView.UserMenu(repoManager);
        }

        public static void PrintUserDetails(User user, bool first = false)
        {
            string infoheader = string.Format("{0,-13} {1,-13} {2,-13}", "Fornavn", "Efternavn", "Bruger ID");
            string line = new string('-', 42);

            if (first)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(infoheader);
                Console.ResetColor();
                Console.WriteLine(line);
            }

            string routeDetails = string.Format("{0,-13} {1,-13} {2,-13}",
            user.FirstName,
            user.LastName,
            user.UserID);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(routeDetails);
            Console.ResetColor();
        }

        public static void UserList(RepositoryManager repoManager)
        {
            Console.WriteLine("<<< Alle Brugere >>>");
            var users = repoManager.UserRepository.GetAllUsers();

            if (users.Count <= 0)
            {
                Console.WriteLine("\nIngen brugere fundet i systemet - Opret en bruger inden du fortsætter");
                Console.Write("Tryk en tast for at vende tilbage til menuen...");
                Console.ReadKey();
                UserView.UserMenu(repoManager);
                return;
            }

            bool first = true;
            foreach (var user in users)
            {
                PrintUserDetails(user, first);
                first = false;
            }
        }
    }
}