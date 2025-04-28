using StorageProject.Interfaces;
using StorageProject.Models.Users;

namespace StorageProject.Models.Storage
{
        public class Box : IBox
        {
            public int id { get; set; }
            public int id_section { get; set; }
            public string? objectsName { get; set; }
            public int count { get; set; }
            public BoxType type { get; set; }

            // Obiekt User zwraca wartość _owner. Przypisuje wartość tylko jeśli użytkownik jest klientem
            public User? owner { get; set; }
            public Box() { }
            // Przypisuje podstawowe wartości przy tworzeniu
            public Box(int id, string objectsName, int count, User owner, BoxType type)
            {
                this.id = id;
                this.objectsName = objectsName;
                this.count = count;
                this.owner = owner;
                this.type = type;
            }
            // Metoda wyświetlająca informację o pudełku
            public virtual void ShowInfo()
            {
                Console.WriteLine($"id: {id}, section: {id_section + 1}, objects: {objectsName}, count: {count}");
            }
            // Metoda zmieniająca informację o pudełku
            public void ChangeInfo(string objectsName, int count)
            {
                this.objectsName = objectsName;
                this.count = count;
            }
            // Metoda zmieniająca pozycję pudełka
            public void MoveBox(int id_section)
            {
                this.id_section = id_section;
            }
        }
    
}
public enum BoxType
{
    FruitBox,
    VegetableBox
}
