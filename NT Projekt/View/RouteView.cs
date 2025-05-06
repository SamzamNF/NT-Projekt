using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model.Repositories;
using NT_Projekt.ViewModel;

namespace NT_Projekt.View {
    public class RouteView {
        /* 
         * Menu til at håndtere handlinger for rute information. Kalder metoder afhængig af hvad brugeren vil
         * Opretter en instans af Menu med prompt og options som parameter
         * Bruger switch statement til at håndtere hvilken metode skal kaldes afhængig af bruger input
         */
        public static void RouteMenu(RepositoryManager repoManager) {
            string prompt = "Rutehåndtering";
            string[] options = { "Tilføj Rute", "Slet Rute", "Se Ruter", "Returner til datahåndtering" };
            Menu routeMenu = new Menu(prompt, options);
            int selectedIndex = routeMenu.Run();

            switch (selectedIndex) {
                case 0:
                    RouteViewModel.AddRoute(repoManager);
                    break;
                case 1:
                    RouteViewModel.DeleteRoute(repoManager);
                    break;
                case 2:
                    RouteViewModel.ShowAllRoutes(repoManager);
                    break;
                case 3:
                    DataMenu.DataMenuHandler(repoManager);
                    break;
            }
        }
    }
}
