using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model.Repositories
{
    /*
     * ITripRepository definerer et interface for håndtering af tripdata
     * Det giver en abstraktion over dataadgagen og tillader polymorfi
     * Definere metoder til at:
     * Hente alle trips i systemet
     * Hente rute baseret på et tripID
     * Tilføje en rute
     * Slette en rute baseret på et tripID
     */
    public interface ITripRepository
    {
        public List<Trip> GetAllTrips();
        public Trip? GetTrip(string tripID);
        public void AddTrip(Trip trip);
        public void DeleteTrip(string tripID);
    }
}
