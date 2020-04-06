using System;
using NUnit.Framework;

namespace ContactsApp.UnitTests
{
    /// <summary>
    /// Класс, в котором проводятся юнит-тесты для класса Contact
    /// </summary>
    [TestFixture]
    public class ContactTest
    {
        private Contact _contact;

        [SetUp]
        public void InitContact()
        {
            _contact = new Contact();
        }

        /// <summary>
        /// Метод с юнит-тестом геттера поля Surname
        /// </summary>
        [Test(Description = "Позитивный тест геттера Surname")]
        public void TestSurnameGet_CorrectValue()
        {
            var expected = "Смирнов";
            _contact.Surname = expected;
            var actual = _contact.Surname;

            Assert.AreEqual(expected, actual, "Геттер Surname возвращает неправильную фамилию");
        }

        /// <summary>
        /// Метод с юнит-тестами сеттера поля Surname
        /// </summary>
        [TestCase("", "Должно возникать исключение, если фамилия - пустая строка",
            TestName = "Присвоение пустой строки в качестве фамилии")]
        [TestCase("Смирнов-Смирнов-Смирнов-Смирнов-Смирнов-Смирнов-Смирнов", 
            "Должно возникать исключение, если фамилия длиннее 50 символов",
            TestName = "Присвоение неправильной фамилии больше 50 символов")]
        public void TestSurnameSet_ArgumentException(string wrongSurname, string message)
        {
            Assert.Throws<ArgumentException>(
                () => { _contact.Surname = wrongSurname; },
                message);
        }

        /// <summary>
        /// Метод с тестом геттера поля Name
        /// </summary>
        [Test(Description = "Позитивный тест геттера Name")]
        public void TestNameGet_CorrectValue()
        {
            var expected = "Андрей";
            _contact.Name = expected;
            var actual = _contact.Name;

            Assert.AreEqual(expected, actual, "Геттер Name возвращает неправильное имя");
        }

        /// <summary>
        /// Метод с тестами сеттера поля Name
        /// </summary>
        [TestCase("", "Должно возникать исключение, если имя - пустая строка",
            TestName = "Присвоение пустой строки в качестве имени")]
        [TestCase("Андрей-Андрей-Андрей-Андрей-Андрей-Андрей-Андрей-Андрей-Андрей",
            "Должно возникать исключение, если имя длиннее 50 символов",
            TestName = "Присвоение неправильного имени больше 50 символов")]
        public void TestNameSet_ArgumentException(string wrongName, string message)
        {
            Assert.Throws<ArgumentException>(
                () => { _contact.Name = wrongName; },
                message);
        }

        /// <summary>
        /// Метод с тестом геттера Number
        /// </summary>
        [Test(Description = "Позитивный тест геттера Number")]
        public void TestNumberGet_CorrectValue()
        {
            _contact.Number = new PhoneNumber();
            _contact.Number.Number = 79996195751;
            var expected = _contact.Number;
            _contact.Number = expected;
            var actual = _contact.Number;

            Assert.AreEqual(expected, actual, "Геттер Number возвращает неправильный номер");
        }

        /// <summary>
        /// Метод с тестом геттера поля Birthday
        /// </summary>
        [Test(Description = "Позитивный тест геттера Birthday")]
        public void TestBirthdayGet_CorrectValue()
        {
            var expected = new DateTime(1999, 04,02);
            _contact.Birthday = expected;
            var actual = _contact.Birthday;

            Assert.AreEqual(expected, actual, "Геттер Birthday возвращает неправильное значение");
        }

        /// <summary>
        /// Метод с тестами сеттера поля Birthday
        /// </summary>
        [TestCase("05/01/1800", "Должно возникать исключение, если дата раньше 01/01/1900",
            TestName = "Присвоение неправильной даты, которая раньше 01/01/1900")]
        [TestCase("05/01/2200",
            "Должно возникать исключение, если дата позже сегодняшнего дня",
            TestName = "Присвоение неправильной даты, которая позже сегодняшнего дня")]
        public void TestBirthdaySet_ArgumentException(string date, string message)
        {
            var wrongDate = DateTime.Parse(date);
            Assert.Throws<ArgumentException>(
                () => { _contact.Birthday = wrongDate; },
                message);
        }

        /// <summary>
        /// Метод с тестом геттера поля Email
        /// </summary>
        [Test(Description = "Позитивный тест геттера Email")]
        public void TestEmailGet_CorrectValue()
        {
            var expected = "qwerty@mail.ru";
            _contact.Email = expected;
            var actual = _contact.Email;

            Assert.AreEqual(expected, actual, "Геттер Email возвращает неправильное значение");
        }

        /// <summary>
        /// Метод с тестом геттера поля IDVK
        /// </summary>
        [Test(Description = "Позитивный тест геттера IDVK")]
        public void TestIDVKGet_CorrectValue()
        {
            var expected = "vasiliy";
            _contact.IDVK = expected;
            var actual = _contact.IDVK;

            Assert.AreEqual(expected, actual, "Геттер IDVK возвращает неправильное значение");
        }

        /// <summary>
        /// Метод с тестами сеттера поля IDVK
        /// </summary>
        [TestCase("vasiliy-vasiliy-vasiliy-vasiliy", "Должно возникать исключение, если IDVK больше 15 символов",
            TestName = "Присвоение неправильного IDVK, больше 15 символов")]
        public void TestIDVKSet_ArgumentException(string iDVK, string message)
        {
            Assert.Throws<ArgumentException>(
                () => { _contact.IDVK = iDVK; },
                message);
        }
    }
}
