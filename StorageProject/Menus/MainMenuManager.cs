namespace StorageProject.Menus
{
    // Menu główne
    public class MainMenuManager
    {
        public delegate void MenuAction(string message);
        //Zdarzenie wywołane po zakończeniu operacji
        public static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Główne Menu:");
                Console.WriteLine("1. Zarządzanie magazynem");
                Console.WriteLine("2. Wyjdź z programu");

                switch (SelectNumber(1, 2))
                {
                    case 1:
                        Console.Clear();
                        StorageMenu.StorageManager.StorageMain();
                        break;
                    case 2:
                        Console.WriteLine("Program się kończy...");
                        Environment.Exit(0);
                        break;
                }
                Console.Clear();
            }

        }
        // Funkcja zwracająca liczbę, pobraną od użytkownika
        public static int SelectNumber(int min, int max)
        {
            int selected;
            while (true)
            {
                string range = $"({min}-{max})";
                if (min == max)
                {
                    range = $"({min.ToString()})";
                }
                Console.Write($"Wybierz opcję {range}: ");

                // Sprawsza czy podana liczba wykonuje wszystkie kryteria
                if (int.TryParse(Console.ReadLine(), out selected) && selected >= min && selected <= max)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Wpisana nieprawidłowa wartość. Liczba musi być w przedziale: {min}-{max}");
                }
            }
            return selected;
        }
        // Funkcja zwracająca tekst, pobrany od użytkownika 
        public static string SelectText(string text)
        {
            string result;
            do
            {
                Console.Write(text);
                result = Console.ReadLine() ?? "";
            } while (string.IsNullOrWhiteSpace(result) || int.TryParse(result, out _));
            return result;
        }
    }


   
    
}
