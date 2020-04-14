using Newtonsoft.Json;
using System;
using System.IO;

namespace ContactsApp
{
    /// <summary>
    /// Класс менеджера проекта, позволяющий сохранить в файл или загрузить из файла информацию о всех контактах
    /// </summary>
    static public class ProjectManager
    {
        /// <summary>
        /// Метод загрузки информации о всех контактах из файла
        /// </summary>
        static public Project LoadFromFile(string path)
        {
            if (!File.Exists(path))
                return null;
            Project project = null;
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader(path))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                project = (Project)serializer.Deserialize<Project>(reader);
            }
            return project;
        }

        /// <summary>
        /// Метод сохранения информации о всех контактах в файл
        /// </summary>
        static public void SaveToFile(Project project, string path)
        {
            if (!File.Exists(path))
                using (FileStream fs = File.Create(path)) { }
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, project);
            }

        }
    }
}
