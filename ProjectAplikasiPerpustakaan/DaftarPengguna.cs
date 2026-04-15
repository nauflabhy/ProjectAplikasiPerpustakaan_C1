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
    public partial class DaftarPengguna : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        public DaftarPengguna()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        // ================== LOAD DAFTAR PENGGUNA (VERSI SEDERHANA) ==================

        // ================== TOMBOL REFRESH ==================

        // ================== TOMBOL HAPUS PENGGUNA ==================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih pengguna yang ingin dihapus.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil data dari baris yang dipilih
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            int idUser = Convert.ToInt32(row.Cells["id_user"].Value);
            string username = row.Cells["username"].Value.ToString();

            // Proteksi akun Admin utama
            if (username.ToLower() == "admin")
            {
                MessageBox.Show("Tidak dapat menghapus akun Admin utama!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Konfirmasi sebelum menghapus
            DialogResult konfirmasi = MessageBox.Show(
                $"Yakin ingin menghapus pengguna:\n\n" +
                $"Username : {username}\n" +
                $"ID       : {idUser}\n\n" +
                "Data ini tidak dapat dikembalikan.",
                "Konfirmasi Hapus Pengguna",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (konfirmasi == DialogResult.Yes)
            {
                HapusPengguna(idUser);
            }
        }

        private void HapusPengguna(int idUser)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Langkah 1: Ambil id_pengunjung dari tabel PENGUNJUNG
                            int? idPengunjung = null;
                            string queryIdPengunjung = "SELECT id_pengunjung FROM PENGUNJUNG WHERE id_user = @idUser";
                            using (SqlCommand cmd = new SqlCommand(queryIdPengunjung, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@idUser", idUser);
                                object result = cmd.ExecuteScalar();
                                if (result != null && result != DBNull.Value)
                                {
                                    idPengunjung = Convert.ToInt32(result);
                                }
                            }

                            if (idPengunjung.HasValue)
                            {
                                // Langkah 2: Hapus dari PENGEMBALIAN terlebih dahulu (karena bergantung ke PEMINJAMAN)
                                string sqlPengembalian = @"
                            DELETE FROM PENGEMBALIAN 
                            WHERE id_peminjaman IN (
                                SELECT id_peminjaman 
                                FROM PEMINJAMAN 
                                WHERE id_pengunjung = @idPengunjung
                            )";
                                using (SqlCommand cmd = new SqlCommand(sqlPengembalian, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@idPengunjung", idPengunjung.Value);
                                    cmd.ExecuteNonQuery();
                                }

                                // Langkah 3: Hapus dari PEMINJAMAN
                                string sqlPeminjaman = "DELETE FROM PEMINJAMAN WHERE id_pengunjung = @idPengunjung";
                                using (SqlCommand cmd = new SqlCommand(sqlPeminjaman, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@idPengunjung", idPengunjung.Value);
                                    cmd.ExecuteNonQuery();
                                }

                                // Langkah 4: Hapus dari KUNJUNGAN
                                string sqlKunjungan = "DELETE FROM KUNJUNGAN WHERE id_pengunjung = @idPengunjung";
                                using (SqlCommand cmd = new SqlCommand(sqlKunjungan, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@idPengunjung", idPengunjung.Value);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // Langkah 5: Hapus dari PENGUNJUNG
                            string sqlPengunjung = "DELETE FROM PENGUNJUNG WHERE id_user = @idUser";
                            using (SqlCommand cmd = new SqlCommand(sqlPengunjung, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@idUser", idUser);
                                cmd.ExecuteNonQuery();
                            }

                            // Langkah 6: Hapus dari Pengguna
                            string sqlPengguna = "DELETE FROM Pengguna WHERE id_user = @idUser";
                            using (SqlCommand cmd = new SqlCommand(sqlPengguna, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@idUser", idUser);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            MessageBox.Show("Pengguna berhasil dihapus beserta semua data terkaitnya\n(kunjungan, peminjaman, dan pengembalian).",
                                "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh grid
                            DaftarPengguna_Load_1(this, EventArgs.Empty);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Gagal menghapus pengguna:\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR koneksi database:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih pengguna yang ingin diedit.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idUser = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_user"].Value);

            EditPengguna formEdit = new EditPengguna(idUser);
            formEdit.ShowDialog(); // pakai ShowDialog agar modal

            // Setelah form ditutup, refresh data otomatis
            if (formEdit.DialogResult == DialogResult.OK)
            {
                DaftarPengguna_Load_1(this, EventArgs.Empty);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bisa digunakan nanti jika ingin tambah tombol di dalam grid
        }

        private void DaftarPengguna_Load_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT 
                    u.id_user,
                    u.username,
                    u.role,
                    u.created_at,
                    ISNULL(p.nama_lengkap, u.nama_lengkap) AS nama_lengkap,
                    ISNULL(p.no_hp, u.no_hp) AS no_hp,
                    ISNULL(p.email, u.email) AS email,
                    p.perguruan,
                    p.nik
                FROM Pengguna u
                LEFT JOIN PENGUNJUNG p ON u.id_user = p.id_user
                ORDER BY u.id_user DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Reset total
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    dataGridView1.AutoGenerateColumns = true;
                    dataGridView1.DataSource = dt;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}