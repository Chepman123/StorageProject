using StorageProject.Models.Storage;
using StorageProject.Models.Users;
using StorageProject.utils;

namespace StorageProject.Menus
{
    public class SectionsMenu
    {
        // Menu sekcji
       public static class SectionManager
        {
            public delegate void MenuAction(string message);
            //Zdarzenie wywołane po zakończeniu operacji
            public static event MenuAction? OnAction;

            public static void SectionMain(int which)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Edycja sekcji:");

                    Role role = LogInSystem.user?.Role ?? Role.Client;

                    // Wyświetla menu jęsli użytkownik jest menedżerem
                    if (role == Role.Manager)
                    {
                        Console.WriteLine("1. Wyświetl wszystkie pudełka");
                        Console.WriteLine("2. Znajdź pudełko po ID");
                        Console.WriteLine("3. Wyjdź");

                        switch (MainMenuManager.SelectNumber(1, 3))
                        {
                            case 1:
                                DisplayAllBoxes(which);
                                break;
                            case 2:
                                FindBox(which);
                                break;
                            case 3:
                                Console.Clear();
                                return;
                        }
                    }
                    // Wyświetla menu jęsli użytkownik jest adminem
                    else
                    {
                        Console.WriteLine("1. Przenieś pudełko między sekcjami");
                        Console.WriteLine("2. Wyświetl wszystkie pudełka");
                        Console.WriteLine("3. Znajdź pudełko po ID");
                        Console.WriteLine("4. Zmień informacje o pudełku");
                        Console.WriteLine("5. Usuń pudełko");
                        Console.WriteLine("6. Wyjdź");

                        switch (MainMenuManager.SelectNumber(1, 6))
                        {
                            case 1:
                                MoveBox(which);
                                break;
                            case 2:
                                DisplayAllBoxes(which);
                                break;
                            case 3:
                                FindBox(which);
                                break;
                            case 4:
                                ChangeBox(which);
                                break;
                            case 5:
                                RemoveBox(which);
                                break;
                            case 6:
                                Console.Clear();
                                return;
                        }
                    }
                }
            }
            // Metoda dodająca pudełko
            public static void AddBox(int which)
            {
                Console.Clear();

                Boxes.Box box;

                Console.WriteLine("Wybierz typ:");
                Console.WriteLine("1.Owocy");
                Console.WriteLine("2.Warzywa");
                int type = MainMenuManager.SelectNumber(1, 2);

                Console.Clear();

                Console.Write("Podaj ilość: ");
                int countInBox = MainMenuManager.SelectNumber(1, 9999);

                Console.Clear();

                string typeObject = MainMenuManager.SelectText("Podaj zawartość pudełka: ");

                Console.Clear();

                // Tworzy pudełko z podanymi wartościami 
                if (type == 1)
                {
                    box = new Boxes.BoxWithFruits(Storage.sections[which].Boxes.Count, which, countInBox, typeObject, LogInSystem.user ?? new User("root",Role.Client));
                }
                else
                {
                    box = new Boxes.BoxWithVegetables(Storage.sections[which].Boxes.Count, which, countInBox, typeObject,LogInSystem.user ?? new User("root", Role.Client));
                }

                // Dodaje pudełko do sekcji
                Storage.sections[which].AddBox(box);
                Logger.SendLog("Dodał pudełko");

                // Zdarzenie kończy operację i wyświetla komunikat
                OnAction?.Invoke("Pudełko zostało dodane");

                return;
            }
            // Metoda przesuwa pudełko
            private static void MoveBox(int which)
            {
                Console.Clear();

                // Sprawdzenie czy podana sekcja jest pusta
                if (Storage.sections[which].Boxes.Count == 0)
                {
                    // Zdarzenie kończy operację i wyświetla komunikat
                    OnAction?.Invoke("Brak pudełek w tej sekcji!");
                    return;
                }

                Console.Write($"Wybierz pudełko(x{Storage.sections[which].Boxes.Count}): ");
                int id_box = MainMenuManager.SelectNumber(1, Storage.sections[which].Boxes.Count) - 1;

                Console.Clear();

                Console.WriteLine("Wybierz nową sekcję: ");
                int new_id_section = MainMenuManager.SelectNumber(1, Storage.sections.Count) - 1;
                Storage.MoveBox(which, new_id_section, id_box);

                Console.Clear();
                Logger.SendLog($"Przeniosł pudełko z sekcji #{which} do sekcji #{new_id_section}");
                // Zdarzenie kończy operację i wyświetla komunikat
                OnAction?.Invoke("Pudełko zostało przeniesione");
                return;

            }
            // Metoda wyświetlająca wszystkie pudełka
            private static void DisplayAllBoxes(int which)
            {
                Console.Clear();

                // Sprawdzenie czy podana sekcja jest pusta
                if (Storage.sections[which].Boxes.Count == 0)
                {
                    // Zdarzenie kończy operację i wyświetla komunikat
                    OnAction?.Invoke("Brak pudełek w tej sekcji!");
                    return;
                }

                Storage.sections[which].DisplayInfo();
                // Zdarzenie kończy operację i wyświetla komunikat
                OnAction?.Invoke("");
                return;
            }
            // Metoda wyświetlająca informację o wybranym pudełku
            public static void FindBox(int which)
            {
                Console.Clear();

                // Sprawdzenie czy podana sekcja jest pusta
                if (Storage.sections[which].Boxes.Count == 0)
                {
                    // Zdarzenie kończy operację i wyświetla komunikat
                    OnAction?.Invoke("Brak pudełek w tej sekcji!");
                    return;
                }

                // Pobiera od użytkownika indeks pudełka
                Console.WriteLine($"Wybierz pudełko(x{Storage.sections[which].Boxes.Count}): ");
                int whichBox = MainMenuManager.SelectNumber(1, Storage.sections[which].Boxes.Count) - 1;
                Console.WriteLine();

                // Sprawdzenie czy użytkownik może zobaczyć informację o pudełku
                if (Storage.sections[which].Boxes[whichBox].owner.UserName == LogInSystem.user?.UserName || LogInSystem.user?.Role != Role.Client)
                {
                    Storage.sections[which].Boxes[whichBox].ShowInfo();
                }
                else 
                {
                    Console.WriteLine($"Pudełko nie należy do {LogInSystem.user?.UserName}");
                }

                // Zdarzenie kończy operację i wyświetla komunikat
                OnAction?.Invoke("");
                return;
            }
            // Metoda zmieniająca informację o pudełku
            private static void ChangeBox(int which)
            {
                // Sprawdzenie czy podana sekcja jest pusta
                if (Storage.sections[which].Boxes.Count == 0)
                {
                    // Zdarzenie kończy operację i wyświetla komunikat
                    OnAction?.Invoke("Brak pudełek w tej sekcji!");
                    return;
                }

                // Pobiera od użytkownika wartości pudełka
                Console.Write($"Wybierz pudełko(x{Storage.sections[which].Boxes.Count}): ");
                int selectedBox_toChange = MainMenuManager.SelectNumber(1, Storage.sections[which].Boxes.Count) - 1;
                Console.Clear();

                string objectsName = MainMenuManager.SelectText("Podaj zawartość pudełka: ");
                Console.Clear();

                Console.Write("Podaj ilość: ");
                int count = MainMenuManager.SelectNumber(1, 9999);
                Console.Clear();

                // Zmienia pudełko
                Storage.sections[which].Boxes[selectedBox_toChange].ChangeInfo(objectsName, count);
                Logger.SendLog($"Zmienił dane pudełka #{selectedBox_toChange} z sekcji #{which}");
                // Zdarzenie kończy operację i wyświetla komunikat
                OnAction?.Invoke("Pudełko zostało zmienione");
                return;
            }
            // Metoda usuwająca pudełko
            private static void RemoveBox(int which)
            {
                // Sprawdzenie czy podana sekcja jest pusta
                if (Storage.sections[which].Boxes.Count == 0)
                {
                    // Zdarzenie kończy operację i wyświetla komunikat
                    OnAction?.Invoke("Brak pudełek w tej sekcji!");
                    return;
                }

                // Pobieranie od użytkownika indeksu pudełka i usuwanie go
                Console.Write($"Wybierz pudełko(x{Storage.sections[which].Boxes.Count}): ");
                int whichBox = MainMenuManager.SelectNumber(1, Storage.sections[which].Boxes.Count) - 1;
                Storage.sections[which].RemoveBox(whichBox);
                Console.Clear();
                Console.WriteLine();

                Logger.SendLog($"Usunął pudełko #{whichBox} z sekcji #{which}");
                // Zdarzenie kończy operację i wyświetla komunikat
                OnAction?.Invoke("Pudełko zostało usunięte");
                return;
            }
        }
    }
}
