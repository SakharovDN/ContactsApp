using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ContactsApp.UnitTests
{
    /// <summary>
    /// Класс, в котором проводятся юнит-тесты для класса PhoneNumber
    /// </summary>
    [TestFixture]
    class PhoneNumberTest
    {
        private PhoneNumber _number;

        [SetUp]
        public void InitContact()
        {
            _number = new PhoneNumber();
        }

        /// <summary>
        /// Метод с тестом геттера поля Number
        /// </summary>
        [Test(Description = "Позитивный тест геттера Number")]
        public void TestNumberGet_CorrectValue()
        {
            var expected = 79996195751;
            _number.Number = expected;
            var actual = _number.Number;

            Assert.AreEqual(expected, actual, "Геттер Number возвращает неправильный номер");
        }

        /// <summary>
        /// Метод с тестами сеттера поля Number
        /// </summary>
        [TestCase(7999, "Должно возникать исключение, если номер состоит не из 11 цифр",
            TestName = "Присвоение цифры не из 11 цифр в качестве номера")]
        [TestCase(89996195751, "Должно возникать исключение, если номер начинается не с цифры 7",
            TestName = "Присвоение неправильного номера, который не начинается с цифры 7")]
        public void TestSurnameSet_ArgumentException(long wrongNumber, string message)
        {
            Assert.Throws<ArgumentException>(
                () => { _number.Number = wrongNumber; },
                message);
        }
    }
}
