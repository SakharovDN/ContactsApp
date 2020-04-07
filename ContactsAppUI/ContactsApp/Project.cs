using System;
using System.Collections.Generic;

namespace ContactsApp
{
    /// <summary>
    /// Класс проекта, хранящий информацию о всех контактах
    /// </summary>
    public class Project : IEquatable<Project>
    {
        /// <summary>
        /// Задает и возвращает список всех контактов
        /// </summary>
        public List<Contact> Contacts { get; set; }

        public bool Equals(Project other)
        {
            if (other.Contacts.Count == this.Contacts.Count)
                for (int i = 0; i < other.Contacts.Count; i++)
                {
                    if (other.Contacts[i].Equals(this.Contacts[i]))
                        continue;
                    else
                        return false;
                }
            else
                return false;
            return true;
        }
    }
}
