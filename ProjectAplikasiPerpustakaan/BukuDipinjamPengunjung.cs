using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class BukuDipinjamPengunjung : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly string namaPengguna;
        private readonly string rolePengguna;
        public BukuDipinjamPengunjung (string nama, string role)
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            this.namaPengguna = nama;
            this.rolePengguna = role;
        }

        private void BukuDipinjamPengunjung_Load(object sender, EventArgs e)
        {
            TampilkanBukuDipinjam();
        }

        private void TampilkanBukuDipinjam()
        {
            try
            {
                conn.Open();

                string query = @"
                    SELECT 
                        b.kode_buku AS [Kode Buku],
                        b.judul AS [Judul Buku],
                        b.pengarang AS [Pengarang],
                        b.kategori AS [Kategori],
                        pm.tanggal_ajuan AS [Tanggal Ajuan],
                        pm.tanggal_pinjam AS [Tanggal Pinjam],
                        pm.tanggal_jatuh_tempo AS [Jatuh Tempo],
                        pm.status AS [Status],
                        DATEDIFF(DAY, GETDATE(), pm.tanggal_jatuh_tempo) AS [Sisa Hari]
                    FROM PEMINJAMAN pm
                    INNER JOIN PENGUNJUNG pg ON pm.id_pengunjung = pg.id_pengunjung
                    INNER JOIN Pengguna u ON pg.id_user = u.id_user
                    INNER JOIN BUKU b ON pm.id_buku = b.id_buku
                    WHERE u.username = @namaPengguna 
                      AND pm.status IN ('menunggu', 'disetujui', 'dipinjam')
                    ORDER BY pm.tanggal_ajuan DESC";

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

                // Pengaturan tampilan DataGridView
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;

                // Ubah warna header agar lebih jelas
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                // Jika ingin mewarnai baris berdasarkan status (opsional tapi bagus)
                WarnaiBarisBerdasarkanStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data buku yang dipinjam:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void WarnaiBarisBerdasarkanStatus()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Status"].Value != null)
                {
                    string status = row.Cells["Status"].Value.ToString().ToLower();

                    if (status == "menunggu")
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    else if (status == "disetujui" || status == "dipinjam")
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    else if (status == "ditolak")
                        row.DefaultCellStyle.BackColor = Color.LightPink;
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bisa dikosongkan atau digunakan nanti jika ingin tambah fitur klik
        }

        private void btnKembali_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}