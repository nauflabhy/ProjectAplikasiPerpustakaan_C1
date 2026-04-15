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
            LoadCombinedReport();           // Ganti ke method baru
        }

        // ================== SETUP DATAGRIDVIEW ==================
        private void SetupDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGridView1.RowHeadersVisible = false;
        }

        // ================== LOAD DATA LAPORAN + PENGEMBALIAN ==================
        private void LoadCombinedReport(string periodeFilter = "")
        {
            string query = @"
        SELECT 
            L.id_laporan,
            L.periode,
            L.total_kunjungan AS [Total Kunjungan],
            L.total_peminjaman AS [Total Peminjaman],
            L.total_pengembalian AS [Total Pengembalian],
            L.total_denda AS [Total Denda (Summary)],
            ISNULL(SUM(P.denda), 0) AS [Total Denda Aktual],
            COUNT(P.id_pengembalian) AS [Jumlah Pengembalian Detail],
            MAX(P.tanggal_kembali) AS [Pengembalian Terakhir],
            L.generated_at AS [Tanggal Dibuat]
        FROM LAPORAN L
        LEFT JOIN PENGEMBALIAN P ON P.id_peminjaman IN 
            (SELECT id_peminjaman FROM PEMINJAMAN)  -- Safety join
        LEFT JOIN PEMINJAMAN PM ON PM.id_peminjaman = P.id_peminjaman
        WHERE (@periode = '' OR L.periode = @periode)
        GROUP BY 
            L.id_laporan, L.periode, L.total_kunjungan, 
            L.total_peminjaman, L.total_pengembalian, 
            L.total_denda, L.generated_at
        ORDER BY L.periode DESC, L.generated_at DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@periode", periodeFilter);

                    dtLaporan = new DataTable();
                    da.Fill(dtLaporan);

                    dataGridView1.DataSource = dtLaporan;

                    // Sembunyikan ID
                    if (dataGridView1.Columns["id_laporan"] != null)
                        dataGridView1.Columns["id_laporan"].Visible = false;

                    FormatGridColumns();
                }

                this.Text = $"Cetak Laporan - {dtLaporan.Rows.Count} Data";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data laporan:\n" + ex.Message,
                    "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGridColumns()
        {
            // Format Rupiah
            var colDendaSummary = dataGridView1.Columns["Total Denda (Summary)"];
            var colDendaAktual = dataGridView1.Columns["Total Denda Aktual"];

            if (colDendaSummary != null)
            {
                colDendaSummary.DefaultCellStyle.Format = "N0";
                colDendaSummary.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (colDendaAktual != null)
            {
                colDendaAktual.DefaultCellStyle.Format = "N0";
                colDendaAktual.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Format angka tengah
            string[] kolomAngka = { "Total Kunjungan", "Total Peminjaman", "Total Pengembalian", "Jumlah Pengembalian Detail" };
            foreach (string kolom in kolomAngka)
            {
                if (dataGridView1.Columns[kolom] != null)
                {
                    dataGridView1.Columns[kolom].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            // Format tanggal
            if (dataGridView1.Columns["Tanggal Dibuat"] != null)
                dataGridView1.Columns["Tanggal Dibuat"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            if (dataGridView1.Columns["Pengembalian Terakhir"] != null)
                dataGridView1.Columns["Pengembalian Terakhir"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        // ================== TOMBOL REFRESH ==================


        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Double click untuk melihat detail pengembalian
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string periode = dataGridView1.Rows[e.RowIndex].Cells["periode"].Value.ToString();
                MessageBox.Show($"Menampilkan detail pengembalian periode: {periode}\n\n" +
                              "Fitur ini dapat dikembangkan lebih lanjut (buka form detail).",
                              "Detail Laporan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCombinedReport();
        }
    }
}