using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsApp
{
    public partial class AddEditForm : Form
    {
        private Contact _contact = new Contact();
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
        public AddEditForm()
        {
            InitializeComponent();
        }

        private void AddEditForm_Load(object sender, EventArgs e)
        {
            
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                _contact.Surname = SurnameTextBox.Text;
                _contact.Name = NameTextBox.Text;
                _contact.Birthday = BirthdayDateTimePicker.Value;
                _contact.Number = new PhoneNumber();
                if (PhoneTextBox.Text != "")
                    _contact.Number.Number = long.Parse(PhoneTextBox.Text);
                else
                    _contact.Number.Number = 0;
                _contact.Email = EmailTextBox.Text;
                _contact.IDVK = IDVKTextBox.Text;
                DialogResult = DialogResult.OK;
            }
            catch(ArgumentException exception)
            {
                MessageBox.Show(exception.Message);
            }
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
    }
}
