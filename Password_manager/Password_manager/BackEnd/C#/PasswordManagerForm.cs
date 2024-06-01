using System;
using System.Collections.Generic;
using System.Drawing;
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
            AdjustLayout();
            ApplyStyles();
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                var password = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Clipboard.SetText(password);
                MessageBox.Show("Slaptažodis nukopijuotas į iškarpinę.");
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

        private void AdjustLayout()
        {
            // Aukštis ir plotis
            int btnHeight = 30;
            int btnWidth = 100;

            // Mygtukų ir kontrolinių elementų dydžiai ir pozicijos
            btnAdd.Height = btnHeight;
            btnAdd.Width = btnWidth;
            btnAdd.Location = new Point(10, this.ClientSize.Height - btnHeight - 10);

            // Pasirinkimo žymės dėžutė, jei naudojama rodyti slaptažodžius
            chkShowPasswords.Height = 20;
            chkShowPasswords.Width = 150;
            chkShowPasswords.Location = new Point(btnAdd.Right + 10, this.ClientSize.Height - chkShowPasswords.Height - 10);

            // DataGridView pritaikymas
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void ApplyStyles()
        {
            // Formos fono spalva
            this.BackColor = Color.LightGray;

            // Mygtukų stilius
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.BackColor = Color.SteelBlue;
            btnAdd.ForeColor = Color.White;

            // CheckBox stilius
            chkShowPasswords.ForeColor = Color.DarkBlue;

            // DataGridView stilius
            dataGridView1.BackgroundColor = Color.WhiteSmoke;
            dataGridView1.DefaultCellStyle.BackColor = Color.Beige;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
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
