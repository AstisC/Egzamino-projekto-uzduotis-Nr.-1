namespace WinFormsApp
{
    partial class PasswordManagerForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkShowPasswords;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkShowPasswords = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(560, 387);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(497, 405);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkShowPasswords
            // 
            this.chkShowPasswords.AutoSize = true;
            this.chkShowPasswords.Location = new System.Drawing.Point(12, 409);
            this.chkShowPasswords.Name = "chkShowPasswords";
            this.chkShowPasswords.Size = new System.Drawing.Size(107, 17);
            this.chkShowPasswords.TabIndex = 2;
            this.chkShowPasswords.Text = "Show Passwords";
            this.chkShowPasswords.UseVisualStyleBackColor = true;
            this.chkShowPasswords.CheckedChanged += new System.EventHandler(this.chkShowPasswords_CheckedChanged);
            // 
            // PasswordManagerForm
            // 
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.chkShowPasswords);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PasswordManagerForm";
            this.Text = "Password Manager";
            this.Load += new System.EventHandler(this.PasswordManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
