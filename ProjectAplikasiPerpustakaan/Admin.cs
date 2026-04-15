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
    public partial class Admin : Form
    {
        private readonly string namaAdmin;
        private readonly string roleAdmin;
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private DataTable dtBuku;


        public Admin(string nama, string role)
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            this.namaAdmin = nama;
            this.roleAdmin = role;
        }

        public Admin()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(namaAdmin))
            {
                this.Text = $"Admin Panel - {namaAdmin}";
                // lblWelcome.Text = $"Selamat datang, {namaAdmin}"; // uncomment jika ada label
            }

            LoadDataBuku();   // Tampilkan daftar buku otomatis saat form dibuka
        }

        // ================== LOAD DAFTAR BUKU ==================
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

                // Pengaturan tampilan DataGridView
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;

                // Sembunyikan kolom ID
                if (dataGridView1.Columns["id_buku"] != null)
                    dataGridView1.Columns["id_buku"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        // Tombol Refresh Daftar Buku
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataBuku();
        }

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

        // ================== TOMBOL LAINNYA (siap dikembangkan) ==================
        private void btnEditBuku_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih buku yang ingin diedit terlebih dahulu.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idBuku = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_buku"].Value);
                // EditBuku formEdit = new EditBuku(idBuku);
                // formEdit.ShowDialog();
                // LoadDataBuku(); // refresh setelah edit

                MessageBox.Show("Fitur Edit Buku akan dibuat selanjutnya.", "Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message);
            }
        }

        private void btnDaftarPengajuan_Click(object sender, EventArgs e)
        {
            try
            {
                // Panggil form DaftarPengajuan dan kirimkan nama admin
                btnKembali formPengajuan = new btnKembali(namaAdmin);
                formPengajuan.ShowDialog();   // Gunakan ShowDialog agar setelah selesai bisa refresh

                // Refresh daftar buku (stok mungkin berubah setelah persetujuan)
                LoadDataBuku();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka daftar pengajuan:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPengguna_Click(object sender, EventArgs e)
        {
            try
            {
                DaftarPengguna formDaftarPengguna = new DaftarPengguna();
                formDaftarPengguna.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka daftar pengguna:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event kosong yang tidak diperlukan lagi bisa dihapus atau dibiarkan
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bisa digunakan nanti jika ada tombol di dalam grid (misalnya tombol Edit/Hapus per baris)
        }
    }
}