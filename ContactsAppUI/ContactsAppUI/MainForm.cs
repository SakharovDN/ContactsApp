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

        /// <summary>
        /// Объект класса ProjectManager, с помощью которого контакты будут загружаться из файла и сохраняться в файл
        /// </summary>
        private ProjectManager _projectManager = new ProjectManager();

        /// <summary>
        /// Объект класса Project, в котором хранится список всех существующих контактов
        /// </summary>
        private Project _project = new Project();

        /// <summary>
        /// Класс, который нужен для преобразования строк: первая буква в верхний регистр, остальные в нижний
        /// </summary>
        private readonly TextInfo _firstUppercaseLetter = CultureInfo.CurrentCulture.TextInfo;

        /// <summary>
        /// В словаре indecis хранятся индексы, необходимые для реализации создания, 
        /// редактирования и удаления контакта во время использования поиска
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
                _project.Contacts = _project.SortedContacts();
                Rewrite();
                BirthdayContacts();
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
                _project.Contacts = _project.SortedContacts();
                Rewrite();
                BirthdayContacts();
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
                            _project.Contacts = _project.SortedContacts();
                            ContactsListBox.SetSelected(selectedIndexListBox, false);
                            Rewrite();
                            BirthdayContacts();
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
            //TODO: лучше инвертировать условие, чтобы вложенность была меньше, а код читаемее
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
                            _project.Contacts.Add(updatedContact);
                            _project.Contacts = _project.SortedContacts();
                            Rewrite();
                            BirthdayContacts();
                            _projectManager.SaveToFile(_project, path);
                        }
                        break;
                    }
                }
            }
            //TODO: после редактирования контакта на правой панели остаются данные не отредактированного контакта
        }

        /// <summary>
        /// Нажатие кнопки "Добавить контакт"
        /// </summary>
        private void AddContactButton_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        /// <summary>
        /// Нажатие кнопки "Удалить контакт"
        /// </summary>
        private void RemoveContactButton_Click(object sender, EventArgs e)
        {
            RemoveContact();
        }

        /// <summary>
        /// Нажатие кнопки "Редактировать контакт"
        /// </summary>
        private void EditContactButton_Click(object sender, EventArgs e)
        {
            EditContact();
        }

        /// <summary>
        /// Нажатие кнопки "Добавить контакт" в menuStrip
        /// </summary>
        private void addContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        /// <summary>
        /// Нажатие кнопки "Удалить контакт" в menuStrip
        /// </summary>
        private void removeContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveContact();
        }

        /// <summary>
        /// Нажатие кнопки "Редактировать контакт" в menuStrip
        /// </summary>
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

        /// <summary>
        /// Нажатие кнопки "Выход" в menuStrip
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Нажатие кнопки "О программе" в menuStrip. Вызывает форму AboutForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        /// <summary>
        /// Перед закрытием формы все контакты сохраняются в файл
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _projectManager.SaveToFile(_project, path);
        }

        /// <summary>
        /// Метод, реализующий поиск контакта
        /// </summary>
        private void FindTextBox_TextChanged(object sender, EventArgs e)
        {
            Rewrite();
        }

        /// <summary>
        /// Метод, который полностью перепиывает ListBox и словарь indecis
        /// </summary>
        private void Rewrite()
        {
            ContactsListBox.Items.Clear();
            indecis.Clear();
            string findname = _firstUppercaseLetter.ToTitleCase(FindTextBox.Text);
            for (int i = 0; i < _project.Contacts.Count; i++)
            {
                var contact = _project.Contacts[i];
                string fullname = $"{contact.Surname} {contact.Name}";
                if (fullname.Contains(findname))
                {
                    //TODO: должен ли работать поиск по подстроке (середине фамилии, имени)? Если да, то у меня не работает. Если нет, то для цифр в имени работает
                    ContactsListBox.Items.Add(fullname);
                    indecis.Add(i, ContactsListBox.Items.Count - 1);
                }
            }
        }

        /// <summary>
        /// Метод, который выводит список контактов, у которых сегодня день рождения
        /// </summary>
        private void BirthdayContacts()
        {
            var birthdayContacts = _project.BirthdayContacts(DateTime.Today);
            string birthday = "";
            //TODO: вместо отдельных строк присвоения true и false можно сразу здесь присвоить результат сравнения с Count
            if (birthdayContacts.Count == 0)
                panel1.Visible = false;
            else
            {
                //TODO: вместо for попробуй использовать Select для создания строк с фамилией-именем именниника,
                // а с помощью string.Join объединить их в одну строку. Тогда вместо цикла будет две простых читаемых строки
                for (int i = 0; i < birthdayContacts.Count; i++)
                {
                    if (i == 0)
                        birthday = $"{birthdayContacts[i].Surname} {birthdayContacts[i].Name}";
                    else
                        birthday = $"{birthday}, {birthdayContacts[i].Surname} {birthdayContacts[i].Name}";
                }
                BirthdayInfoContactsLabel.Text = birthday;
                panel1.Visible = true;
            }
        }

        /// <summary>
        /// Метод, позволяющий удалять контакт с помощью клавиши Delete
        /// </summary>
        private void ContactsListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveContact();
            }
        }
    }
}
