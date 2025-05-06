using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model {
    public class Route {
        //Private felter
        private string _startPoint;
        private string _endPoint;
        private TimeSpan _estimatedDuration;
        private double _estimatedEnergyUsage;
        private double _distance;
        private string _routeID;

        //public properties der giver adgang til private felter og gør dem læsbare og skrivbar
        public string StartPoint { get { return _startPoint; } set { _startPoint = value; } }
        public string EndPoint { get { return _endPoint; } set { _endPoint = value; } }
        public TimeSpan EstimatedDuration { get { return _estimatedDuration; } set { _estimatedDuration = value; } }
        public double EstimatedEnergyUsage { get { return _estimatedEnergyUsage; } set { _estimatedEnergyUsage = value; } }
        public double Distance { get { return _distance; } set { _distance = value; } }
        public string RouteID { get { return _routeID; } set { _routeID = value; } }

        //Constructor der initialisere en Route instans med givne parameter
        public Route(string startPoint, string endPoint, TimeSpan estimatedDuration, double estimatedEnergyUsage, double distance, string routeID) {
            this._startPoint = startPoint;
            this._endPoint = endPoint;
            this._estimatedDuration = estimatedDuration;
            this._estimatedEnergyUsage = estimatedEnergyUsage;
            this._distance = distance;
            this._routeID = routeID;
        }

        //ToString() formatere objektets data til en string seperaret med semikolon til lagring
        public override string ToString() {
            return $"{StartPoint};{EndPoint};{EstimatedDuration:c};{EstimatedEnergyUsage};{Distance};{RouteID}";
        }

        //FromString() rekonstruerer et Route objekt fra en string genereret af ToString()
        public static Route FromString(string line) {
            var parts = line.Split(';');
            string startPoint = parts[0];
            string endPoint = parts[1];
            TimeSpan estimatedDuration = TimeSpan.Parse(parts[2]);
            double estimatedEnergyUsage = double.Parse(parts[3]);
            double distance = double.Parse(parts[4]);
            string routeID = parts[5];

            Route route = new Route(startPoint, endPoint, estimatedDuration, estimatedEnergyUsage, distance, routeID);

            return route;
        }
    }
}
