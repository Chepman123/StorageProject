using System.Text.Json;
using StorageProject.Models.Storage;
using StorageProject.Models.Users;
using StorageProject.utils;

namespace StorageProject.Menus
{
    class DataSystem
    {
        #region HelpMethods
        private static string GetFolderPath()
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Storage");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            return folderPath;
        }
        private static string GetFilePath()
        {
            string folderPath = GetFolderPath();

            string filePath = Path.Combine(folderPath,"Data.json");
            if (!File.Exists(filePath)) File.Create(filePath).Dispose();
             
            return filePath;
        }
        private static void CreateBackupData()
        {
            string json = File.ReadAllText(GetFilePath());
            string folderPath = GetFolderPath();

            string filePath = Path.Combine(folderPath,"BackUpData.json");

            File.WriteAllText(filePath,json);
        }
        private static void ErrorData(string message)
        {
            CreateBackupData();

            File.Delete(GetFilePath());
            Console.WriteLine(message);
            Logger.SendLog($"Failed save read: {message}");
            Console.ReadKey();
        }
        #endregion
        #region SaveData
        public static void SaveData()
        {
            try
            {
                string filePath = GetFilePath();

                List<Section> sections = new List<Section>();

                if (Storage.sections.Count > 0) sections = Storage.sections;

                string data = JsonSerializer.Serialize(sections);

                File.WriteAllText(filePath,data);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
        #endregion
        #region ReadData
        public static void GetData()
        {
            try
            {
                string filepath = GetFilePath();
                string json = File.ReadAllText(filepath);

                if (string.IsNullOrWhiteSpace(json)) throw new InvalidDataException("");

                List<Section> sections = JsonSerializer.Deserialize<List<Section>>(json);
                Storage.sections = sections;
            }
            catch (FileNotFoundException)
            {
                ErrorData("Brak pliku");
            }
            catch (IOException ex)
            {
                ErrorData($"Błąd przy odczytu pliku: {ex.Message}");
            }
            catch (FormatException)
            {
                ErrorData("Nie prawidłowy format");
            }
            catch (InvalidDataException)
            {
                ErrorData("Uszkodzone dane");
            }
            catch (ArgumentException)
            {
                ErrorData("Uszkodzone dane");
            }
            catch (Exception ex)
            {
                ErrorData($"Błąd:{ex}");
            }
        }
        #endregion
    }
}
