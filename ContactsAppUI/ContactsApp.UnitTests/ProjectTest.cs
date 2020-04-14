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

        private Contact _contact;

        public void InitProject()
        { 
            _project = new Project();
            _project.Contacts = new List<Contact>();
            for (int i = 0; i < 3; i++)
            {
                _contact = new Contact();
                _contact.Number = new PhoneNumber();
                switch (i)
                {
                    case 0:
                        _contact.Surname = "Сергеев";
                        _contact.Name = "Сергей";
                        _contact.Birthday = DateTime.Parse("08/04/1999");
                        break;
                    case 1:
                        _contact.Surname = "Александров";
                        _contact.Name = "Александр";
                        _contact.Birthday = DateTime.Parse("08/04/2005");
                        break;
                    case 2:
                        _contact.Surname = "Петров";
                        _contact.Name = "Петр";
                        _contact.Birthday = DateTime.Parse("05/02/2010");
                        break;
                }
                _contact.Number.Number = 79995553344;
                _contact.Email = "qwerty@mail.ru";
                _contact.IDVK = "id155";
                _project.Contacts.Add(_contact);
            }
        }

        /// <summary>
        /// Метод с тестом геттера поля Contacts
        /// </summary>
        [Test(Description = "Позитивный тест геттера Contacts")]
        public void TestContactsGet_CorrectValue()
        {
            InitProject();
            var expected = _project.Contacts;
            _project.Contacts = expected;
            var actual = _project.Contacts;

            Assert.AreEqual(expected, actual, "Геттер Contacts возвращает неправильный список контактов");
        }

        /// <summary>
        /// Метод с тестом метода SortedContacts
        /// </summary>
        [Test(Description = "Позитивный тест метода SortedContacts")]
        public void TestProjectSortedContacts_CorrectValue()
        {
            InitProject();
            _project.Contacts.Add(_project.Contacts[0]);
            _project.Contacts.RemoveAt(0);
            var expected = _project.Contacts;
            InitProject();
            _project.Contacts = _project.SortedContacts();
            var actual = _project.Contacts;

            Assert.AreEqual(expected, actual, "Метод SortedContacts возвращает неправильный список контактов");
        }

        /// <summary>
        /// Метод с тестом метода BirthdayContacts
        /// </summary>
        [Test(Description = "Позитивный тест метода BirthdayContacts")]
        public void TestProjectBirthdayContacts_CorrectValue()
        {
            InitProject();
            _project.Contacts.RemoveAt(2);
            var expected = _project.Contacts;
            InitProject();
            _project.Contacts = _project.BirthdayContacts(DateTime.Parse("08/04/2000"));
            var actual = _project.Contacts;

            Assert.AreEqual(expected.Count, actual.Count, "Метод BirthdayContacts возвращает неправильный список контактов");
        }
    }
}
