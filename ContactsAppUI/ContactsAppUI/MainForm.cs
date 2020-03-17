using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace ContactsApp
{
    public partial class MainForm : Form
    {
        ProjectManager projectmanager = new ProjectManager();
        Project project = new Project();
        TextInfo FirstUppercaseLetter = CultureInfo.CurrentCulture.TextInfo;
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// При загрузке MainForm в класс Project и ListBox загружаются существующие контакты
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
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
        /// <summary>
        /// Функция добавления нового контакта
        /// </summary>
        private void addContact()
        {
            string findname = FirstUppercaseLetter.ToTitleCase(FindTextBox.Text);
            var addContantForm = new AddEditForm();
            addContantForm.ShowDialog();
            var newContact = addContantForm.Contact;
            if (newContact != null)
            {
                string fullname = newContact.Surname + " " + newContact.Name;
                project.Contacts.Add(newContact);
                projectmanager.SaveToFile(project);
                if (fullname.Contains(findname))
                    ContactsListBox.Items.Add(fullname);
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
                string[] fullname = ContactsListBox.SelectedItem.ToString().Split();
                foreach (Contact contact in project.Contacts)
                {
                    if (fullname[0] == contact.Surname && fullname[1] == contact.Name)
                    {
                        DialogResult result = MessageBox.Show("Do you really want to remove this contact: " + contact.Surname + " " + contact.Name + "?", "Removing contact", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            project.Contacts.Remove(contact);
                            projectmanager.SaveToFile(project);
                            ContactsListBox.Items.RemoveAt(selectedIndexListBox);
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
            var editContantForm = new AddEditForm();
            var selectedIndexListBox = ContactsListBox.SelectedIndex;
            if (selectedIndexListBox != -1)
            {
                string[] fullname = ContactsListBox.SelectedItem.ToString().Split();
                foreach (Contact contact in project.Contacts)
                {
                    if (fullname[0] == contact.Surname && fullname[1] == contact.Name)
                    {
                        var selectedIndexProject = project.Contacts.IndexOf(contact);
                        editContantForm.Contact = contact;
                        editContantForm.ShowDialog();
                        var updatedContact = editContantForm.Contact;
                        if (updatedContact != null)
                        {
                            project.Contacts.Remove(contact);
                            project.Contacts.Insert(selectedIndexProject, updatedContact);
                            projectmanager.SaveToFile(project);
                            ContactsListBox.Items.RemoveAt(selectedIndexListBox);
                            ContactsListBox.Items.Insert(selectedIndexListBox, updatedContact.Surname + " " + updatedContact.Name);
                            ContactsListBox.SetSelected(selectedIndexListBox, true);
                            break;
                        }
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
            // Изначально для нахождения нужного контакта использовался только selectedIndex, но тогда Поиск контакта работает некорректно
            var selectedIndexListBox = ContactsListBox.SelectedIndex;
            if (selectedIndexListBox != -1)
            {
                string[] fullname = ContactsListBox.SelectedItem.ToString().Split();
                foreach (Contact contact in project.Contacts)
                {
                    if (fullname[0] == contact.Surname && fullname[1] == contact.Name)
                    {
                        SurnameTextBox.Text = contact.Surname;
                        NameTextBox.Text = contact.Name;
                        BirthdayTextBox.Text = contact.Birthday.ToShortDateString();
                        if (contact.Number.Number != 0)
                            PhoneTextBox.Text = contact.Number.Number.ToString();
                        else
                            PhoneTextBox.Text = "";
                        EmailTextBox.Text = contact.Email;
                        IDVKTextBox.Text = contact.IDVK;
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
            projectmanager.SaveToFile(project);
        }

        /// <summary>
        /// Метод, реализующий поиск контакта
        /// </summary>
        private void FindTextBox_TextChanged(object sender, EventArgs e)
        {
            ContactsListBox.Items.Clear();
            string findname = FindTextBox.Text;
            TextInfo FirstUppercaseLetter = CultureInfo.CurrentCulture.TextInfo;
            findname = FirstUppercaseLetter.ToTitleCase(findname);
            foreach(Contact contact in project.Contacts)
            {
                string fullname = contact.Surname + " " + contact.Name;
                if (fullname.Contains(findname))
                {
                    ContactsListBox.Items.Add(fullname);
                }
            }
        }
    }
}
