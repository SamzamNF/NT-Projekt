using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model {
    public class User {
        //Private felter
        private string _firstName;
        private string _lastName;
        private string _userID;

        //public properties der giver adgang til private felter og gør dem læsbare og skrivbar
        public string FirstName { get { return _firstName; } set { _firstName = value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        public string UserID { get { return _userID; } set { _userID = value; } }

        //Constructor der initialisere en User instans med givne parameter
        public User(string firstName, string lastName, string userID) {
            this._firstName = firstName;
            this._lastName = lastName;
            this._userID = userID;
        }

        //ToString() formatere objektets data til en string seperaret med semikolon til lagring
        public override string ToString() {
            return $"{FirstName};{LastName};{UserID}";
        }

        //FromString() rekonstruerer et User objekt fra en string genereret af ToString()
        public static User FromString(string line) {
            var parts = line.Split(';');
            string firstName = parts[0];
            string lastName = parts[1];
            string userID = parts[2];

            User user = new User(firstName, lastName, userID);

            return user;
        }
    }
}
