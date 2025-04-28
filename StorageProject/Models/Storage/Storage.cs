namespace StorageProject.Models.Storage
{
        // Podstawowa klasa magazynu
        public static class Storage
        {
            // Lista wszystkich sekcji
            public static List<Section> sections { get; set; } = new List<Section>();

            // Metoda zmieniająca położenie pudełka
            public static void MoveBox(int id_section, int new_id_section, int id_box)
            {
                try
                {
                    // Sprawdzenie czy pudełko już jest w wybranej sekcsji
                    if (id_section == new_id_section)
                    {
                        Console.WriteLine("Pudełko już jest w tej sekcji");
                        return;
                    }

                    // Zmiana sekcji pudełka
                    Box box = sections[id_section].Boxes[id_box];
                    sections[id_section].Boxes.Remove(box);
                    box.MoveBox(new_id_section);
                    sections[new_id_section].Boxes.Add(box);
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"Nie prawidłowy indeks sekcji lub pudełka: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Nieoczekiwany błąd: {ex.Message}");
                }
            }
            // Metoda usuwająca sekcję
            public static void RemoveSection(int id)
            {
                // Usuwa wszystkie pudełka i sekcję
                sections[id].Boxes.Clear();
                sections.RemoveAt(id);

                //Zmienia indeks sekcji w pudełkach
                for (int i = 0; i < sections.Count; i++)
                {
                    sections[i].id_Section = i;
                    foreach (Box box in sections[i].Boxes)
                    {
                        box.id_section = i;
                    }
                }
            }
            // Metoda dodająca nowe pudełko
            public static void AddBox(Box box, int id_section)
            {
                // Zmienia sekcję pudełka i dodaje go do wybranej sekcji
                box.MoveBox(id_section);
                sections[id_section].AddBox(box);
            }
            // Metoda wyświetlająca informację o wszystkich sekcjach
            public static void DisplayAllSections()
            {
                foreach (Section section in sections)
                {
                    section.DisplayInfo();
                }
            }
        }
}

