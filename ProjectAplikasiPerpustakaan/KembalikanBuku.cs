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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idPeminjamanTerpilih = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id_peminjaman"].Value);
            }
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

            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin mengembalikan buku ini?",
                "Konfirmasi Pengembalian", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                ProsesPengembalian();
            }
        }

        private void ProsesPengembalian()
        {
            try
            {
                conn.Open();

                string query = @"
                    INSERT INTO PENGEMBALIAN (id_peminjaman, tanggal_ajuan, status)
                    VALUES (@id_peminjaman, GETDATE(), 'menunggu');

                    UPDATE PEMINJAMAN 
                    SET status = 'selesai' 
                    WHERE id_peminjaman = @id_peminjaman;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_peminjaman", idPeminjamanTerpilih);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Pengembalian buku berhasil diajukan!\nSilakan tunggu verifikasi admin.",
                            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh data
                        TampilkanBukuYangDipinjam();
                        idPeminjamanTerpilih = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mengajukan pengembalian:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

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

                // Sembunyikan kolom id_peminjaman
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
