using System;
using System.Globalization;

namespace ContactsAppUI
{
    public class Contact : ICloneable
    {
        private string _surname;
        private string _name;
        private PhoneNumber _number = new PhoneNumber();
        private DateTime _birthday = new DateTime();
        private string _email;
        private string _iDVK;

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
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                _surname = ti.ToTitleCase(_surname);
            }
        }

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
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                _name = ti.ToTitleCase(_name);
            }
        }

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

        public string Email { get; set; }

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