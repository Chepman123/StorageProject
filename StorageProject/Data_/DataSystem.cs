using StorageProject.Models.Storage;
using StorageProject.Models.Users;

namespace StorageProject.Menus
{
    class DataSystem
    {
        public static void SaveData()
        {
            string filepath = "storage.txt";
            if (!File.Exists(filepath))
            {
                Console.WriteLine("Plik został stworzony");
                File.Create(filepath).Dispose();
            }
            File.WriteAllText(filepath, $"Sections count: {Storage.sections.Count}" + Environment.NewLine);

            for (int i = 0; i < Storage.sections.Count; i++)
            {
                File.AppendAllText(filepath, $"Section: {i}" + Environment.NewLine);
                File.AppendAllText(filepath, $"BoxesCount: {Storage.sections[i].Boxes.Count}" + Environment.NewLine);
                for (int j = 0; j < Storage.sections[i].Boxes.Count; j++)
                {
                    Boxes.Box box = Storage.sections[i].Boxes[j];
                    string type;
                    if (box is Boxes.BoxWithFruits)
                    {
                        type = "Fruits";
                    }
                    else
                    {
                        type = "Vegetables";
                    }
                    File.AppendAllText(filepath, $"Box: {j}, {type} , {box.count}, {box.objectsName}" + Environment.NewLine);
                }
                File.AppendAllText(filepath, Environment.NewLine);
            }

            Console.WriteLine("Dane zostały zapisane");
        }
        private static bool CheckData(string filepath)
        {
            bool result = true;

            try
            {
                string[] allLines = File.ReadAllLines(filepath);
                for (int i = 0; i < allLines.Length; i++)
                {
                    if (allLines[i].Split(":")[0] == "Section")
                    {
                        int section = int.Parse(allLines[i].Split(":")[1]);
                        if (int.Parse(allLines[i + 1].Split(": ")[1]) < 0)
                        {

                            throw new ArgumentException();
                        }
                        for (int j = 0; j < int.Parse(allLines[i + 1].Split(": ")[1]); j++)
                        {

                            string[] BoxesInfo = allLines[i + j + 2].Split(": ")[1].Split(", ");
                            if (BoxesInfo.Length < 5)
                            {
                                throw new InvalidDataException();
                            }
                            int id = int.Parse(BoxesInfo[0]);
                            if (id < 0 || id > int.Parse(allLines[i + 1].Split(": ")[1]))
                            {

                                throw new ArgumentException();
                            }
                            string type = BoxesInfo[1].Trim();
                            if (type != "Fruits" && type != "Vegetables")
                            {
                                throw new ArgumentException();
                            }
                            int count = int.Parse(BoxesInfo[2]);
                            if (count <= 0)
                            {
                                throw new ArgumentException();
                            }
                            string objectsName = BoxesInfo[3];
                            string login = BoxesInfo[4].Trim();
                            User owner = new User(login, Role.Client);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Brak pliku");
                Console.ReadKey();
                return false;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Błąd przy odczytu pliku: {ex.Message}");
                Console.ReadKey();
                return false;
            }
            catch (FormatException)
            {
                Console.WriteLine("Nie prawidłowy format");
                Console.ReadKey();
                return false;
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("Uszkodzone dane");
                Console.ReadKey();
                return false;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Uszkodzone dane");
                Console.ReadKey();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd:{ex}");
                Console.ReadKey();
                return false;
            }
            return result;
        }
        public static void GetData()
        {
            string filepath = "storage.txt";
            if (CheckData(filepath))
            {
                string[] allLines = File.ReadAllLines(filepath);
                for (int i = 0; i < allLines.Length; i++)
                {
                    if (allLines[i].Split(":")[0] == "Section")
                    {
                        Storage.sections.Add(new (Storage.sections.Count));
                        int section = int.Parse(allLines[i].Split(":")[1]);
                        for (int j = 0; j < int.Parse(allLines[i + 1].Split(": ")[1]); j++)
                        {

                            string[] BoxesInfo = allLines[i + j + 2].Split(": ")[1].Split(", ");

                            int id = int.Parse(BoxesInfo[0]);

                            string type = BoxesInfo[1].Trim();
                            int count = int.Parse(BoxesInfo[2]);

                            string objectsName = BoxesInfo[3];
                            string login = BoxesInfo[4].Trim();
                            Boxes.Box box;
                            User owner = new User(login, Role.Client);
                            if (type == "Fruits")
                            {
                                box = new Boxes.BoxWithFruits(id, section, count, objectsName, owner);
                            }
                            else
                            {
                                box = new Boxes.BoxWithVegetables(id, section, count, objectsName, owner);
                            }
                            Storage.sections[section].AddBox(box);
                        }
                    }
                }
                Console.WriteLine("Dane zostały wczytane!");
            }
        }

    }
}
