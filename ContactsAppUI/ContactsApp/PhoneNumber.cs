using System;

namespace ContactsAppUI
{
    /// <summary>
    /// Класс номера телефона, хранящий информацию о номере телефона
    /// </summary>
    public class PhoneNumber
    {
        private long _number;

        /// <summary>
        /// Задает и возвращает номер телефона
        /// </summary>
        public long Number
        {
            get { return _number; }

            set
            {
                if (value.ToString().Length != 11)
                    throw new ArgumentException("Номер должен состоять из 11 цифр");
                else if (value / 10000000000 != 7)
                    throw new ArgumentException("Номер должен начинаться с цифры 7");
                else
                    _number = value;
            }
        }
    }
}