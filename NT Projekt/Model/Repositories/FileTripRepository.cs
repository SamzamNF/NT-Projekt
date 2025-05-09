using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model.Repositories
{
    //FileRouteRepository implementerer IRouteRepository og håndterer lagring af rutedata i en fil
    public class FileTripRepository : ITripRepository
    {
        // Privat falt som gemmer stien for dataen
        private readonly string _filepath = "trips.txt";

        // Konstruktør som inti. objektet med en specifk sti til filen - Hvis filen ikke findes, så oprettes den automatisk i programstart.
        public FileTripRepository(string filepath)
        {
            _filepath = filepath;
            if (File.Exists(_filepath))
            {
                File.Create(_filepath).Close();
            }
        }


        // Metode som tilføjer et tur objekt til filen. Den bruger metoden ToString() fra model klassen af Trips. Bemærk, append = true som gør at den IKKE overskriver filen, blot tilføjer
        public void AddTrip(Trip trip)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_filepath, true))
                {
                    sw.WriteLine(trip.ToString());
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        /* Metode som sletter en trip baseret på et unikt trip ID.
        Metoden virker ved at den bruger GetAllTrips() metoden til at hente alle ture fra filen ned i en liste
        Derefter filtrere den listen ved brug af LINQ udtrykket FirstOrDefault, som vælger den første der matcher.
        Hvis der er et match, så fjerner metoden den søgte tur fra den midlertidige liste "trips", og derefter bruger den
        metoden "SaveAll" med denne liste trips, og gemmer den nye og opdateret data til filen.*/
        public void DeleteTrip(string tripID)
        {
            var trips = GetAllTrips();

            var tripToDelete = trips.FirstOrDefault(t => t.TripID == tripID);

            if (tripToDelete != null)
            {
                trips.Remove(tripToDelete);
                SaveAll(trips);
            }
            else
            {
                Console.WriteLine("Tur ikke fundet");
            }
        }

        /* Metoden bruger StreamReader til at hente alle turene fra den fil som er angivet (trips.txt)
           Den opretter en liste af trips først, en variabel string line til at holde dataen på
           Så kører den igennem hver linje af filen, indtil line = null
           Så længe den finder data, så bliver den ved og tilføjer den data den henter, ned i den lokale variabel trip -
           Hvis trip ikke er tom, så tilføjes den til listen af trips. Sådan fortsætter den indtil listen er tom, hvor den så 
           returnere den fulde liste af trips.
           */
        public List<Trip> GetAllTrips()
        {
            try
            {
                List<Trip> trips = new();

                using (StreamReader sr = new StreamReader(_filepath))
                {
                    
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var trip = Trip.FromString(line);
                        if (trip != null)
                        {
                            trips.Add(trip);
                        }
                    }
                    return trips;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl i hente alle trips: {ex.Message}");
                return null;
            }
        }

        
        /* Metoden virker ved at den modtager et parameter af tripID først. Så starter den med at lave en lokal liste af trips
           ved at bruge metoden GetAllTrips(), så kører den listen igennem hvor den leder efter det første ID der matcher, som er unikt.
           Når det er fundet, så returnere den et trip objekt tilbage.
        */
        public Trip? GetTrip(string tripID)
        {
            var trips = GetAllTrips();
            return trips.FirstOrDefault(t => t.TripID == tripID);
        }

        /* Metoden bruges til at gemme alle trips efter en trip er blevet slettet i RemoveTrip metoden.
         * Den bruger en simpel streamwriter, hvor append er FALSE - dvs den overskrider filen.
         * Den tager en liste af trips som parameter, og kører denne liste igennem en foreach loop
         * Den tilføjer her hver enkelt trip i listen til file ved brug af ToString metoden fra Trip klassen.
         */
        public void SaveAll(List<Trip> trips)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_filepath, append: false))
                {
                    foreach (Trip trip in trips)
                    {
                        sw.WriteLine(trip.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }
    }
}
