using System;

namespace ContactsAppUI
{
    internal class PhoneNumber
    {
        private long _number;

        public long Number
        {
            get { return _number; }

            set
            {
                if (Convert.ToString(value).Length != 11)
                    throw new Exception("Номер должен состоять из 11 цифр");
                else if (value / 10000000000 != 7)
                    throw new Exception("Номер должен начинаться с цифры 7");
                else
                    _number = value;
            }
        }
    }
}