using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model.Repositories {
    //FileBusRepository implementerer IBusRepository og håndterer lagring af busdata i en fil
    public class FileBusRepository : IBusRepository {
        //Privat felt der gemmer filstien for busdata
        private readonly string _filePath = "buses.txt";

        /* 
         * Contructor der initialisere FileBusRepository med en specificeret filsti
         * Hvis filen ikke findes, oprettes den automatisk
         */
        public FileBusRepository(string filePath) {
            _filePath = filePath;
            if (!File.Exists(_filePath)) {
                File.Create(_filePath).Close();
            }
        }

        /* 
         * Tilføjer en ny bus til filen
         * Bus data konverteres til en string med ToString()
         * Data skrives til file med StreamWriter i append-tilstand
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked
         */
        public void AddBus(Bus bus) {
            try {
                using (StreamWriter sw = new StreamWriter(_filePath, true)) {
                    sw.WriteLine(bus.ToString());
                }
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        /* 
         * Sletter en bus fra filen baseret på licensePlate
         * Henter alle busser fra filen og samler dem til en liste
         * Fjerner alle busser med den angivne licensePlate
         * Overskriver filen med den opdaterede liste af busser
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked
         */
        public void DeleteBus(string licensePlate) {
            try {
                List<Bus> buses = GetAllBuses().ToList();
                buses.RemoveAll(b => b.LicensePlate == licensePlate);
                RewriteFile(buses);
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved slet af bus: {ex.Message}");
            }
        }

        /* 
         * Henter en bus baseret på licensePlate
         * Læser filen og laver en liste med busser
         * returnere den første bus med den angivne licensePlate eller null, hvis det ikke findes
         */
        public Bus? GetBus(string licensePlate) {
            return GetAllBuses().FirstOrDefault(b => b.LicensePlate == licensePlate);
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
         * Henter alle busser fra filen
         * Filens indhold læses linje for linje
         * Hver linje konverteres til Bus objekter med FromString()
         * Listen af busser returneres
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked og returnere en tom liste
         */
        public List<Bus> GetAllBuses() {
            try {
                using (StreamReader sr = new StreamReader(_filePath)) {
                    List<Bus> buses = new List<Bus>();
                    string line;

                    while ((line = sr.ReadLine()) != null) {
                        if (!string.IsNullOrEmpty(line)) {
                            buses.Add(Bus.FromString(line));
                        }
                    }
                    return buses;
                }
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return new List<Bus>();
            }
        }

        /* 
         * Overskriver filen med en ny liste af busser
         * Åbner filen og skriver hver bus fra listen
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked
         */
        private void RewriteFile(List<Bus> buses) {
            try {
                using (StreamWriter sw = new StreamWriter(_filePath)) {
                    foreach (var bus in buses) {
                        sw.WriteLine(bus.ToString());
                    }
                }
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }
    }
}
