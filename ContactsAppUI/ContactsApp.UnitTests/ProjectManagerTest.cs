using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace ContactsApp.UnitTests
{
    //TODO: файлы с тестовыми данными (referenceFile) лучше помещать в подпапку TestData, чтобы файлы не путались с самими тестами
    /// <summary>
    /// Класс, в котором проводятся юнит-тесты для класса ProjectManager
    /// </summary>
    [TestFixture]
    class ProjectManagerTest
    {
        //TODO: нет негативных тестов на отсутствие считываемого файла

        private ProjectManager _projectManager; 
        private Project _project;
        //TODO: это поле не нужно - используется только внутри одного метода, может быть локальной переменной
        private Contact _contact;
        private readonly string pathToReferenceFile = AppDomain.CurrentDomain.BaseDirectory + @"\referenceFile.notes";
        private readonly string pathToTestFile = AppDomain.CurrentDomain.BaseDirectory + @"\testFile.notes";

        //TODO: Рекомендуется не использовать атрибут Сетап, а лучше явно вызывать метод внутри каждого теста - код тестов должен быть максимально прозрачным, а неявно вызываемые методы сбивают с толка.
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
