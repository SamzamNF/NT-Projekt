using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT_Projekt.Model.Repositories {
    /*
     * IUserRepository definerer et interface for håndtering af brugerdata
     * Det giver en abstraktion over dataadgagen og tillader polymorfi
     * Definere metoder til at:
     * Hente alle brugere i systemet
     * Hente bruger baseret på et userID
     * Tilføje en bruger
     * Slette en bruger baseret på et userID
     */
    public interface IUserRepository {
        List<User> GetAllUsers();
        User? GetUser(string userID);
        void AddUser(User user);
        void DeleteUser(string userID);
    }
}
