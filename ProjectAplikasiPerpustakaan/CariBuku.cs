using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjectAplikasiPerpustakaan
{
    public partial class CariBuku : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly string namaPengguna;
        private readonly string rolePengguna;

        private DataTable dtBuku;


        public CariBuku(string nama, string role)
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            this.namaPengguna = nama;   // ← pastikan baris ini ADA
            this.rolePengguna = role;   // ← pastikan baris ini ADA
        }

        public CariBuku()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

        }

        private void btnTampilBuku_Click(object sender, EventArgs e)
        {

        }

        private void btnPinjam_Click(object sender, EventArgs e)
        {
            // Cek apakah ada baris yang dipilih di DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih buku yang ingin dipinjam terlebih dahulu.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil data buku dari baris yang dipilih
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            int idBuku = Convert.ToInt32(selectedRow.Cells["id_buku"].Value);
            string kodeBuku = selectedRow.Cells["kode_buku"].Value.ToString();
            string judulBuku = selectedRow.Cells["judul"].Value.ToString();
            int stokTersedia = Convert.ToInt32(selectedRow.Cells["stok_tersedia"].Value);

            // Cek stok tersedia
            if (stokTersedia <= 0)
            {
                MessageBox.Show($"Buku \"{judulBuku}\" sedang tidak tersedia (stok habis).",
                    "Stok Habis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Konfirmasi peminjaman
            DialogResult konfirmasi = MessageBox.Show(
                $"Anda akan meminjam buku:\n\nJudul : {judulBuku}\nKode  : {kodeBuku}\n\nLanjutkan?",
                "Konfirmasi Peminjaman", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                // Buka form Pinjam dan kirim data buku yang dipilih
                Pinjam formPinjam = new Pinjam(idBuku, kodeBuku, judulBuku, namaPengguna, rolePengguna);
                formPinjam.Show();
                this.Hide(); // Sembunyikan form CariBuku, atau gunakan this.Close() jika ingin ditutup
            }
        }

        private void btnKembalikan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(namaPengguna))
            {
                MessageBox.Show("Data pengguna tidak ditemukan.\nSilakan login kembali.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Buka form KembalikanBuku dan kirimkan namaPengguna
            KembalikanBuku formKembali = new KembalikanBuku(namaPengguna);

            formKembali.Show();           // Buka form (bisa kembali ke CariBuku)
                                          // this.Hide();               // Opsional: Sembunyikan form CariBuku
                                          // this.Close();              // Atau tutup form CariBuku (pilih salah satu)
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Hindari klik pada header kolom
            dataGridView1.ReadOnly = true;
        }

        private void CariBukuByKeyword()
        {
            if (dtBuku == null) return;

            string keyword = txtCariBuku.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                dataGridView1.DataSource = dtBuku;   // Tampilkan semua
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

        private void CariBuku_Load(object sender, EventArgs e)
        {
            LoadDataBuku();        // Tampilkan semua buku saat form dibuka
            txtCariBuku.Focus();   // Fokus ke textbox pencarian
        }

        private void LoadDataBuku()
        {
            try
            {
                conn.Open();
                string query = @"SELECT 
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

                // Atur tampilan kolom (opsional)
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["id_buku"].Visible = false; // sembunyikan ID jika tidak perlu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            CariBukuByKeyword();
            txtCariBuku.Focus();
        }

        private void btnBukuDipinjam_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(namaPengguna))
            {
                MessageBox.Show("Data pengguna tidak ditemukan. Silakan login kembali.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Buka form BukuDipinjamPengunjung dan kirimkan namaPengguna + role
            BukuDipinjamPengunjung formRiwayat = new BukuDipinjamPengunjung(namaPengguna, rolePengguna);
            formRiwayat.Show();        // Gunakan Show() agar bisa kembali ke CariBuku
                                       // formRiwayat.ShowDialog(); // Jika ingin modal (tidak bisa klik form lain)
        }

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
    }
}
