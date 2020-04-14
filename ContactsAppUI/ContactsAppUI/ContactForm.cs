using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ContactsApp
{
    public partial class ContactForm : Form
    {
        /// <summary>
        /// Массив, в котором содержатся объекты (все TextBox'ы и DateTimePicker)
        /// </summary>
        private Control[] _elements;

        /// <summary>
        /// Контакт
        /// </summary>
        private Contact _contact = new Contact();

        /// <summary>
        /// Задает и возвращает контакт
        /// </summary>
        public Contact Contact
        {
            get
            {
                if (DialogResult == DialogResult.Cancel)
                    return null;
                else
                    return _contact;
            }
            set
            {
                _contact = value;
                SurnameTextBox.Text = value.Surname;
                NameTextBox.Text = value.Name;
                BirthdayDateTimePicker.Value = value.Birthday;
                PhoneTextBox.Text = value.Number.Number.ToString();
                EmailTextBox.Text = value.Email;
                IDVKTextBox.Text = value.IDVK;
            }
        }

        public ContactForm()
        {
            InitializeComponent();
            _elements = new Control[] { SurnameTextBox, NameTextBox, BirthdayDateTimePicker, PhoneTextBox, EmailTextBox, IDVKTextBox };
        }

        /// <summary>
        /// Нажатие кнопки OK
        /// </summary>
        private void OKButton_Click(object sender, EventArgs e)
        {
            SurnameTextBox.Focus();
            NameTextBox.Focus();
            BirthdayDateTimePicker.Focus();
            PhoneTextBox.Focus();
            EmailTextBox.Focus();
            IDVKTextBox.Focus();
            OKButton.Focus();
            if(IsValid())
                DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Нажатие кнопки Cancel
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Метод, позволяющий вводить в PhoneTextBox только цифры и клавишу Backspace
        /// </summary>
        private void PhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != (char)Keys.Back)
                e.Handled = true;
        }

        /// <summary>
        /// Метод, присваивающий фамилию
        /// </summary>
        private void SurnameTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _contact.Surname = SurnameTextBox.Text;
                errorProvider.SetError(SurnameTextBox, "");
            }
            catch (ArgumentException exception)
            {
                errorProvider.SetError(SurnameTextBox, exception.Message);
            }
        }

        /// <summary>
        /// Метод, присваивающий имя
        /// </summary>
        private void NameTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _contact.Name = NameTextBox.Text;
                errorProvider.SetError(NameTextBox, "");
            }
            catch (ArgumentException exception)
            {
                errorProvider.SetError(NameTextBox, exception.Message);
            }
        }

        /// <summary>
        /// Метод, присваивающий день рождения
        /// </summary>
        private void BirthdayDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _contact.Birthday = BirthdayDateTimePicker.Value;
                errorProvider.SetError(BirthdayDateTimePicker, "");
            }
            catch (ArgumentException exception)
            {
                errorProvider.SetError(BirthdayDateTimePicker, exception.Message);
            }
        }

        /// <summary>
        /// Метод, присваивающий номер телефона
        /// </summary>
        private void PhoneTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _contact.Number = new PhoneNumber();
                _contact.Number.Number = long.Parse(PhoneTextBox.Text);
                errorProvider.SetError(PhoneTextBox, "");
            }
            catch (ArgumentException exception)
            {
                errorProvider.SetError(PhoneTextBox, exception.Message);
            }
            catch (FormatException)
            {
                errorProvider.SetError(PhoneTextBox, "Введите номер телефона");
            }
        }

        /// <summary>
        /// Метод, присваивающий E-mail
        /// </summary>
        private void EmailTextBox_Validating(object sender, CancelEventArgs e)
        {
            _contact.Email = EmailTextBox.Text;
        }

        /// <summary>
        /// Метод, присваивающий idVK
        /// </summary>
        private void IDVKTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _contact.IDVK = IDVKTextBox.Text;
                errorProvider.SetError(IDVKTextBox, "");
            }
            catch (ArgumentException exception)
            {
                errorProvider.SetError(IDVKTextBox, exception.Message);
            }
        }

        /// <summary>
        /// Метод, который проверяет наличие ошибок
        /// </summary>
        private bool IsValid()
        {
            foreach (Control control in _elements)
                if (errorProvider.GetError(control) != "")
                    return false;
            return true;
        }
    }
}
