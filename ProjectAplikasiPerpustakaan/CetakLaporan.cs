using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class CetakLaporan : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private DataTable dtLaporan;

        public CetakLaporan()
        {
            InitializeComponent();
        }

        // ================== FORM LOAD ==================
        private void CetakLaporan_Load(object sender, EventArgs e)
        {
            LoadDataLaporan();
        }

        // ================== LOAD DATA DARI TABEL LAPORAN ==================
        private void LoadDataLaporan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            id_laporan,
                            periode,
                            total_kunjungan,
                            total_peminjaman,
                            total_pengembalian,
                            total_denda,
                            generated_at,
                            (SELECT username FROM Pengguna p 
                             JOIN ADMIN a ON p.id_user = a.id_user 
                             WHERE a.id_admin = LAPORAN.id_admin) AS nama_admin
                        FROM LAPORAN
                        ORDER BY generated_at DESC;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        dtLaporan = new DataTable();
                        adapter.Fill(dtLaporan);

                        // Tampilkan di DataGridView
                        dgvLaporan.DataSource = dtLaporan;

                        // Atur tampilan kolom agar lebih rapi
                        if (dgvLaporan.Columns.Count > 0)
                        {
                            dgvLaporan.Columns["id_laporan"].HeaderText = "ID Laporan";
                            dgvLaporan.Columns["periode"].HeaderText = "Periode";
                            dgvLaporan.Columns["total_kunjungan"].HeaderText = "Total Kunjungan";
                            dgvLaporan.Columns["total_peminjaman"].HeaderText = "Total Peminjaman";
                            dgvLaporan.Columns["total_pengembalian"].HeaderText = "Total Pengembalian";
                            dgvLaporan.Columns["total_denda"].HeaderText = "Total Denda (Rp)";
                            dgvLaporan.Columns["generated_at"].HeaderText = "Tanggal Dibuat";
                            dgvLaporan.Columns["nama_admin"].HeaderText = "Dibuat Oleh";

                            // Format kolom denda menjadi mata uang
                            dgvLaporan.Columns["total_denda"].DefaultCellStyle.Format = "N0";
                            dgvLaporan.Columns["total_denda"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                            dgvLaporan.Columns["generated_at"].DefaultCellStyle.Format = "dd MMMM yyyy HH:mm";
                        }

                        // Jika tidak ada data
                        if (dtLaporan.Rows.Count == 0)
                        {
                            MessageBox.Show("Belum ada data laporan yang tersedia.", "Informasi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat memuat data laporan:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ================== TOMBOL CETAK / EXPORT (Opsional) ==================


        // ================== TOMBOL TUTUP ==================
        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCetak_Click_1(object sender, EventArgs e)
        {
            if (dtLaporan == null || dtLaporan.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk dicetak.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Untuk sementara tampilkan pesan (bisa dikembangkan ke Crystal Report / PrintDocument nanti)
            MessageBox.Show($"Mencetak {dtLaporan.Rows.Count} data laporan...\n\n" +
                          "Fitur cetak lengkap akan ditambahkan kemudian.",
                          "Cetak Laporan", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            LoadDataLaporan();
        }
    }
}