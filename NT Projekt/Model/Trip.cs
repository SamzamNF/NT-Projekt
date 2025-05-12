using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model
{
    public class Trip
    {
        private DateTime _dateTime;
        private double _energyUsage;
        private TimeSpan _duration;
        private string _routeID;
        private string _licensePlate;
        private string _userID;
        private string _tripID;

        //public properties der giver adgang til private felter og gør dem læsbare og skrivbar
        public DateTime DateTime { get { return _dateTime; } set { _dateTime = value; } }
        public double EnergyUsage { get { return _energyUsage; } set { _energyUsage = value; } }
        public TimeSpan Duration { get { return _duration; } set { _duration = value; } }
        public string RouteID { get { return _routeID; } set { _routeID = value; } }
        public string LicensePlate { get { return _licensePlate; } set { _licensePlate = value; } }
        public string UserID { get { return _userID; } set { _userID = value; } }
        public string TripID { get { return _tripID; } }


        //Constructor der initialisere en Trip instans med givne parameter
        public Trip(DateTime date, double energyUsage, TimeSpan duration, string routeID, string licensePlate, string userID, string? tripID)
        {
            this._dateTime = date;
            this._energyUsage = energyUsage;
            this._duration = duration;
            this._routeID = routeID;
            this._licensePlate = licensePlate;
            this._userID = userID;
            if (tripID != null)
            {
                this._tripID = tripID;
            }
            else
            {
                this._tripID = Guid.NewGuid().ToString();
            }
        }

        //ToString() formatere objektets data til en string seperaret med semikolon til lagring
        public override string ToString()
        {
            return $"{DateTime};{EnergyUsage};{Duration};{RouteID};{LicensePlate};{UserID};{TripID}";
        }

        //FromString() rekonstruerer et Trip objekt fra en string genereret af ToString()
        public static Trip FromString(string line)
        {
            var parts = line.Split(';');
            DateTime date = DateTime.Parse(parts[0]);
            double energyUsage = double.Parse(parts[1]);
            TimeSpan duration = TimeSpan.Parse(parts[2]);
            string routeID = parts[3];
            string licensePlate = parts[4];
            string userID = parts[5];
            string tripID = parts[6];
            
            Trip trip = new Trip(date, energyUsage, duration, routeID, userID, licensePlate, tripID);
            return trip;
        }
    }
}
