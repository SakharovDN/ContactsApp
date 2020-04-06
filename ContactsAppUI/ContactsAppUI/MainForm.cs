using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;

namespace ContactsApp
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// В переменной path хранится путь до файла, где хранится список существующих контактов
        /// </summary>
        private readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ContactsApp.notes";

        private ProjectManager _projectManager = new ProjectManager();

        private Project _project = new Project();

        private readonly TextInfo _firstUppercaseLetter = CultureInfo.CurrentCulture.TextInfo;

        /// <summary>
        /// В словаре indecis хранятся индексы, необходимые для реализации создания, редактирования и удаления контакта во время использования поиска
        /// </summary>
        private Dictionary<int, int> indecis = new Dictionary<int, int>();

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При загрузке MainForm в класс Project, словарь indecis и ListBox загружаются существующие контакты
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            _project = _projectManager.LoadFromFile(path);
            if (_project != null)
            {
                foreach (Contact contact in _project.Contacts)
                {
                    indecis.Add(_project.Contacts.IndexOf(contact), _project.Contacts.IndexOf(contact));
                    ContactsListBox.Items.Add($"{contact.Surname} {contact.Name}");
                }
            }
            else
            {
                indecis = new Dictionary<int, int>();
                _project = new Project();
                _project.Contacts = new List<Contact>();
            }
        }

        /// <summary>
        /// Функция добавления нового контакта
        /// </summary>
        private void AddContact()
        {
            var addContaсtForm = new ContactForm();
            addContaсtForm.ShowDialog();
            var newContact = addContaсtForm.Contact;
            if (newContact != null)
            {
                _project.Contacts.Add(newContact);
                Rewriting();
                _projectManager.SaveToFile(_project, path);
            }
        }

        /// <summary>
        /// Функция удаления контакта
        /// </summary>
        private void RemoveContact()
        {
            var selectedIndexListBox = ContactsListBox.SelectedIndex;
            if (selectedIndexListBox != -1)
            {
                foreach (var pair in indecis)
                {
                    if (selectedIndexListBox == pair.Value)
                    {
                        var selectedContact = _project.Contacts[pair.Key];
                        DialogResult result = MessageBox.Show($"Do you really want to remove this contact: {selectedContact.Surname} {selectedContact.Name}?",
                            "Removing contact", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            _project.Contacts.RemoveAt(pair.Key);
                            ContactsListBox.SetSelected(selectedIndexListBox, false);
                            Rewriting();
                            _projectManager.SaveToFile(_project, path);
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Функция редактирования контакта
        /// </summary>
        private void EditContact()
        {
            var editContactForm = new ContactForm();
            var selectedIndexListBox = ContactsListBox.SelectedIndex;
            if (selectedIndexListBox != -1)
            {
                foreach (var pair in indecis)
                {
                    if (selectedIndexListBox == pair.Value)
                    {
                        var selectedContact = _project.Contacts[pair.Key];
                        editContactForm.Contact = selectedContact;
                        editContactForm.ShowDialog();
                        var updatedContact = editContactForm.Contact;
                        if (updatedContact != null)
                        {
                            _project.Contacts.RemoveAt(pair.Key);
                            _project.Contacts.Insert(pair.Key, updatedContact);
                            Rewriting();
                            ContactsListBox.SetSelected(selectedIndexListBox, true);
                            _projectManager.SaveToFile(_project, path);
                        }
                        break;
                    }
                }
            }
        }
       
        private void AddContactButton_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        private void RemoveContactButton_Click(object sender, EventArgs e)
        {
            RemoveContact();
        }

        private void EditContactButton_Click(object sender, EventArgs e)
        {
            EditContact();
        }

        private void addContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        private void removeContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveContact();
        }

        private void editContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditContact();
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
                        var selectedContact = _project.Contacts[pair.Key];
                        SurnameTextBox.Text = selectedContact.Surname;
                        NameTextBox.Text = selectedContact.Name;
                        BirthdayTextBox.Text = selectedContact.Birthday.ToShortDateString();
                        PhoneTextBox.Text = selectedContact.Number.Number.ToString();
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
            _projectManager.SaveToFile(_project, path);
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
            string findname = _firstUppercaseLetter.ToTitleCase(FindTextBox.Text);
            foreach (Contact contact in _project.Contacts)
            {
                string fullname = $"{contact.Surname} {contact.Name}";
                if (fullname.Contains(findname))
                {
                    ContactsListBox.Items.Add(fullname);
                    indecis.Add(_project.Contacts.IndexOf(contact), ContactsListBox.Items.Count - 1);
                }
            }
        }
    }
}
