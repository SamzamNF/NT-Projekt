using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model.Repositories {
    /*
     * IBusRepository definerer et interface for håndtering af busdata
     * Det giver en abstraktion over dataadgagen og tillader polymorfi
     * Definere metoder til at:
     * Hente alle busser i systemet
     * Hente bus baseret på en licensePlate
     * Tilføje en bus
     * Slette en bus baseret på en licensePlate
     */
    public interface IBusRepository {
        List<Bus> GetAllBuses();
        Bus? GetBus(string licensePlate);
        void AddBus(Bus bus);
        void DeleteBus(string licensePlate);
    }
}
