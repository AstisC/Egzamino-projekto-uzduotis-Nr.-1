using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class PasswordForm : Form
    {
        public PasswordEntry PasswordEntry { get; private set; }

        public PasswordForm()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            txtPassword.Text = GeneratePassword();
        }

        private string GeneratePassword()
        {
            int length = 14; // Pakeiskite pagal poreikį
            int numUpperCase = 3;
            int numLowerCase = 5;
            int numDigits = 3;
            int numSpecialChars = 3;
            return PasswordGenerator.GeneratePassword(length, numUpperCase, numLowerCase, numDigits, numSpecialChars);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PasswordEntry = new PasswordEntry
            {
                Name = txtName.Text,
                Site = txtSite.Text,
                Password = txtPassword.Text,
                CreatedAt = DateTime.Now
            };
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
