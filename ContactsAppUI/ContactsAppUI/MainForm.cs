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
    public partial class MainForm : Form
    {
        ProjectManager projectmanager = new ProjectManager();
        Project project = new Project();
        public MainForm()
        {
            InitializeComponent();
            project = projectmanager.LoadFromFile();
            if (project != null)
            {
                foreach (Contact contact in project.Contacts)
                    ContactsListBox.Items.Add(contact.Surname + " " + contact.Name);
            }
            else
            {
                project = new Project();
                project.Contacts = new List<Contact>();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddContactButton_Click(object sender, EventArgs e)
        {
            var addContant = new AddEditForm();
            addContant.ShowDialog();
            var newContact = addContant.Contact;
            project.Contacts.Add(newContact);
            projectmanager.SaveToFile(project);
            ContactsListBox.Items.Add(newContact.Surname + " " + newContact.Name);
        }

        private void ContactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contact showContact = new Contact();
        }
    }
}
