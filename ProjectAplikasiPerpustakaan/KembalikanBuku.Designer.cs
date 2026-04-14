namespace ProjectAplikasiPerpustakaan
{
    partial class KembalikanBuku
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
            this.btnKembalikanBuku = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnKembalikanBuku
            // 
            this.btnKembalikanBuku.Location = new System.Drawing.Point(1522, 721);
            this.btnKembalikanBuku.Name = "btnKembalikanBuku";
            this.btnKembalikanBuku.Size = new System.Drawing.Size(164, 63);
            this.btnKembalikanBuku.TabIndex = 5;
            this.btnKembalikanBuku.Text = "Kembalikan Buku";
            this.btnKembalikanBuku.UseVisualStyleBackColor = true;
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(1305, 721);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(164, 63);
            this.btnKembali.TabIndex = 4;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1767, 609);
            this.dataGridView1.TabIndex = 3;
            // 
            // KembalikanBuku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1779, 832);
            this.Controls.Add(this.btnKembalikanBuku);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.dataGridView1);
            this.Name = "KembalikanBuku";
            this.Text = "KembalikanBuku";
            this.Load += new System.EventHandler(this.KembalikanBuku_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKembalikanBuku;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}