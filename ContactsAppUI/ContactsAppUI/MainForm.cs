using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;

namespace ContactsApp
{
    public partial class MainForm : Form
    {
        //private static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Roaming\ContactsApp.notes";
        private static string path = @"d:\ContactsApp.notes";

        ProjectManager projectmanager = new ProjectManager();

        Project project = new Project();

        TextInfo FirstUppercaseLetter = CultureInfo.CurrentCulture.TextInfo;

        Dictionary<int, int> indecis = new Dictionary<int, int>();

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При загрузке MainForm в класс Project, словарь indecis и ListBox загружаются существующие контакты
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            project = projectmanager.LoadFromFile(path);
            if (project != null)
            {
                foreach (Contact contact in project.Contacts)
                {
                    indecis.Add(project.Contacts.IndexOf(contact), project.Contacts.IndexOf(contact));
                    ContactsListBox.Items.Add($"{contact.Surname} {contact.Name}");
                }
            }
            else
            {
                indecis = new Dictionary<int, int>();
                project = new Project();
                project.Contacts = new List<Contact>();
            }
        }

        /// <summary>
        /// Функция добавления нового контакта
        /// </summary>
        private void addContact()
        {
            var addContaсtForm = new ContactForm();
            addContaсtForm.ShowDialog();
            var newContact = addContaсtForm.Contact;
            if (newContact != null)
            {
                project.Contacts.Add(newContact);
                Rewriting();
                projectmanager.SaveToFile(project, path);
            }
        }

        /// <summary>
        /// Функция удаления контакта
        /// </summary>
        private void removeContact()
        {
            var selectedIndexListBox = ContactsListBox.SelectedIndex;
            if (selectedIndexListBox != -1)
            {
                foreach(var pair in indecis)
                {
                    if(selectedIndexListBox == pair.Value)
                    {
                        var selectedContact = project.Contacts[pair.Key];
                        DialogResult result = MessageBox.Show($"Do you really want to remove this contact: {selectedContact.Surname} {selectedContact.Name}?",
                            "Removing contact", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            project.Contacts.RemoveAt(pair.Key);
                            Rewriting();
                            projectmanager.SaveToFile(project, path);
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Функция редактирования контакта
        /// </summary>
        private void editContact()
        {
            var editContactForm = new ContactForm();
            var selectedIndexListBox = ContactsListBox.SelectedIndex;
            if (selectedIndexListBox != -1)
            {
                foreach(var pair in indecis)
                {
                    if(selectedIndexListBox == pair.Value)
                    {
                        var selectedContact = project.Contacts[pair.Key];
                        editContactForm.Contact = selectedContact;
                        editContactForm.ShowDialog();
                        var updatedContact = editContactForm.Contact;
                        if(updatedContact != null)
                        {
                            project.Contacts.RemoveAt(pair.Key);
                            project.Contacts.Insert(pair.Key, updatedContact);
                            Rewriting();
                            projectmanager.SaveToFile(project, path);
                        }
                        break;
                    }
                }
            }
        }
       
        private void AddContactButton_Click(object sender, EventArgs e)
        {
            addContact();
        }

        private void RemoveContactButton_Click(object sender, EventArgs e)
        {
            removeContact();
        }

        private void EditContactButton_Click(object sender, EventArgs e)
        {
            editContact();
        }

        private void addContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addContact();
        }

        private void removeContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeContact();
        }

        private void editContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editContact();
        }

        /// <summary>
        /// Метод, который при выделении элемента ListBox выводит всю информацию о контакте в TextBox'ы
        /// </summary>
        private void ContactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIndexListBox = ContactsListBox.SelectedIndex;
            if (selectedIndexListBox != -1)
            {
                foreach (var pair in indecis)
                {
                    if(selectedIndexListBox == pair.Value)
                    {
                        var selectedContact = project.Contacts[pair.Key];
                        SurnameTextBox.Text = selectedContact.Surname;
                        NameTextBox.Text = selectedContact.Name;
                        BirthdayTextBox.Text = selectedContact.Birthday.ToShortDateString();
                        if (selectedContact.Number.Number != 0)
                            PhoneTextBox.Text = selectedContact.Number.Number.ToString();
                        else
                            PhoneTextBox.Text = "";
                        EmailTextBox.Text = selectedContact.Email;
                        IDVKTextBox.Text = selectedContact.IDVK;
                        break;
                    }
                }
            }
            else
            {
                SurnameTextBox.Clear();
                NameTextBox.Clear();
                BirthdayTextBox.Clear();
                PhoneTextBox.Clear();
                EmailTextBox.Clear();
                IDVKTextBox.Clear();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            projectmanager.SaveToFile(project, path);
        }

        /// <summary>
        /// Метод, реализующий поиск контакта
        /// </summary>
        private void FindTextBox_TextChanged(object sender, EventArgs e)
        {
            Rewriting();
        }

        private void Rewriting()
        {
            ContactsListBox.Items.Clear();
            indecis.Clear();
            string findname = FirstUppercaseLetter.ToTitleCase(FindTextBox.Text);
            foreach (Contact contact in project.Contacts)
            {
                string fullname = $"{contact.Surname} {contact.Name}";
                if (fullname.Contains(findname))
                {
                    ContactsListBox.Items.Add(fullname);
                    indecis.Add(project.Contacts.IndexOf(contact), ContactsListBox.Items.Count - 1);
                }
            }
        }
    }
}
