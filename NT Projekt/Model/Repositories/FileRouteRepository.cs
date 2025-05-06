using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model.Repositories {
    //FileRouteRepository implementerer IRouteRepository og håndterer lagring af rutedata i en fil
    public class FileRouteRepository : IRouteRepository {
        //Privat felt der gemmer filstien for rutedata
        private readonly string _filePath = "routes.txt";

        /* 
         * Contructor der initialisere FileRouteRepository med en specificeret filsti
         * Hvis filen ikke findes, oprettes den automatisk
         */
        public FileRouteRepository(string filePath) {
            _filePath = filePath;
            if (!File.Exists(_filePath)) {
                File.Create(_filePath).Close();
            }
        }

        /* 
         * Tilføjer en ny rute til filen
         * Route data konverteres til en string med ToString()
         * Data skrives til file med StreamWriter i append-tilstand
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked
         */
        public void AddRoute(Route route) {
            try {
                using (StreamWriter sw = new StreamWriter(_filePath, true)) {
                    sw.WriteLine(route.ToString());
                }
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        /* 
         * Sletter en rute fra filen baseret på routeID
         * Henter alle ruter fra filen og samler dem til en liste
         * Fjerner alle ruter med det angivne routeID
         * Overskriver filen med den opdaterede liste af ruter
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked
         */
        public void DeleteRoute(string routeID) {
            try {
                List<Route> routes = GetAllRoutes().ToList();
                routes.RemoveAll(r => r.RouteID == routeID);
                RewriteFile(routes);
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved slet af rute: {ex.Message}");
            }
        }

        /* 
         * Henter en rute baseret på routeID
         * Læser filen og laver en liste med ruter
         * returnere den første rute med det angivne ruteID eller null, hvis det ikke findes
         */
        public Route? GetRoute(string routeID) {
            return GetAllRoutes().FirstOrDefault(r => r.RouteID == routeID);
        }

        /* 
         * Henter alle ruter fra filen
         * Filens indhold læses linje for linje
         * Hver linje konverteres til Route objekter med FromString()
         * Listen af ruter returneres
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked og returnere en tom liste
         */
        public List<Route> GetAllRoutes() {
            try {
                using (StreamReader sr = new StreamReader(_filePath)) {
                    List<Route> routes = new List<Route>();
                    string line;

                    while ((line = sr.ReadLine()) != null) {
                        if (!string.IsNullOrEmpty(line)) {
                            routes.Add(Route.FromString(line));
                        }
                    }
                    return routes;
                }
            }
            catch (IOException ex) {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return new List<Route>();
            }
        }

        /* 
         * Overskriver filen med en ny liste af ruter
         * Åbner filen og skriver hver rute fra listen
         * Hvis der opstår en IO-fejl, fanges den og sender en fejl-besked
         */
        private void RewriteFile(List<Route> routes) {
            try {
                using (StreamWriter sw = new StreamWriter(_filePath)) {
                    foreach (var route in routes) {
                        sw.WriteLine(route.ToString());
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }
    }
}
