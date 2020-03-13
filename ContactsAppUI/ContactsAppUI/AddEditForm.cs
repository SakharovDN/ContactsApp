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
                _contact.Surname = SurnameTextBox.Text;
                _contact.Name = NameTextBox.Text;
                _contact.Birthday = BirthdayDateTimePicker.Value;
                _contact.Number = new PhoneNumber();
                _contact.Number.Number = long.Parse(PhoneTextBox.Text);
                _contact.Email = EmailTextBox.Text;
                _contact.IDVK = IDVKTextBox.Text;
                return _contact;
            }
            set
            {
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
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
