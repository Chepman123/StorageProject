using StorageProject.Interfaces;
using StorageProject.Models.Users;

namespace StorageProject.Models.Storage
{
    public class Boxes
    {
        public abstract class Box : IBox
        {
            public int id { get; set; }
            public int id_section { get; set; }
            public string objectsName { get; set; }
            public int count { get; set; }
            private User _owner;

            // Obiekt User zwraca wartość _owner. Przypisuje wartość tylko jeśli użytkownik jest klientem
            public User owner
            {
                get
                {
                    return _owner;
                }
                set
                {
                    if (value.Role == Role.Client)
                    {
                        _owner = value;
                    }
                    else
                    {
                        throw new ArgumentException("Właściciel musi mieć rolę Klienta");
                    }
                }
            }
            // Przypisuje podstawowe wartości przy tworzeniu
            public Box(int id, string objectsName, int count, User owner)
            {
                this.id = id;
                this.objectsName = objectsName;
                this.count = count;
                _owner = owner;
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
        public class BoxWithFruits : Box
        {
            public string FruitType { get; private set; }
            public BoxWithFruits(int id, int id_section, int count, string FruitType, User owner) : base(id, FruitType, count,owner)
            {
                this.FruitType = FruitType;
            }
            public override void ShowInfo()
            {
                base.ShowInfo();
                Console.WriteLine($"fruit type: {FruitType}");
            }
        }
        public class BoxWithVegetables : Box
        {
            public string VegetableType { get; private set; }
            public BoxWithVegetables(int id, int id_section, int count, string VegetableType, User owner) : base(id, VegetableType, count, owner)
            {
                this.VegetableType = VegetableType;
            }
            public override void ShowInfo()
            {
                base.ShowInfo();
                Console.WriteLine($"vegetable type: {VegetableType}");
            }
        }
    }
}
