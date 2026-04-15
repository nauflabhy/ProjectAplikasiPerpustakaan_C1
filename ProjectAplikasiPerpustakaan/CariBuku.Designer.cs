namespace ProjectAplikasiPerpustakaan
{
    partial class CariBuku
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDetailBuku = new System.Windows.Forms.Button();
            this.btnPinjam = new System.Windows.Forms.Button();
            this.btnKembalikan = new System.Windows.Forms.Button();
            this.txtCariBuku = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCari = new System.Windows.Forms.Button();
            this.btnBukuDipinjam = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(29, 156);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1747, 508);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(62, 723);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(149, 67);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Load Koneksi";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDetailBuku
            // 
            this.btnDetailBuku.Location = new System.Drawing.Point(247, 723);
            this.btnDetailBuku.Name = "btnDetailBuku";
            this.btnDetailBuku.Size = new System.Drawing.Size(149, 68);
            this.btnDetailBuku.TabIndex = 2;
            this.btnDetailBuku.Text = "Detail Buku";
            this.btnDetailBuku.UseVisualStyleBackColor = true;
            this.btnDetailBuku.Click += new System.EventHandler(this.btnDetailBuku_Click);
            // 
            // btnPinjam
            // 
            this.btnPinjam.Location = new System.Drawing.Point(435, 727);
            this.btnPinjam.Name = "btnPinjam";
            this.btnPinjam.Size = new System.Drawing.Size(149, 59);
            this.btnPinjam.TabIndex = 3;
            this.btnPinjam.Text = "Pinjam Buku";
            this.btnPinjam.UseVisualStyleBackColor = true;
            this.btnPinjam.Click += new System.EventHandler(this.btnPinjam_Click);
            // 
            // btnKembalikan
            // 
            this.btnKembalikan.Location = new System.Drawing.Point(624, 727);
            this.btnKembalikan.Name = "btnKembalikan";
            this.btnKembalikan.Size = new System.Drawing.Size(149, 59);
            this.btnKembalikan.TabIndex = 4;
            this.btnKembalikan.Text = "Kembalikan Buku";
            this.btnKembalikan.UseVisualStyleBackColor = true;
            this.btnKembalikan.Click += new System.EventHandler(this.btnKembalikan_Click);
            // 
            // txtCariBuku
            // 
            this.txtCariBuku.Location = new System.Drawing.Point(535, 55);
            this.txtCariBuku.Name = "txtCariBuku";
            this.txtCariBuku.Size = new System.Drawing.Size(514, 26);
            this.txtCariBuku.TabIndex = 5;
            this.txtCariBuku.TextChanged += new System.EventHandler(this.txtCariBuku_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(447, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cari Buku";
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(1072, 55);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(90, 32);
            this.btnCari.TabIndex = 7;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // btnBukuDipinjam
            // 
            this.btnBukuDipinjam.Location = new System.Drawing.Point(802, 727);
            this.btnBukuDipinjam.Name = "btnBukuDipinjam";
            this.btnBukuDipinjam.Size = new System.Drawing.Size(149, 59);
            this.btnBukuDipinjam.TabIndex = 8;
            this.btnBukuDipinjam.Text = "Buku yang dipinjam";
            this.btnBukuDipinjam.UseVisualStyleBackColor = true;
            this.btnBukuDipinjam.Click += new System.EventHandler(this.btnBukuDipinjam_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(90, 41);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(121, 46);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Log Out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // CariBuku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1788, 832);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnBukuDipinjam);
            this.Controls.Add(this.btnCari);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCariBuku);
            this.Controls.Add(this.btnKembalikan);
            this.Controls.Add(this.btnPinjam);
            this.Controls.Add(this.btnDetailBuku);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CariBuku";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.CariBuku_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDetailBuku;
        private System.Windows.Forms.Button btnPinjam;
        private System.Windows.Forms.Button btnKembalikan;
        private System.Windows.Forms.TextBox txtCariBuku;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Button btnBukuDipinjam;
        private System.Windows.Forms.Button btnLogout;
    }
}