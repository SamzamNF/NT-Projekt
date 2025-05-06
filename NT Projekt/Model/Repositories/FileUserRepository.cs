using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model.Repositories {
    //FileUserRepository implementerer IUserRepository og håndterer lagring af brugerdata i en fil
    public class FileUserRepository : IUserRepository {
        //Privat felt der gemmer filstien for brugerdata
        private readonly string _filePath = "users.txt";

        /* 
         * Contructor der initialisere FileUserRepository med en specificeret filsti
         * Hvis filen ikke findes, oprettes den automatisk
         */
        public FileUserRepository(string filePath) {
            _filePath = filePath;
            if (!File.Exists(_filePath)) {
                File.Create(_filePath).Close();
            }
        }

        /* 
         * Tilføjer en ny bruger til filen
         * User data konverteres til en string med ToString()
         * Data skrives til file med StreamWriter i append-tilstand
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked
         */
        public void AddUser(User user) {
            try {
                using (StreamWriter sw = new StreamWriter(_filePath, true)) {
                    sw.WriteLine(user.ToString());
                }
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        /* 
         * Sletter en bruger fra filen baseret på userID
         * Henter alle brugere fra filen og samler dem til en liste
         * Fjerner alle brugere med det angivne userID
         * Overskriver filen med den opdaterede liste af brugere
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked
         */
        public void DeleteUser(string userID) {
            try {
                List<User> users = GetAllUsers().ToList();
                users.RemoveAll(u => u.UserID == userID);
                RewriteFile(users);
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved slet af bruger: {ex.Message}");
            }
        }

        /* 
         * Henter en bruger baseret på userID
         * Læser filen og laver en liste med brugere
         * returnere den første bruger med det angivne userID eller null, hvis det ikke findes
         */
        public User? GetUser(string userID) {
            return GetAllUsers().FirstOrDefault(u => u.UserID == userID);
            /*
            try {
                using(StreamReader sr = new StreamReader(_filePath)) {
                    List<User> users = new List<User>();
                    string line;

                    while((line = sr.ReadLine()) != null) {
                        if(!string.IsNullOrEmpty(line)) {
                            User user = User.FromString(line);
                            if(user.UserID == userID) {
                                users.Add(user);
                            }
                        }
                    }
                    return users;
                }
            } catch(IOException ex) {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return new List<User>();
            }
            */
        }

        /* 
         * Henter alle brugere fra filen
         * Filens indhold læses linje for linje
         * Hver linje konverteres til User objekter med FromString()
         * Listen af brugere returneres
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked og returnere en tom liste
         */
        public List<User> GetAllUsers() {
            try {
                using (StreamReader sr = new StreamReader(_filePath)) {
                    List<User> users = new List<User>();
                    string line;

                    while ((line = sr.ReadLine()) != null) {
                        if (!string.IsNullOrEmpty(line)) {
                            users.Add(User.FromString(line));
                        }
                    }
                    return users;
                }
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return new List<User>();
            }
        }

        /* 
         * Overskriver filen med en ny liste af brugere
         * Åbner filen og skriver hver bruger fra listen
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked
         */
        private void RewriteFile(List<User> users) {
            try {
                using (StreamWriter sw = new StreamWriter(_filePath)) {
                    foreach (var user in users) {
                        sw.WriteLine(user.ToString());
                    }
                }
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }
    }
}
