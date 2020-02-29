using System;
using System.Globalization;

namespace ContactsAppUI
{
    public class Contact
    {
        private string _surname;

        public string Surname
        {
            get { return _surname; }

            set
            {
                if (value.Length > 50)
                {
                    throw new Exception("Фамилия не должна превышать 50 символов");
                }
                else
                    _surname = value;
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                _surname = ti.ToTitleCase(_surname);
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }

            set
            {
                if (value.Length > 50)
                {
                    throw new Exception("Фамилия не должна превышать 50 символов");
                }
                else
                    _name = value;
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                _name = ti.ToTitleCase(_name);
            }
        }

        private DateTime _birthday = new DateTime();
        
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                DateTime dateFrom = new DateTime(1900, 01, 01);
                if (value > DateTime.Today && value < dateFrom)
                    throw new Exception("Дата рождения не корректна");
                else
                    _birthday = value;

            }
        }

        private string _email;

        public string Email { get; set; }

        private string _iDVK;

        public string IDVK
        {
            get { return _iDVK; }

            set
            {
                if (value.Length > 15)
                {
                    throw new Exception("Фамилия не должна превышать 50 символов");
                }
                else
                    _iDVK = value;
            }
        }

    }
}