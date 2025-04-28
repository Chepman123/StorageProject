using StorageProject.Menus;
using StorageProject.utils;

namespace StorageProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Subskrypcja zdarzeń systemu logowania
            LogInSystem.OnLoginResult += UIHelper.EndLogin;
            LogInSystem.OnLoginResult += UIHelper.EndTask;

            // Subskrypcja zdarzeń menu magazynu
            StorageMenu.StorageManager.OnAction += UIHelper.ShowMessage;
            StorageMenu.StorageManager.OnAction += UIHelper.EndTask;

            // Subskrypcja zdarzeń menu sekcji
            SectionsMenu.SectionManager.OnAction += UIHelper.ShowMessage;
            SectionsMenu.SectionManager.OnAction += UIHelper.EndTask;

            // Wywołanie logowania i menu głównego
            LogInMenu.LoginMenu();
            MainMenuManager.MainMenu();
        }

        
    }
}

