using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model.Repositories;
using NT_Projekt.ViewModel;

namespace NT_Projekt.View
{
    public class ShowTripView
    {
        /* 
         * Menu til at håndtere handlinger for bruger information. Kalder metoder afhængig af hvad brugeren vil
         * Opretter en instans af Menu med prompt og options som parameter
         * Bruger switch statement til at håndtere hvilken metode skal kaldes afhængig af bruger input
         */
        public static void ShowTrips(RepositoryManager repoManager)
        {
            TripViewModel tripViewModel = new TripViewModel();
            
            string prompt = "Print tur information";
            string[] options = { "Alle", "Tur i tids interval", "Mindst energi effektive", "Mest energi effektive", "Mest forsinkede", "Chauffør ID", "Rute ID", "Returner til start" };
            Menu userMenu = new Menu(prompt, options);
            int selectedIndex = userMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    tripViewModel.ShowAllTrips(repoManager);
                    break;
                case 1:
                    tripViewModel.ShowTripsByPeriod(repoManager);
                    break;
                case 2:
                    tripViewModel.ShowTripsByLeastFuelEfficient(repoManager);
                    break;
                case 3:
                    tripViewModel.ShowTripsByMostFuelEfficient(repoManager);
                    break;
                case 4:
                    tripViewModel.ShowTripsByMostDelayed(repoManager);
                    break;
                case 5:
                    tripViewModel.ShowTripsByUserID(repoManager);
                    break;
                case 6:
                    tripViewModel.ShowTripsByRouteID(repoManager);
                    break;
                case 7:
                    StartMenu.StartingMenu(repoManager);
                    break;
            }
        }
    }
}
