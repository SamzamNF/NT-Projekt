using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model {
    public class Bus {
        //Private felter
        private string _brand;
        private string _model;
        private string _licensePlate;
        private string _energyType;

        //public properties der giver adgang til private felter og gør dem læsbare og skrivbar
        public string Brand { get { return _brand; } set { _brand = value; } }
        public string Model { get { return _model; } set { _model = value; } }
        public string LicensePlate { get { return _licensePlate; } set { _licensePlate = value; } }
        public string EnergyType { get { return _energyType; } set { _energyType = value; } }

        //Constructor der initialisere en Bus instans med givne parameter
        public Bus(string brand, string model, string licensePlate, string energyType) {
            this._brand = brand;
            this._model = model;
            this._licensePlate = licensePlate;
            this._energyType = energyType;
        }

        //ToString() formatere objektets data til en string seperaret med semikolon til lagring
        public override string ToString() {
            return $"{Brand};{Model};{LicensePlate};{EnergyType}";
        }

        //FromString() rekonstruerer et Bus objekt fra en string genereret af ToString()
        public static Bus FromString(string line) {
            var parts = line.Split(";");
            string brand = parts[0];
            string model = parts[1];
            string licensePlate = parts[2];
            string energyType = parts[3];

            Bus bus = new Bus(brand, model, licensePlate, energyType);

            return bus;
        }
    }
}
