﻿using System;

namespace ContactsApp
{
    /// <summary>
    /// Класс номера телефона, хранящий информацию о номере телефона
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// Номер телефона
        /// </summary>
        private long _number;

        /// <summary>
        /// Задает и возвращает номер телефона
        /// </summary>
        public long Number
        {
            get { return _number; }

            set
            {
                if (value.ToString().Length != 11 || value.ToString().Length != 0)
                    throw new ArgumentException("Номер должен состоять из 11 цифр");
                else if (value.ToString()[0] != '7')
                    throw new ArgumentException("Номер должен начинаться с цифры 7");
                else
                    _number = value;
            }
        }
    }
}