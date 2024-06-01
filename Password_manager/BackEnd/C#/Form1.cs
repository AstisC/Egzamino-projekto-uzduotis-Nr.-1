using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly ApiService _apiService;

        public Form1()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;
            var result = await _apiService.RegisterUserAsync(username, password);
            if (result)
            {
                MessageBox.Show("Registration successful!");
                var passwordManagerForm = new PasswordManagerForm();
                passwordManagerForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Registration failed.");
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;
            var result = await _apiService.LoginUserAsync(username, password);
            if (result)
            {
                MessageBox.Show("Login successful!");
                var passwordManagerForm = new PasswordManagerForm();
                passwordManagerForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login failed.");
            }
        }
    }
}
