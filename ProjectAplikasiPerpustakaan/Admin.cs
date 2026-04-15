using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class Admin : Form
    {
        private readonly string namaAdmin;
        private readonly string roleAdmin;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private DataTable dtBuku;

        public Admin(string nama, string role)
        {
            InitializeComponent();
            this.namaAdmin = nama;
            this.roleAdmin = role;
        }

        public Admin()
        {
            InitializeComponent();
        }

        // ================== FORM LOAD (TIDAK load data otomatis) ==================
        private void Admin_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(namaAdmin))
            {
                this.Text = $"Admin Panel - {namaAdmin}";
            }

            // TIDAK memanggil LoadDataBuku() lagi
            // Data akan muncul hanya setelah tombol Load Database ditekan
        }

        // ================== LOAD DAFTAR BUKU ==================
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
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToDeleteRows = false;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;

                    // Sembunyikan kolom ID
                    if (dataGridView1.Columns["id_buku"] != null)
                        dataGridView1.Columns["id_buku"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL LOAD DATABASE ==================
        private void btnLoadDatabase_Click(object sender, EventArgs e)
        {
            LoadDataBuku();
        }

        // ================== TOMBOL EDIT BUKU ==================
        private void btnEditBuku_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih buku yang ingin diedit.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idBuku = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_buku"].Value);

            EditBuku formEdit = new EditBuku(idBuku);
            formEdit.ShowDialog();

            if (formEdit.DialogResult == DialogResult.OK)
            {
                LoadDataBuku();        // Refresh setelah edit
            }
        }

        // ================== TOMBOL DAFTAR PENGAJUAN ==================
        private void btnDaftarPengajuan_Click(object sender, EventArgs e)
        {
            try
            {
                btnKembali formPengajuan = new btnKembali(namaAdmin, roleAdmin);
                formPengajuan.ShowDialog();

                // Refresh data setelah kembali dari form pengajuan
                LoadDataBuku();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka daftar pengajuan:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL DAFTAR PENGGUNA ==================
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

        // ================== TOMBOL LAPORAN ==================
        private void btnLaporan_Click(object sender, EventArgs e)
        {
            try
            {
                CetakLaporan formLaporan = new CetakLaporan();
                formLaporan.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka form Laporan:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}