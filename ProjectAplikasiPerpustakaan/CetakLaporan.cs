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

        private void CetakLaporan_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataLaporan();
        }

        // ================== PENGATURAN DATAGRIDVIEW ==================
        private void SetupDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        }

        // ================== LOAD DATA LAPORAN ==================
        private void LoadDataLaporan()
        {
            string query = @"
                SELECT 
                    id_laporan,
                    periode,
                    total_kunjungan AS [Total Kunjungan],
                    total_peminjaman AS [Total Peminjaman],
                    total_pengembalian AS [Total Pengembalian],
                    total_denda AS [Total Denda (Rp)],
                    generated_at AS [Tanggal Dibuat]
                FROM LAPORAN
                ORDER BY periode DESC, generated_at DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    dtLaporan = new DataTable();
                    da.Fill(dtLaporan);

                    dataGridView1.DataSource = dtLaporan;

                    // Sembunyikan kolom ID
                    if (dataGridView1.Columns["id_laporan"] != null)
                        dataGridView1.Columns["id_laporan"].Visible = false;

                    // Format tampilan kolom
                    if (dataGridView1.Columns["Total Denda (Rp)"] != null)
                    {
                        dataGridView1.Columns["Total Denda (Rp)"].DefaultCellStyle.Format = "N2";
                        dataGridView1.Columns["Total Denda (Rp)"].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleRight;
                    }

                    // Format kolom angka lainnya
                    string[] kolomAngka = { "Total Kunjungan", "Total Peminjaman", "Total Pengembalian" };
                    foreach (string kolom in kolomAngka)
                    {
                        if (dataGridView1.Columns[kolom] != null)
                        {
                            dataGridView1.Columns[kolom].DefaultCellStyle.Alignment =
                                DataGridViewContentAlignment.MiddleCenter;
                        }
                    }

                    // Format tanggal
                    if (dataGridView1.Columns["Tanggal Dibuat"] != null)
                    {
                        dataGridView1.Columns["Tanggal Dibuat"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    }

                    // Update judul form
                    this.Text = $"Cetak Laporan - {dtLaporan.Rows.Count} Data";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data laporan:\n" + ex.Message,
                    "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL REFRESH ==================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataLaporan();
        }

        // ================== TOMBOL KEMBALI ==================
        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Optional: Double click untuk melihat detail laporan (bisa dikembangkan)
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string periode = dataGridView1.Rows[e.RowIndex].Cells["periode"].Value.ToString();
                MessageBox.Show($"Detail Laporan Periode: {periode}\n\n" +
                              "Fitur cetak PDF akan ditambahkan di versi selanjutnya.",
                              "Informasi Laporan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}