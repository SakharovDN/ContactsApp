using System;
using System.Globalization;

namespace ContactsApp
{
    /// <summary>
    /// Класс контакта, хранящий информацию о фамилии, имени,
    /// номере телефона, дне рождения, E-mal'a и idVK
    /// </summary>
    public class Contact : ICloneable
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
        /// Номер телефона
        /// </summary>
        private PhoneNumber _number = new PhoneNumber();
        /// <summary>
        /// День рождения
        /// </summary>
        private DateTime _birthday = new DateTime();
        /// <summary>
        /// E-mail
        /// </summary>
        private string _email;
        /// <summary>
        /// idVK
        /// </summary>
        private string _iDVK;
        private readonly TextInfo FirstUppercaseLetter = CultureInfo.CurrentCulture.TextInfo;

        /// <summary>
        /// Возвращает и задает фамилию
        /// </summary>
        public string Surname
        {
            get { return _surname; }

            set
            {
                if (value.Length > 50)
                {
                    throw new ArgumentException("Фамилия не должна превышать 50 символов");
                }
                else
                    _surname = value;
                _surname = FirstUppercaseLetter.ToTitleCase(_surname);
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
                if (value.Length > 50)
                {
                    throw new ArgumentException("Фамилия не должна превышать 50 символов");
                }
                else
                    _name = value;
                _name = FirstUppercaseLetter.ToTitleCase(_name);
            }
        }

        /// <summary>
        /// Возвращает и задает номер телефона
        /// </summary>
        public PhoneNumber Number { get; set; }

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                DateTime dateFrom = new DateTime(1900, 01, 01);
                if (value > DateTime.Today && value < dateFrom)
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
    }
}