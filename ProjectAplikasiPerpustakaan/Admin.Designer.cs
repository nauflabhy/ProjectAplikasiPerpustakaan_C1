namespace ProjectAplikasiPerpustakaan
{
    partial class Admin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDaftarPengajuan = new System.Windows.Forms.Button();
            this.btnPengguna = new System.Windows.Forms.Button();
            this.btnEditBuku = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(839, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "ADMIN MENU";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(40, 43);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(184, 47);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 158);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1683, 513);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnDaftarPengajuan
            // 
            this.btnDaftarPengajuan.Location = new System.Drawing.Point(160, 725);
            this.btnDaftarPengajuan.Name = "btnDaftarPengajuan";
            this.btnDaftarPengajuan.Size = new System.Drawing.Size(183, 65);
            this.btnDaftarPengajuan.TabIndex = 3;
            this.btnDaftarPengajuan.Text = "Daftar Pengajuan";
            this.btnDaftarPengajuan.UseVisualStyleBackColor = true;
            this.btnDaftarPengajuan.Click += new System.EventHandler(this.btnDaftarPengajuan_Click);
            // 
            // btnPengguna
            // 
            this.btnPengguna.Location = new System.Drawing.Point(401, 725);
            this.btnPengguna.Name = "btnPengguna";
            this.btnPengguna.Size = new System.Drawing.Size(183, 65);
            this.btnPengguna.TabIndex = 4;
            this.btnPengguna.Text = "Daftar Pengguna";
            this.btnPengguna.UseVisualStyleBackColor = true;
            this.btnPengguna.Click += new System.EventHandler(this.btnPengguna_Click);
            // 
            // btnEditBuku
            // 
            this.btnEditBuku.Location = new System.Drawing.Point(651, 725);
            this.btnEditBuku.Name = "btnEditBuku";
            this.btnEditBuku.Size = new System.Drawing.Size(183, 65);
            this.btnEditBuku.TabIndex = 5;
            this.btnEditBuku.Text = "Edit Buku";
            this.btnEditBuku.UseVisualStyleBackColor = true;
            this.btnEditBuku.Click += new System.EventHandler(this.btnEditBuku_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(878, 725);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(147, 65);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1735, 830);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnEditBuku);
            this.Controls.Add(this.btnPengguna);
            this.Controls.Add(this.btnDaftarPengajuan);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.label1);
            this.Name = "Admin";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Admin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDaftarPengajuan;
        private System.Windows.Forms.Button btnPengguna;
        private System.Windows.Forms.Button btnEditBuku;
        private System.Windows.Forms.Button btnRefresh;
    }
}