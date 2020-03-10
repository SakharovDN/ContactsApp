using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp
{
    /// <summary>
    /// Класс менеджера проекта, позволяющий сохранить в файл или загрузить из файла информацию о всех контактах
    /// </summary>
    public class ProjectManager
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Roaming\ContactsApp.notes";

        /// <summary>
        /// Метод загрузки информации о всех контактах из файла
        /// </summary>
        public Project LoadFromFile()
        {
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
        public void SaveToFile(Project project)
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
