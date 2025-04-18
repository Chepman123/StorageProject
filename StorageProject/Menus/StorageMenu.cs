using StorageProject.Models.Storage;
using StorageProject.Models.Users;
using StorageProject.utils;

namespace StorageProject.Menus
{
    public class StorageMenu
    {
        public static class StorageManager
        {
            
            public delegate void MenuAction(string message);
            //Zdarzenie wywołane po zakończeniu operacji
            public static event MenuAction? OnAction;

            public static void StorageMain()
            {
                while (true)
                {
                    Console.WriteLine("Zarządzanie magazynem:");

                    // Wyświetla menu jęsli użytkownik jest klientem
                    Role role = LogInSystem.user?.Role ?? Role.Client;
                    if (role == Role.Client)
                    {
                        Console.WriteLine("1. Dodaj pudełko");
                        Console.WriteLine("2. Znajdź pudełko po ID");
                        Console.WriteLine("3. Wyjdź");

                        switch (MainMenuManager.SelectNumber(1, 3))
                        {
                            case 1:
                                int result = SelectSection();

                                // Sprawdza obecność sekcji
                                if (result == -1)
                                {
                                    break;
                                }
                                else
                                {
                                    SectionsMenu.SectionManager.AddBox(result);
                                }

                                break;
                            case 2:
                                int Result = SelectSection();

                                // Sprawdza obecność sekcji
                                if (Result == -1)
                                {
                                    break;
                                }
                                else
                                {
                                    SectionsMenu.SectionManager.FindBox(Result);
                                }
                                break;
                            case 3:
                                Console.Clear();
                                return;
                        }
                    }
                    // Wyświetla menu jęsli użytkownik jest menedżerem
                    else if (role == Role.Manager)
                    {
                        Console.WriteLine("1. Edytuj sekcję");
                        Console.WriteLine("2. Wyświetl wszystkie sekcje");
                        Console.WriteLine("3. Wyjdź");

                        switch (MainMenuManager.SelectNumber(1, 3))
                        {
                            case 1:
                                EditSection();
                                break;
                            case 2:
                                DisplayAllSections();
                                break;
                            case 3:
                                Console.Clear();
                                return;
                        }
                    }
                    // Wyświetla menu jęsli użytkownik jest adminem
                    else
                    {
                        Console.WriteLine("1. Edytuj sekcję");
                        Console.WriteLine("2. Dodaj sekcję");
                        Console.WriteLine("3. Usuń sekcję");
                        Console.WriteLine("4. Wyświetl wszystkie sekcje");
                        Console.WriteLine("5. Wyjdź");

                        switch (MainMenuManager.SelectNumber(1, 5))
                        {
                            case 1:
                                EditSection();
                                break;
                            case 2:
                                AddSection();
                                break;
                            case 3:
                                RemoveSection();
                                break;
                            case 4:
                                DisplayAllSections();
                                break;
                            case 5:
                                Console.Clear();
                                return;
                        }
                    }
                }
            }

            // Metoda wywołująca menu sekcji
            private static void EditSection()
            {
                Console.Clear();

                // Sprawdzenie obecność sekcji
                if (Storage.sections.Count == 0)
                {
                    OnAction?.Invoke("Brak sekcji do edycji");
                    return;
                }
                // Wyświtela informację o sekcjach
                Storage.DisplayAllSections();
                Console.WriteLine();
                Console.Write($"Wybierz sekcje:");
                // Wywołanie menu magazynu
                SectionsMenu.SectionManager.SectionMain(MainMenuManager.SelectNumber(1, Storage.sections.Count) - 1);
            }
            // Metoda dodająca sekcję
            private static void AddSection()
            {
                Console.Clear();
                Storage.sections.Add(new Section(Storage.sections.Count));
                Logger.SendLog("Dodał sekcję");

                // Zdarzenie wyświetla wiadomość i kończy operację
                OnAction?.Invoke("Sekcja była dodana");
                
                return;
            }
            // Metoda usuwająca sekcję
            private static void RemoveSection()
            {
                Console.Clear();

                // Sprawdzenia obecności sekcji
                if (Storage.sections.Count == 0)
                {
                    OnAction?.Invoke("Brak sekcji do usunięcia");
                    return;
                }
                // Wyświetlanie wszystkich sekcji
                Storage.DisplayAllSections();
                Console.WriteLine();

                Console.WriteLine($"Wybierz sekcję do usunięcia: ");
                int selected_to_remove = MainMenuManager.SelectNumber(1, Storage.sections.Count) - 1;
                Storage.RemoveSection(selected_to_remove);

                Logger.SendLog("Usunął sekcję");
                // Zdarzenie wyświetla wiadomość i kończy operację
                OnAction?.Invoke($"Sekcja #{selected_to_remove + 1} była usunięta");
                return;
            }
            // Metoda wyświetlająca wszystkie sekcję
            private static void DisplayAllSections()
            {
                Console.Clear();
                Storage.DisplayAllSections();
                // Zdarzenie wyświetla wiadomość i kończy operację
                OnAction?.Invoke("");
                return;
            }
            // Funkcja zwracająca indeks sekcji od użytkownika
            private static int SelectSection()
            {
                // Sprawdza obecność sekcji
                if (Storage.sections.Count == 0)
                {
                    Console.Clear();
                    OnAction?.Invoke("Brak sekcji do edycji");
                    return -1;
                }
                Console.WriteLine();
                Console.Write($"Wybierz sekcje:");
                return MainMenuManager.SelectNumber(1, Storage.sections.Count) - 1;
            }
        }
    }
}
