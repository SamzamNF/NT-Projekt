using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NT_Projekt.Model.Repositories;
using NT_Projekt.ViewModel;

namespace NT_Projekt.View {
    public class UserView {
        /* 
         * Menu til at håndtere handlinger for bruger information. Kalder metoder afhængig af hvad brugeren vil
         * Opretter en instans af Menu med prompt og options som parameter
         * Bruger switch statement til at håndtere hvilken metode skal kaldes afhængig af bruger input
         */
        public static void UserMenu(RepositoryManager repoManager) {
            string prompt = "Brugerhåndtering";
            string[] options = { "Tilføj Bruger", "Slet Bruger", "Se Brugere", "Returner til datahåndtering" };
            Menu userMenu = new Menu(prompt, options);
            int selectedIndex = userMenu.Run();

            switch (selectedIndex) {
                case 0:
                    UserViewModel.AddUser(repoManager);
                    break;
                case 1:
                    UserViewModel.DeleteUser(repoManager);
                    break;
                case 2:
                    UserViewModel.ShowAllUsers(repoManager);
                    break;
                case 3:
                    DataMenu.DataMenuHandler(repoManager);
                    break;
            }
        }
    }
}
