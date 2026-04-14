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
            this.btnBukuDipinjam = new System.Windows.Forms.Button();
            this.btnCari = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCariBuku = new System.Windows.Forms.TextBox();
            this.btnKembalikan = new System.Windows.Forms.Button();
            this.btnPinjam = new System.Windows.Forms.Button();
            this.btnDetailBuku = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBukuDipinjam
            // 
            this.btnBukuDipinjam.Location = new System.Drawing.Point(811, 711);
            this.btnBukuDipinjam.Name = "btnBukuDipinjam";
            this.btnBukuDipinjam.Size = new System.Drawing.Size(149, 59);
            this.btnBukuDipinjam.TabIndex = 17;
            this.btnBukuDipinjam.Text = "Buku yang dipinjam";
            this.btnBukuDipinjam.UseVisualStyleBackColor = true;
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(692, 49);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(90, 32);
            this.btnCari.TabIndex = 16;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Cari Buku";
            // 
            // txtCariBuku
            // 
            this.txtCariBuku.Location = new System.Drawing.Point(155, 49);
            this.txtCariBuku.Name = "txtCariBuku";
            this.txtCariBuku.Size = new System.Drawing.Size(514, 26);
            this.txtCariBuku.TabIndex = 14;
            // 
            // btnKembalikan
            // 
            this.btnKembalikan.Location = new System.Drawing.Point(633, 711);
            this.btnKembalikan.Name = "btnKembalikan";
            this.btnKembalikan.Size = new System.Drawing.Size(149, 59);
            this.btnKembalikan.TabIndex = 13;
            this.btnKembalikan.Text = "Kembalikan Buku";
            this.btnKembalikan.UseVisualStyleBackColor = true;
            // 
            // btnPinjam
            // 
            this.btnPinjam.Location = new System.Drawing.Point(444, 711);
            this.btnPinjam.Name = "btnPinjam";
            this.btnPinjam.Size = new System.Drawing.Size(149, 59);
            this.btnPinjam.TabIndex = 12;
            this.btnPinjam.Text = "Pinjam Buku";
            this.btnPinjam.UseVisualStyleBackColor = true;
            // 
            // btnDetailBuku
            // 
            this.btnDetailBuku.Location = new System.Drawing.Point(256, 707);
            this.btnDetailBuku.Name = "btnDetailBuku";
            this.btnDetailBuku.Size = new System.Drawing.Size(149, 68);
            this.btnDetailBuku.TabIndex = 11;
            this.btnDetailBuku.Text = "Detail Buku";
            this.btnDetailBuku.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(71, 707);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(149, 67);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Load Koneksi";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(38, 140);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1747, 508);
            this.dataGridView1.TabIndex = 9;
            // 
            // CariBuku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1823, 824);
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
            this.Text = "CariBuku";
            this.Load += new System.EventHandler(this.CariBuku_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBukuDipinjam;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCariBuku;
        private System.Windows.Forms.Button btnKembalikan;
        private System.Windows.Forms.Button btnPinjam;
        private System.Windows.Forms.Button btnDetailBuku;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}