using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model.Repositories;

namespace NT_Projekt.View {
    public class DataMenu {
        /* 
         * Menu til at håndtere handlinger om brugeren vil have adgang til metoder for Brugere, Ruter eller Busser
         * Opretter en instans af Menu med prompt og options som parameter
         * Bruger switch statement til at håndtere hvilken metode skal kaldes afhængig af bruger input
         */
        public static void DataMenuHandler(RepositoryManager repoManager) {
            string prompt = "Datahåndtering";
            string[] options = { "Brugere", "Ruter", "Busser", "Returner til start" };
            Menu dataMenuHandler = new Menu(prompt, options);
            int selectedIndex = dataMenuHandler.Run();

            switch (selectedIndex) {
                case 0:
                    UserView.UserMenu(repoManager);
                    break;
                case 1:
                    RouteView.RouteMenu(repoManager);
                    break;
                case 2:
                    BusView.BusMenu(repoManager);
                    break;
                case 3:
                    StartMenu.StartingMenu(repoManager);
                    break;
            }
        }
    }
}
