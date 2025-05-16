using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model.Repositories;

namespace NT_Projekt.View {
    public class StartMenu {
        /* 
         * Hovedmenu
         * Opretter en instans af Menu med prompt og options som parameter
         * Bruger switch statement til at håndtere hvilken metode skal kaldes afhængig af bruger input
         */
        public static void StartingMenu(RepositoryManager repoManager) {
            string prompt = "Nordjyllands Trafikselskab";
            string[] options = { "Tilføj Tur", "Vis Ture", "Datahåndtering", "Afslut Program" };
            Menu startMenu = new Menu(prompt, options);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex) {
                case 0:
                    AddTripView.AddTrip(repoManager);
                    break;
                case 1:
                    ShowTripView.ShowTrips(repoManager);
                    break;
                case 2:
                    DataMenu.DataMenuHandler(repoManager);
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
