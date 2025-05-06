using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model.Repositories {
    /*
     * IRouteRepository definerer et interface for håndtering af rutedata
     * Det giver en abstraktion over dataadgagen og tillader polymorfi
     * Definere metoder til at:
     * Hente alle ruter i systemet
     * Hente rute baseret på et routeID
     * Tilføje en rute
     * Slette en rute baseret på et routeID
     */
    public interface IRouteRepository {
        List<Route> GetAllRoutes();
        Route? GetRoute(string routeID);
        void AddRoute(Route route);
        void DeleteRoute(string routeID);
    }
}
