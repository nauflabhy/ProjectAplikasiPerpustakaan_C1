using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class TambahBuku : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        public TambahBuku()
        {
            InitializeComponent();
        }

        private void TambahBuku_Load(object sender, EventArgs e)
        {
            // Setup pembatasan input
            SetupInputRestrictions();

            // Optional: Set fokus ke field pertama
            txtKodeBuku.Focus();
        }

        private void SetupInputRestrictions()
        {
            // Kode Buku → Huruf + Angka + "-" (contoh: B001, BK-2025)
            txtKodeBuku.KeyPress += (s, ev) =>
            {
                if (!char.IsLetterOrDigit(ev.KeyChar) && ev.KeyChar != '-' && ev.KeyChar != '\b')
                    ev.Handled = true;
            };
            txtKodeBuku.MaxLength = 20;

            // Judul, Pengarang, Penerbit, Kategori, Lokasi → Huruf, Angka, Spasi
            txtJudul.KeyPress += AllowAlphanumericWithSpace;
            txtPengarang.KeyPress += AllowAlphanumericWithSpace;
            txtPenerbit.KeyPress += AllowAlphanumericWithSpace;
            txtKategori.KeyPress += AllowAlphanumericWithSpace;
            txtLokasi.KeyPress += AllowAlphanumericWithSpace;

            txtJudul.MaxLength = 200;
            txtPengarang.MaxLength = 100;
            txtPenerbit.MaxLength = 100;
            txtKategori.MaxLength = 50;
            txtLokasi.MaxLength = 50;

            // Tahun Terbit → Hanya Angka
            txtTahunTerbit.KeyPress += AllowOnlyNumbers;
            txtTahunTerbit.MaxLength = 4;

            // Stok Total & Stok Tersedia → Hanya Angka
            txtStokTotal.KeyPress += AllowOnlyNumbers;
            txtStokTersedia.KeyPress += AllowOnlyNumbers;
            txtStokTotal.MaxLength = 5;
            txtStokTersedia.MaxLength = 5;
        }

        private void AllowAlphanumericWithSpace(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void AllowOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtKodeBuku_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtJudul_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPengarang_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPenerbit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTahunTerbit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtKategori_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStokTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStokTersedia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLokasi_TextChanged(object sender, EventArgs e)
        {

        }

        private void btlBatal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Batalkan penambahan buku?\nData yang sudah diisi akan hilang.",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Validasi input wajib
            if (string.IsNullOrWhiteSpace(txtKodeBuku.Text) ||
                string.IsNullOrWhiteSpace(txtJudul.Text) ||
                string.IsNullOrWhiteSpace(txtPengarang.Text) ||
                string.IsNullOrWhiteSpace(txtStokTotal.Text) ||
                string.IsNullOrWhiteSpace(txtStokTersedia.Text))
            {
                MessageBox.Show("Kode Buku, Judul, Pengarang, Stok Total, dan Stok Tersedia wajib diisi!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi stok
            if (!int.TryParse(txtStokTotal.Text, out int stokTotal) ||
                !int.TryParse(txtStokTersedia.Text, out int stokTersedia))
            {
                MessageBox.Show("Stok harus berupa angka yang valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (stokTersedia > stokTotal)
            {
                MessageBox.Show("Stok Tersedia tidak boleh lebih besar dari Stok Total!",
                    "Validasi Stok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO BUKU 
                        (kode_buku, judul, pengarang, penerbit, tahun_terbit, kategori, 
                         stok_total, stok_tersedia, lokasi)
                        VALUES 
                        (@kode_buku, @judul, @pengarang, @penerbit, @tahun_terbit, @kategori, 
                         @stok_total, @stok_tersedia, @lokasi)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kode_buku", txtKodeBuku.Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@judul", txtJudul.Text.Trim());
                        cmd.Parameters.AddWithValue("@pengarang", txtPengarang.Text.Trim());
                        cmd.Parameters.AddWithValue("@penerbit",
                            string.IsNullOrWhiteSpace(txtPenerbit.Text) ? (object)DBNull.Value : txtPenerbit.Text.Trim());
                        cmd.Parameters.AddWithValue("@tahun_terbit",
                            string.IsNullOrWhiteSpace(txtTahunTerbit.Text) ? (object)DBNull.Value : txtTahunTerbit.Text.Trim());
                        cmd.Parameters.AddWithValue("@kategori",
                            string.IsNullOrWhiteSpace(txtKategori.Text) ? (object)DBNull.Value : txtKategori.Text.Trim());
                        cmd.Parameters.AddWithValue("@stok_total", stokTotal);
                        cmd.Parameters.AddWithValue("@stok_tersedia", stokTersedia);
                        cmd.Parameters.AddWithValue("@lokasi",
                            string.IsNullOrWhiteSpace(txtLokasi.Text) ? (object)DBNull.Value : txtLokasi.Text.Trim());

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Buku berhasil ditambahkan ke database!",
                                "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reset form setelah berhasil
                            ClearForm();
                            txtKodeBuku.Focus();
                        }
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Message.Contains("duplicate"))
            {
                MessageBox.Show("Kode Buku sudah ada di database!\nSilakan gunakan kode lain.",
                    "Duplikat Kode Buku", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat menambahkan buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtKodeBuku.Clear();
            txtJudul.Clear();
            txtPengarang.Clear();
            txtPenerbit.Clear();
            txtTahunTerbit.Clear();
            txtKategori.Clear();
            txtStokTotal.Clear();
            txtStokTersedia.Clear();
            txtLokasi.Clear();
        }
    }
}
