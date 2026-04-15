using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class CariBuku : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly string namaPengguna;
        private readonly string rolePengguna;
        private DataTable dtBuku;

        // Constructor untuk user yang sudah login
        public CariBuku(string nama, string role)
        {
            InitializeComponent();
            this.namaPengguna = nama;
            this.rolePengguna = role;
        }


        // ================== LOAD DATA BUKU (dipanggil oleh tombol) ==================
        private void LoadDataBuku()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            id_buku,
                            kode_buku,
                            judul,
                            pengarang,
                            penerbit,
                            tahun_terbit,
                            kategori,
                            stok_total,
                            stok_tersedia,
                            lokasi
                        FROM BUKU
                        ORDER BY judul ASC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        dtBuku = new DataTable();
                        da.Fill(dtBuku);
                        dataGridView1.DataSource = dtBuku;
                    }

                    // Pengaturan tampilan DataGridView
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    if (dataGridView1.Columns["id_buku"] != null)
                        dataGridView1.Columns["id_buku"].Visible = false; // Sembunyikan ID
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL LOAD DATABASE ==================
        private void btnLoadDatabase_Click(object sender, EventArgs e)
        {
            LoadDataBuku();
            txtCariBuku.Focus(); // Fokus ke textbox pencarian setelah load
        }

        // ================== PENCARIAN OTOMATIS ==================
        private void CariBukuByKeyword()
        {
            if (dtBuku == null) return;

            string keyword = txtCariBuku.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                dataGridView1.DataSource = dtBuku;
            }
            else
            {
                DataView dv = dtBuku.DefaultView;
                dv.RowFilter = $"judul LIKE '%{keyword}%' " +
                               $"OR pengarang LIKE '%{keyword}%' " +
                               $"OR kategori LIKE '%{keyword}%' " +
                               $"OR kode_buku LIKE '%{keyword}%'";
                dataGridView1.DataSource = dv;
            }
        }

        private void txtCariBuku_TextChanged(object sender, EventArgs e)
        {
            CariBukuByKeyword();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            CariBukuByKeyword();
            txtCariBuku.Focus();
        }

        // ================== FORM LOAD (Hanya inisialisasi, tidak load data) ==================
        private void CariBuku_Load(object sender, EventArgs e)
        {
            // TIDAK memanggil LoadDataBuku() lagi
            // Form akan kosong saat pertama dibuka
            txtCariBuku.Focus();
        }

        // ================== TOMBOL PINJAM ==================
        private void btnPinjam_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih buku yang ingin dipinjam terlebih dahulu.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int idBuku = Convert.ToInt32(selectedRow.Cells["id_buku"].Value);
            string kodeBuku = selectedRow.Cells["kode_buku"].Value.ToString();
            string judulBuku = selectedRow.Cells["judul"].Value.ToString();
            int stokTersedia = Convert.ToInt32(selectedRow.Cells["stok_tersedia"].Value);

            if (stokTersedia <= 0)
            {
                MessageBox.Show($"Buku \"{judulBuku}\" sedang tidak tersedia (stok habis).",
                    "Stok Habis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show(
                $"Anda akan meminjam buku:\n\nJudul : {judulBuku}\nKode : {kodeBuku}\n\nLanjutkan?",
                "Konfirmasi Peminjaman", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                Pinjam formPinjam = new Pinjam(idBuku, kodeBuku, judulBuku, namaPengguna, rolePengguna);
                formPinjam.Show();
                this.Hide();
            }
        }

        // ================== TOMBOL KEMBALIKAN ==================
        private void btnKembalikan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(namaPengguna))
            {
                MessageBox.Show("Data pengguna tidak ditemukan.\nSilakan login kembali.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            KembalikanBuku formKembali = new KembalikanBuku(namaPengguna);
            formKembali.Show();
        }

        // ================== TOMBOL DETAIL BUKU ==================
        private void btnDetailBuku_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih buku terlebih dahulu untuk melihat detail.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int idBuku = Convert.ToInt32(selectedRow.Cells["id_buku"].Value);
                string judulBuku = selectedRow.Cells["judul"].Value?.ToString() ?? "";

                DetailBuku formDetail = new DetailBuku(idBuku, judulBuku, namaPengguna);
                formDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka detail buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL RIWAYAT BUKU DIPINJAM ==================
        private void btnBukuDipinjam_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(namaPengguna))
            {
                MessageBox.Show("Data pengguna tidak ditemukan. Silakan login kembali.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BukuDipinjamPengunjung formRiwayat = new BukuDipinjamPengunjung(namaPengguna, rolePengguna);
            formRiwayat.Show();
        }

        // ================== LOGOUT ==================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?",
                "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                LoginMenu formLogin = new LoginMenu();
                formLogin.Show();
                this.Close();
            }
        }

        // Event kosong (bisa dihapus jika tidak dipakai)
        private void btnConnect_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}