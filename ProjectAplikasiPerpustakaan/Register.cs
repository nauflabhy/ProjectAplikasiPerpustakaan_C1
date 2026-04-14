using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class Register : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        public Register()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void Register_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '●';
            txtConfirmPassword.PasswordChar = '●';
            btnRegister.Enabled = false; // Nonaktifkan dulu sampai input lengkap
        }

        private void EnableRegisterButton()
        {
            btnRegister.Enabled = !string.IsNullOrWhiteSpace(txtUsername.Text) &&
                                  !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                                  !string.IsNullOrWhiteSpace(txtConfirmPassword.Text) &&
                                  !string.IsNullOrWhiteSpace(txtNamaLengkap.Text) &&
                                  !string.IsNullOrWhiteSpace(txtNoHp.Text) &&
                                  !string.IsNullOrWhiteSpace(txtEmail.Text); // tambahkan field lain jika ada
        }

        private void txtUsername_TextChanged(object sender, EventArgs e) => EnableRegisterButton();
        private void txtPassword_TextChanged(object sender, EventArgs e) => EnableRegisterButton();
        private void txtConfirmPassword_TextChanged(object sender, EventArgs e) => EnableRegisterButton();
        private void txtNamaLengkap_TextChanged(object sender, EventArgs e) => EnableRegisterButton(); // jika ada field nama

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }


        // Opsional: Tombol Cancel / Kembali ke Login
        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoginMenu loginForm = new LoginMenu();
            loginForm.Show();
            this.Close();
        }


        private void txtUsername_TextChanged_1(object sender, EventArgs e)
        {
            EnableRegisterButton();
        }

        private void txtPassword_TextChanged_1(object sender, EventArgs e)
        {
            EnableRegisterButton();
        }

        private void txtConfirmPassword_TextChanged_1(object sender, EventArgs e)
        {
            EnableRegisterButton();
        }

        private void InitializeComponent()
        {
            this.txtNoHp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNamaLengkap = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNoHp
            // 
            this.txtNoHp.Location = new System.Drawing.Point(882, 481);
            this.txtNoHp.Name = "txtNoHp";
            this.txtNoHp.Size = new System.Drawing.Size(228, 26);
            this.txtNoHp.TabIndex = 35;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(718, 484);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 20);
            this.label8.TabIndex = 34;
            this.label8.Text = "No HP";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(882, 434);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(228, 26);
            this.txtEmail.TabIndex = 33;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(718, 437);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 20);
            this.label7.TabIndex = 32;
            this.label7.Text = "Email";
            // 
            // txtNamaLengkap
            // 
            this.txtNamaLengkap.Location = new System.Drawing.Point(882, 379);
            this.txtNamaLengkap.Name = "txtNamaLengkap";
            this.txtNamaLengkap.Size = new System.Drawing.Size(228, 26);
            this.txtNamaLengkap.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(718, 382);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 20);
            this.label6.TabIndex = 30;
            this.label6.Text = "Nama Lengkap";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(882, 590);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(228, 26);
            this.txtConfirmPassword.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(718, 590);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 20);
            this.label5.TabIndex = 28;
            this.label5.Text = "Confirm Passowrd";
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(893, 661);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(109, 51);
            this.btnRegister.TabIndex = 27;
            this.btnRegister.Text = "Buat Akun";
            this.btnRegister.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(882, 536);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(228, 26);
            this.txtPassword.TabIndex = 26;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(882, 323);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(228, 26);
            this.txtUsername.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(718, 536);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 24;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(718, 326);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(659, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(504, 32);
            this.label2.TabIndex = 22;
            this.label2.Text = "SISTEM PEMINJAMAN BUKU PERPUSTAKAAN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(876, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 32);
            this.label1.TabIndex = 21;
            this.label1.Text = "REGISTER";
            // 
            // Register
            // 
            this.ClientSize = new System.Drawing.Size(1823, 854);
            this.Controls.Add(this.txtNoHp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNamaLengkap);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Register";
            this.Load += new System.EventHandler(this.Register_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private TextBox txtNoHp;
        private Label label8;
        private TextBox txtEmail;
        private Label label7;
        private TextBox txtNamaLengkap;
        private Label label6;
        private TextBox txtConfirmPassword;
        private Label label5;
        private Button btnRegister;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;

        private void Register_Load_1(object sender, EventArgs e)
        {

        }
    }
}
