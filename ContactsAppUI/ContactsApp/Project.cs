using System;
using System.Collections.Generic;

namespace ContactsApp
{
    /// <summary>
    /// Класс проекта, хранящий информацию о всех контактах
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Задает и возвращает список всех контактов
        /// </summary>
        public List<Contact> Contacts { get; set; }
    }
}
