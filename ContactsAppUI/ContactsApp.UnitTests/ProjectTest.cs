using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ContactsApp.UnitTests
{
    /// <summary>
    /// Класс, в котором проводятся юнит-тесты для класса Project
    /// </summary>
    [TestFixture]
    class ProjectTest
    {
        private Project _project;
        //TODO: лучше сделать локальной переменной
        private Contact _contact;

        //TODO: сетап для одного теста смысла делать нет, к тому же большая часть инициализации осталась в самом тесте
        [SetUp]
        public void InitProject()
        {
            _project = new Project();
            _contact = new Contact();
            _contact.Number = new PhoneNumber();
        }

        /// <summary>
        /// Метод с тестом геттера поля Contacts
        /// </summary>
        [Test(Description = "Позитивный тест геттера Contacts")]
        public void TestContactsGet_CorrectValue()
        {
            var expected = new List<Contact>();
            for(int i = 0; i < 3; i++)
            {
                _contact.Surname = "Сергеев";
                _contact.Name = "Сергей";
                _contact.Birthday = DateTime.Parse("01/01/2001");
                _contact.Number.Number = 79995553344;
                _contact.Email = "qwerty@mail.ru";
                _contact.IDVK = "id155";
                expected.Add(_contact);
            }
            _project.Contacts = expected;
            var actual = _project.Contacts;

            Assert.AreEqual(expected, actual, "Геттер Contacts возвращает неправильный номер");
        }
    }
}
