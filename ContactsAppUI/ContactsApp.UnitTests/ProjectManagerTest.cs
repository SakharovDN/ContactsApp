using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace ContactsApp.UnitTests
{
    /// <summary>
    /// Класс, в котором проводятся юнит-тесты для класса ProjectManager
    /// </summary>
    [TestFixture]
    class ProjectManagerTest
    {
        private Project _project;
        private readonly string pathToReferenceFile = AppDomain.CurrentDomain.BaseDirectory + @"\TestData\referenceFile.notes";
        private readonly string wrongPathToReferenceFile = AppDomain.CurrentDomain.BaseDirectory + @"\referenceFile.notes";
        private readonly string pathToTestFile = AppDomain.CurrentDomain.BaseDirectory + @"\testFile.notes";

        
        public void InitProjectManager()
        {
            _project = new Project();
            _project.Contacts = new List<Contact>();
            var _contact = new Contact();
            _contact.Number = new PhoneNumber();
            for (int i = 0; i < 3; i++)
            {
                _contact.Surname = "Сергеев";
                _contact.Name = "Сергей";
                _contact.Birthday = DateTime.Parse("01/01/2001");
                _contact.Number.Number = 79995553344;
                _contact.Email = "qwerty@mail.ru";
                _contact.IDVK = "id155";
                _project.Contacts.Add(_contact);
            }
        }

        /// <summary>
        /// Метод с тестом метода SaveToFile
        /// </summary>
        [Test(Description = "Позитивный тест метода SaveToFile")]
        public void TestProjectManagerSaveToFile_CorrectValue()
        {
            InitProjectManager();
            var expected = File.ReadAllText(pathToReferenceFile);
            ProjectManager.SaveToFile(_project, pathToTestFile);
            var actual = File.ReadAllText(pathToTestFile);

            Assert.AreEqual(expected, actual, "Метод SaveToFile неверно сохраняет данные");
        }

        /// <summary>
        /// Метод с тестом метода LoadFromFile
        /// </summary>
        [Test(Description = "Позитивный тест метода LoadFormFile")]
        public void TestProjectManagerLoadFromFile_CorrectValue()
        {
            InitProjectManager();
            var expected = _project;
            var actual = ProjectManager.LoadFromFile(pathToReferenceFile);

            Assert.AreEqual(expected, actual, "Метод LoadFromFile неверно загружает данные");
        }

        /// <summary>
        /// Метод с негативным тестом метода LoadFromFile
        /// </summary>
        [Test(Description = "Негативный тест метода LoadFromFile")]
        public void TestProjectManagerLoadFromFile_WrongPath()
        {
            Project expected = null;
            var actual = ProjectManager.LoadFromFile(wrongPathToReferenceFile);

            Assert.AreEqual(expected, actual, "Метод LoadFromFile неверно загружает данные");
        }

        [TearDown]
        public void FinalProjectManager()
        {
            if (File.Exists(pathToTestFile))
                File.Delete(pathToTestFile);
        }
    }
}
