using System;
using System.Globalization;

namespace ContactsApp
{
    /// <summary>
    /// Класс контакта, хранящий информацию о фамилии, имени,
    /// номере телефона, дне рождения, E-mail'a и idVK
    /// </summary>
    public class Contact : ICloneable, IEquatable<Contact>
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        private string _surname;

        /// <summary>
        /// Имя
        /// </summary>
        private string _name;

        /// <summary>
        /// День рождения
        /// </summary>
        private DateTime _birthday = new DateTime();

        /// <summary>
        /// idVK
        /// </summary>
        private string _iDVK;

        /// <summary>
        /// Класс, который нужен для преобразования строк: первая буква в верхний регистр, остальные в нижний
        /// </summary>
        private readonly TextInfo _firstUppercaseLetter = CultureInfo.CurrentCulture.TextInfo;

        /// <summary>
        /// Возвращает и задает фамилию
        /// </summary>
        public string Surname
        {
            get { return _surname; }

            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("Введите фамилию");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("Фамилия не должна превышать 50 символов");
                }
                else
                _surname = _firstUppercaseLetter.ToTitleCase(value);
            }
        }

        /// <summary>
        /// Возвращает и задает имя
        /// </summary>
        public string Name
        {
            get { return _name; }

            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Введите имя");
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("Фамилия не должна превышать 50 символов");
                }
                else
                _name = _firstUppercaseLetter.ToTitleCase(value);
            }
        }

        /// <summary>
        /// Возвращает и задает номер телефона
        /// </summary>
        public PhoneNumber Number { get; set; }

        /// <summary>
        /// Возвращает и задает день рождения
        /// </summary>
        public DateTime Birthday
        {
            get { return _birthday; }

            set
            {
                DateTime minDate = new DateTime(1900, 01, 01);
                if (value > DateTime.Today || value < minDate)
                    throw new ArgumentException("Дата рождения не корректна");
                else
                    _birthday = value;
            }
        }

        /// <summary>
        /// Возвращает и задает E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Возвращает и задает idVk
        /// </summary>
        public string IDVK
        {
            get { return _iDVK; }

            set
            {
                if (value.Length > 15)
                {
                    throw new ArgumentException("IDVK не должен превышать 15 символов");
                }
                else
                    _iDVK = value;
            }
        }

        /// <summary>
        /// Метод, выполняющий клонирование контакта
        /// </summary>
        public object Clone()
        {
            Contact contact = new Contact();
            contact.Surname = this.Surname;
            contact.Name = this.Name;
            contact.Number = this.Number;
            contact.Birthday = this.Birthday;
            contact.Email = this.Email;
            contact.IDVK = this.IDVK;
            return contact;
        }

        public bool Equals(Contact other)
        {
            if (other.Surname == this.Surname && other.Name == this.Name
                && other.Birthday == this.Birthday && other.Number.Equals(this.Number)
                && other.Email == this.Email && other.IDVK == this.IDVK)
                return true;
            return false;
        }
    }
}