using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class EditBuku : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly int idBuku;

        // Constructor yang menerima id_buku
        public EditBuku(int idBuku)
        {
            InitializeComponent();
            this.idBuku = idBuku;
        }

        // ================== LOAD DATA SAAT FORM DIBUKA ==================
        private void EditBuku_Load(object sender, EventArgs e)
        {
            LoadDataBuku();
        }

        private void LoadDataBuku()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT kode_buku, judul, pengarang, penerbit, 
                               tahun_terbit, stok_total, stok_tersedia, lokasi 
                        FROM BUKU 
                        WHERE id_buku = @idBuku";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idBuku", idBuku);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtKodeBuku.Text = reader["kode_buku"].ToString();
                                txtJudulBuku.Text = reader["judul"].ToString();
                                txtPengarang.Text = reader["pengarang"].ToString();
                                txtPenerbit.Text = reader["penerbit"].ToString();
                                txtTahunTerbit.Text = reader["tahun_terbit"].ToString();
                                txtStokTotal.Text = reader["stok_total"].ToString();
                                txtStokTersedia.Text = reader["stok_tersedia"].ToString();
                                txtLokasi.Text = reader["lokasi"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Data buku tidak ditemukan!", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL UPDATE ==================
        private void btlUpdateBuku_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKodeBuku.Text) ||
                string.IsNullOrWhiteSpace(txtJudulBuku.Text) ||
                string.IsNullOrWhiteSpace(txtPengarang.Text))
            {
                MessageBox.Show("Kode Buku, Judul, dan Pengarang harus diisi!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtTahunTerbit.Text, out int tahun) || tahun < 1900)
            {
                MessageBox.Show("Tahun terbit tidak valid!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStokTotal.Text, out int stokTotal) || stokTotal < 0)
            {
                MessageBox.Show("Stok Total harus angka positif!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStokTersedia.Text, out int stokTersedia) ||
                stokTersedia < 0 || stokTersedia > stokTotal)
            {
                MessageBox.Show("Stok Tersedia harus antara 0 sampai Stok Total!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE BUKU 
                        SET kode_buku = @kode,
                            judul = @judul,
                            pengarang = @pengarang,
                            penerbit = @penerbit,
                            tahun_terbit = @tahun,
                            stok_total = @stokTotal,
                            stok_tersedia = @stokTersedia,
                            lokasi = @lokasi
                        WHERE id_buku = @idBuku";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kode", txtKodeBuku.Text.Trim());
                        cmd.Parameters.AddWithValue("@judul", txtJudulBuku.Text.Trim());
                        cmd.Parameters.AddWithValue("@pengarang", txtPengarang.Text.Trim());
                        cmd.Parameters.AddWithValue("@penerbit", txtPenerbit.Text.Trim());
                        cmd.Parameters.AddWithValue("@tahun", tahun);
                        cmd.Parameters.AddWithValue("@stokTotal", stokTotal);
                        cmd.Parameters.AddWithValue("@stokTersedia", stokTersedia);
                        cmd.Parameters.AddWithValue("@lokasi", txtLokasi.Text.Trim());
                        cmd.Parameters.AddWithValue("@idBuku", idBuku);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Data buku berhasil diperbarui!",
                                "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui data buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL BATAL ==================
        private void btlBatal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Batalkan perubahan data buku?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void EditBuku_Load_1(object sender, EventArgs e)
        {
            LoadDataBuku();
        }


        private void txtKodeBuku_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtJudulBuku_TextChanged(object sender, EventArgs e)
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

        private void txtStokTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStokTersedia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLokasi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
