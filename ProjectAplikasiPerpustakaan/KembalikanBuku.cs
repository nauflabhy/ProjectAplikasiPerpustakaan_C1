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
    public partial class KembalikanBuku : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly string namaPengguna;
        private int idPeminjamanTerpilih = 0;

        public KembalikanBuku(string namaPengguna)
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            this.namaPengguna = namaPengguna;
        }

        // ================== Event DataGridView ==================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idPeminjamanTerpilih = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id_peminjaman"].Value);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void WarnaiBarisBerdasarkanStatus()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Status"].Value != null)
                {
                    string status = row.Cells["Status"].Value.ToString().ToLower();
                    if (status == "dipinjam")
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        // ================== Tombol ==================
        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKembalikanBuku_Click(object sender, EventArgs e)
        {
            if (idPeminjamanTerpilih == 0)
            {
                MessageBox.Show("Silakan pilih buku yang ingin dikembalikan terlebih dahulu!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil data buku dari DataGridView
            var row = dataGridView1.Rows[dataGridView1.CurrentRow.Index];

            string kodeBuku = row.Cells["Kode Buku"].Value?.ToString();
            string judulBuku = row.Cells["Judul Buku"].Value?.ToString();
            DateTime tglPinjam = Convert.ToDateTime(row.Cells["Tanggal Pinjam"].Value);
            DateTime tglJatuhTempo = Convert.ToDateTime(row.Cells["Jatuh Tempo"].Value);

            using (var formPengembalian = new FormPengembalian(
                idPeminjamanTerpilih,
                namaPengguna,
                kodeBuku,
                judulBuku,
                tglPinjam,
                tglJatuhTempo))
            {
                if (formPengembalian.ShowDialog() == DialogResult.OK)
                {
                    TampilkanBukuYangDipinjam();
                }
            }

            idPeminjamanTerpilih = 0;
        }

        // ================== Load & Tampil Data ==================
        private void KembalikanBuku_Load(object sender, EventArgs e)
        {
            TampilkanBukuYangDipinjam();
        }

        private void TampilkanBukuYangDipinjam()
        {
            try
            {
                conn.Open();
                string query = @"
                    SELECT
                        pm.id_peminjaman,
                        b.kode_buku AS [Kode Buku],
                        b.judul AS [Judul Buku],
                        b.pengarang AS [Pengarang],
                        pm.tanggal_pinjam AS [Tanggal Pinjam],
                        pm.tanggal_jatuh_tempo AS [Jatuh Tempo],
                        DATEDIFF(DAY, GETDATE(), pm.tanggal_jatuh_tempo) AS [Sisa Hari],
                        pm.status AS [Status]
                    FROM PEMINJAMAN pm
                    INNER JOIN PENGUNJUNG pg ON pm.id_pengunjung = pg.id_pengunjung
                    INNER JOIN Pengguna u ON pg.id_user = u.id_user
                    INNER JOIN BUKU b ON pm.id_buku = b.id_buku
                    WHERE u.username = @namaPengguna
                      AND pm.status IN ('dipinjam', 'disetujui')
                    ORDER BY pm.tanggal_pinjam DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@namaPengguna", namaPengguna);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }

                // Pengaturan DataGridView
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;

                if (dataGridView1.Columns["id_peminjaman"] != null)
                    dataGridView1.Columns["id_peminjaman"].Visible = false;

                WarnaiBarisBerdasarkanStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data buku yang dipinjam:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}