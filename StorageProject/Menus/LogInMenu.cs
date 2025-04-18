using StorageProject.utils;

namespace StorageProject.Menus
{
    // Menu logowania
   public class LogInMenu
    {
        public static void LoginMenu()
        {
            while (true)
            {
                Console.WriteLine("Witaj w systemie zarządzania magazynem!");
                if (LogInSystem.LogIn())
                {
                    Console.Clear();
                    return;
                }
            }
        }
    }
}
