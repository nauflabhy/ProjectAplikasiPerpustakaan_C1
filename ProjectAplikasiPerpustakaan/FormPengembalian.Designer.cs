namespace ProjectAplikasiPerpustakaan
{
    partial class FormPengembalian
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbKondisiBuku = new System.Windows.Forms.ComboBox();
            this.lblTanggalAjuan = new System.Windows.Forms.Label();
            this.lblJudulBuku = new System.Windows.Forms.Label();
            this.lblKodeBuku = new System.Windows.Forms.Label();
            this.btnBatal = new System.Windows.Forms.Button();
            this.btnKembalikan = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCatatan = new System.Windows.Forms.TextBox();
            this.lblTanggalPengembalian = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(779, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Pengembalian";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(755, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kode Buku";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(755, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Judul Buku:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(755, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tanggal Ajuan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(755, 316);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tanggal Pengembalian";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(755, 367);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Kondisi Buku:";
            // 
            // cmbKondisiBuku
            // 
            this.cmbKondisiBuku.FormattingEnabled = true;
            this.cmbKondisiBuku.Items.AddRange(new object[] {
            "baik",
            "rusak ringan",
            "rusak berat",
            "hilang"});
            this.cmbKondisiBuku.Location = new System.Drawing.Point(981, 367);
            this.cmbKondisiBuku.Name = "cmbKondisiBuku";
            this.cmbKondisiBuku.Size = new System.Drawing.Size(121, 28);
            this.cmbKondisiBuku.TabIndex = 6;
            this.cmbKondisiBuku.SelectedIndexChanged += new System.EventHandler(this.cmbKondisiBuku_SelectedIndexChanged);
            // 
            // lblTanggalAjuan
            // 
            this.lblTanggalAjuan.AutoSize = true;
            this.lblTanggalAjuan.Location = new System.Drawing.Point(977, 261);
            this.lblTanggalAjuan.Name = "lblTanggalAjuan";
            this.lblTanggalAjuan.Size = new System.Drawing.Size(111, 20);
            this.lblTanggalAjuan.TabIndex = 9;
            this.lblTanggalAjuan.Text = "Tanggal Ajuan";
            // 
            // lblJudulBuku
            // 
            this.lblJudulBuku.AutoSize = true;
            this.lblJudulBuku.Location = new System.Drawing.Point(977, 209);
            this.lblJudulBuku.Name = "lblJudulBuku";
            this.lblJudulBuku.Size = new System.Drawing.Size(92, 20);
            this.lblJudulBuku.TabIndex = 8;
            this.lblJudulBuku.Text = "Judul Buku:";
            // 
            // lblKodeBuku
            // 
            this.lblKodeBuku.AutoSize = true;
            this.lblKodeBuku.Location = new System.Drawing.Point(977, 160);
            this.lblKodeBuku.Name = "lblKodeBuku";
            this.lblKodeBuku.Size = new System.Drawing.Size(87, 20);
            this.lblKodeBuku.TabIndex = 7;
            this.lblKodeBuku.Text = "Kode Buku";
            // 
            // btnBatal
            // 
            this.btnBatal.Location = new System.Drawing.Point(759, 566);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(148, 55);
            this.btnBatal.TabIndex = 11;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = true;
            this.btnBatal.Click += new System.EventHandler(this.btnBatal_Click_1);
            // 
            // btnKembalikan
            // 
            this.btnKembalikan.Location = new System.Drawing.Point(1000, 566);
            this.btnKembalikan.Name = "btnKembalikan";
            this.btnKembalikan.Size = new System.Drawing.Size(148, 55);
            this.btnKembalikan.TabIndex = 12;
            this.btnKembalikan.Text = "Kembalikan";
            this.btnKembalikan.UseVisualStyleBackColor = true;
            this.btnKembalikan.Click += new System.EventHandler(this.btnKembalikan_Click_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(755, 426);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Catatan:";
            // 
            // txtCatatan
            // 
            this.txtCatatan.Location = new System.Drawing.Point(981, 426);
            this.txtCatatan.Multiline = true;
            this.txtCatatan.Name = "txtCatatan";
            this.txtCatatan.Size = new System.Drawing.Size(277, 87);
            this.txtCatatan.TabIndex = 14;
            this.txtCatatan.TextChanged += new System.EventHandler(this.txtCatatan_TextChanged);
            // 
            // lblTanggalPengembalian
            // 
            this.lblTanggalPengembalian.AutoSize = true;
            this.lblTanggalPengembalian.Location = new System.Drawing.Point(977, 316);
            this.lblTanggalPengembalian.Name = "lblTanggalPengembalian";
            this.lblTanggalPengembalian.Size = new System.Drawing.Size(171, 20);
            this.lblTanggalPengembalian.TabIndex = 15;
            this.lblTanggalPengembalian.Text = "Tanggal Pengembalian";
            // 
            // FormPengembalian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 828);
            this.Controls.Add(this.lblTanggalPengembalian);
            this.Controls.Add(this.txtCatatan);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnKembalikan);
            this.Controls.Add(this.btnBatal);
            this.Controls.Add(this.lblTanggalAjuan);
            this.Controls.Add(this.lblJudulBuku);
            this.Controls.Add(this.lblKodeBuku);
            this.Controls.Add(this.cmbKondisiBuku);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormPengembalian";
            this.Text = "FormPengembalian";
            this.Load += new System.EventHandler(this.FormPengembalian_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbKondisiBuku;
        private System.Windows.Forms.Label lblTanggalAjuan;
        private System.Windows.Forms.Label lblJudulBuku;
        private System.Windows.Forms.Label lblKodeBuku;
        private System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Button btnKembalikan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCatatan;
        private System.Windows.Forms.Label lblTanggalPengembalian;
    }
}