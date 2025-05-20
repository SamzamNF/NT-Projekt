using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model.Repositories;
using NT_Projekt.ViewModel;

namespace NT_Projekt.View
{
    public class TripView
    {
        /* 
         * Menu til at håndtere handlinger for bus information. Kalder metoder afhængig af hvad brugeren vil
         * Opretter en instans af Menu med prompt og options som parameter
         * Bruger switch statement til at håndtere hvilken metode skal kaldes afhængig af bruger input
         */
        public static void AddTrip(RepositoryManager repoManager)
        {
            TripViewModel tripViewModel = new TripViewModel();
            
            string prompt = "Bushåndtering";
            string[] options = { "Tilføj Tur", "Slet Tur", "Returner til start" };
            Menu busMenu = new Menu(prompt, options);
            int selectedIndex = busMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    tripViewModel.AddTrip(repoManager);
                    break;
                case 1:
                    tripViewModel.DeleteTrip(repoManager);
                    break;
                case 2:
                    StartMenu.StartingMenu(repoManager);
                    break;
            }
        }
    }
}
