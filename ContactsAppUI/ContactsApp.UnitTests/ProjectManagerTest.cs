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
        private ProjectManager _projectManager;
        private Project _project;
        private Contact _contact;
        private readonly string pathToReferenceFile = AppDomain.CurrentDomain.BaseDirectory + @"\referenceFile.notes";
        private readonly string pathToTestFile = AppDomain.CurrentDomain.BaseDirectory + @"\testFile.notes";

        [SetUp]
        public void InitProjectManager()
        {
            _projectManager = new ProjectManager();
            _project = new Project();
            _project.Contacts = new List<Contact>();
            _contact = new Contact();
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
            var expected = File.ReadAllText(pathToReferenceFile);
            _projectManager.SaveToFile(_project, pathToTestFile);
            var actual = File.ReadAllText(pathToTestFile);

            Assert.AreEqual(expected, actual, "Метод SaveToFile неверно сохраняет данные");
        }

        /// <summary>
        /// Метод с тестом метода LoadFromFile
        /// </summary>
        [Test(Description = "Позитивный тест метода LoadFormFile")]
        public void TestProjectManagerLoadFromFile_CorrectValue()
        {
            var expected = _project;
            var actual = _projectManager.LoadFromFile(pathToReferenceFile);

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
