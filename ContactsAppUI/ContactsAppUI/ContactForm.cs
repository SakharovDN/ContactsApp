using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ContactsApp
{
    public partial class ContactForm : Form
    {
        private Control[] controls;
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
                SurnameTextBox.Text = value.Surname;
                NameTextBox.Text = value.Name;
                BirthdayDateTimePicker.Value = value.Birthday;
                if (value.Number.Number != 0)
                    PhoneTextBox.Text = value.Number.Number.ToString();
                EmailTextBox.Text = value.Email;
                IDVKTextBox.Text = value.IDVK;
                _contact = value;
            }
        }

        public ContactForm()
        {
            InitializeComponent();
            controls = new Control[] { SurnameTextBox, NameTextBox, BirthdayDateTimePicker, PhoneTextBox, EmailTextBox, IDVKTextBox };
        }

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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != (char)Keys.Back)
                e.Handled = true;
        }

        private void SurnameTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _contact.Surname = SurnameTextBox.Text;
                errorProvider1.SetError(SurnameTextBox, "");
            }
            catch (ArgumentException exception)
            {
                errorProvider1.SetError(SurnameTextBox, exception.Message);
            }
        }

        private void NameTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _contact.Name = NameTextBox.Text;
                errorProvider1.SetError(NameTextBox, "");
            }
            catch (ArgumentException exception)
            {
                errorProvider1.SetError(NameTextBox, exception.Message);
            }
        }

        private void BirthdayDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _contact.Birthday = BirthdayDateTimePicker.Value;
                errorProvider1.SetError(BirthdayDateTimePicker, "");
            }
            catch (ArgumentException exception)
            {
                errorProvider1.SetError(BirthdayDateTimePicker, exception.Message);
            }
        }

        private void PhoneTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _contact.Number = new PhoneNumber();
                if (PhoneTextBox.Text != "")
                    _contact.Number.Number = long.Parse(PhoneTextBox.Text);
                else
                    _contact.Number.Number = 0;
                errorProvider1.SetError(PhoneTextBox, "");
            }
            catch (ArgumentException exception)
            {
                errorProvider1.SetError(PhoneTextBox, exception.Message);
            }
        }

        private void EmailTextBox_Validating(object sender, CancelEventArgs e)
        {
            _contact.Email = EmailTextBox.Text;
        }

        private void IDVKTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _contact.IDVK = IDVKTextBox.Text;
                errorProvider1.SetError(IDVKTextBox, "");
            }
            catch (ArgumentException exception)
            {
                errorProvider1.SetError(IDVKTextBox, exception.Message);
            }
        }

        private bool IsValid()
        {
            foreach (Control control in controls)
                if (errorProvider1.GetError(control) != "")
                    return false;
            return true;
        }

        private void ContactForm_Load(object sender, EventArgs e)
        {

        }
    }
}
