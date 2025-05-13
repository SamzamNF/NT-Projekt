using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model.Repositories {
    /* 
     * RepositoryManager er en central administrations klasse for FileRepositories
     * Her indkapsles adgangen til repositories, så de administreres fra et sted
     * Dette er en løs kopling, da repositories kan skiftes her uden at påvirke resten af programmet
     */
    public class RepositoryManager {
        //public properties der gør repositories læsbare i resten af programmet
        public IUserRepository UserRepository { get; }
        public IBusRepository BusRepository { get; }
        public IRouteRepository RouteRepository { get; }
        public ITripRepository TripRepository { get; }
        /* 
         * Constructor initialisere RepositoryManager med instanser af user-, bus- og route-repositories
         */
        public RepositoryManager(IUserRepository userRepository, IBusRepository busRepository, IRouteRepository routeRepository, ITripRepository tripRepository) {
            UserRepository = userRepository;
            BusRepository = busRepository;
            RouteRepository = routeRepository;
            TripRepository = tripRepository;
        }
    }
}
