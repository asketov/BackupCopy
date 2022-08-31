using System;
using System.IO;
using System.Linq.Expressions;
using System.Text.Json;
using System.Xml;

namespace exercise1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText(@".\SettingsFile.json");
            if (json == "")
            {
                Console.WriteLine("Неправильно указан путь или имя файла с настройками");
                return;
            }
            SettingsFile settingsFile = JsonSerializer.Deserialize<SettingsFile>(json);
            string destFolderPathName = Path.Combine(settingsFile.DestFolder, DateTime.Now.ToString("dd_MM_yyyy H-mm"));
            Directory.CreateDirectory(destFolderPathName);
            string pathLoggerFile = Path.Combine(destFolderPathName, "loggerFile.txt");
            File.AppendAllText(pathLoggerFile, "Info: Старт процесса копирования\n");
            foreach (var folder in settingsFile.SourceFolders)
            {
                string nameFolder = Path.GetFileName(folder);
                string pathToFolder = Path.Combine(destFolderPathName,nameFolder);
                Directory.CreateDirectory(pathToFolder);
                string[] files = Directory.GetFiles(folder);
                foreach (string file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var destFile = Path.Combine(pathToFolder, fileName);
                    try
                    {
                        System.IO.File.Copy(file, destFile, true);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        File.AppendAllText(pathLoggerFile, string.Format("Error: Нет доступа к файлу {0} в папке {1}\n", fileName, folder));
                    }
                    File.AppendAllText(pathLoggerFile, string.Format("Info: Закончили с файлом {0}\n", file));
                }
                File.AppendAllText(pathLoggerFile, string.Format("Info: Закончили с папкой {0}\n", folder));
            }
        }
    }
}

