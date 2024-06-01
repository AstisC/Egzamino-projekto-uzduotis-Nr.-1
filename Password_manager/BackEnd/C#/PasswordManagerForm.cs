using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class PasswordManagerForm : Form
    {
        private readonly List<PasswordEntry> _passwordEntries;

        public PasswordManagerForm()
        {
            InitializeComponent();
            _passwordEntries = new List<PasswordEntry>();
        }

        private void PasswordManagerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Site", "Site");
            dataGridView1.Columns.Add("Password", "Password");
            dataGridView1.Columns["Password"].DefaultCellStyle.Format = "Password";
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.Value != null)
            {
                e.Value = new string('•', e.Value.ToString().Length);
                e.FormattingApplied = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var passwordForm = new PasswordForm();
            if (passwordForm.ShowDialog() == DialogResult.OK)
            {
                _passwordEntries.Add(passwordForm.PasswordEntry);
                dataGridView1.Rows.Add(passwordForm.PasswordEntry.Name, passwordForm.PasswordEntry.Site, passwordForm.PasswordEntry.Password);
            }
        }

        private void chkShowPasswords_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var cell = row.Cells["Password"];
                if (chkShowPasswords.Checked)
                {
                    cell.Value = _passwordEntries[row.Index].Password;
                }
                else
                {
                    cell.Value = new string('•', _passwordEntries[row.Index].Password.Length);
                }
            }
        }
    }

    public class PasswordEntry
    {
        public string Name { get; set; }
        public string Site { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
