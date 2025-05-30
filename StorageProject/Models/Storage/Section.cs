﻿namespace StorageProject.Models.Storage
{
        public class Section
        {
            // Podstawowe wartości sekcji
            public int id_Section { get; set; }
            public List<Box> Boxes { get; set; } = new List<Box>();
        
            public Section() { }

            // Przypisuje id przy tworzeniu
            public Section(int id)
            {
                id_Section = id;
            }
            // Dodaje pudełko do sekcji
            public void AddBox(Box box)
            {
                Boxes.Add(box);
            }
            // Usuwa pudełko z sekcji
            public void RemoveBox(int id)
            {
                Boxes.RemoveAt(id);
                for(int i = 0; i < Boxes.Count; i++)
                {
                    Boxes[i].id = i;
                }
            }
            // Wyświetla informację o wszystkich pudełkach
            public void DisplayInfo()
            {
                Console.WriteLine($"Sekcja #{id_Section+1}");
                Console.WriteLine("Zawartość:");

                //Sprawdzenie czy jest sekcja pusta
                if(Boxes.Count == 0)
                {
                    Console.WriteLine("Sekcja jest pusta");
                    return;
                }
                foreach(Box box in Boxes)
                {
                    box.ShowInfo();
                }
            }
        }

    
}
