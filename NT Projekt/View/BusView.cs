using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model.Repositories;
using NT_Projekt.ViewModel;

namespace NT_Projekt.View {
    public class BusView {
        /* 
         * Menu til at håndtere handlinger for bus information. Kalder metoder afhængig af hvad brugeren vil
         * Opretter en instans af Menu med prompt og options som parameter
         * Bruger switch statement til at håndtere hvilken metode skal kaldes afhængig af bruger input
         */
        public static void BusMenu(RepositoryManager repoManager) {
            string prompt = "Bushåndtering";
            string[] options = { "Tilføj Bus", "Slet Bus", "Se Busser", "Returner til datahåndtering" };
            Menu busMenu = new Menu(prompt, options);
            int selectedIndex = busMenu.Run();

            switch (selectedIndex) {
                case 0:
                    BusViewModel.AddBus(repoManager);
                    break;
                case 1:
                    BusViewModel.DeleteBus(repoManager);
                    break;
                case 2:
                    BusViewModel.ShowAllBuses(repoManager);
                    break;
                case 3:
                    DataMenu.DataMenuHandler(repoManager);
                    break;
            }
        }
    }
}
