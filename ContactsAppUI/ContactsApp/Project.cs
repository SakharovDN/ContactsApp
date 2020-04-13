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

        /// <summary>
        /// Возвращает отсортированный по фамилии список контактов
        /// </summary>
        public List<Contact> SortedContacts()
        {
            Contacts.Sort(delegate (Contact contact1, Contact contact2)
            { return contact1.Surname.CompareTo(contact2.Surname); });
            return Contacts;
        }

        /// <summary>
        /// Возвращает список контактов, у которых сегодня день рождения
        /// </summary>
        public List<Contact> BirthdayContacts(DateTime today)
        {
            var birthdayContacts = new List<Contact>();
            foreach (var contact in Contacts)
            {
                if (contact.Birthday.Month == today.Month && contact.Birthday.Day == today.Day)
                    birthdayContacts.Add(contact);
            }
            return birthdayContacts;
        }

        public bool Equals(Project other)
        {
            //TODO: лучше инвертировать условие для уменьшения вложенности и повышения читаемости. Если количество не равно, то сразу вернуть false
            if (other.Contacts.Count == this.Contacts.Count)
                for (int i = 0; i < other.Contacts.Count; i++)
                {
                    //TODO: лучше инвертировать условие. Если не равны, то сразу вернуть false.
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
