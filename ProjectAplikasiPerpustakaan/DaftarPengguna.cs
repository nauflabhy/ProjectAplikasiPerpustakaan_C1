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

        private void DaftarPengguna_Load(object sender, EventArgs e)
        {
            LoadDaftarPengguna();
        }

        // ================== LOAD DAFTAR PENGGUNA (VERSI SEDERHANA) ==================
        private void LoadDaftarPengguna()
        {
            try
            {
                DataTable dt = new DataTable();

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

                    using (SqlDataAdapter da = new SqlDataAdapter(query, connection))
                    {
                        da.Fill(dt);
                    }
                }

                // Reset grid sepenuhnya untuk menghindari konflik kolom
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();           // ← ini penting
                dataGridView1.AutoGenerateColumns = true;

                dataGridView1.DataSource = dt;

                // Pengaturan tampilan
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Sembunyikan kolom ID
                if (dataGridView1.Columns["id_user"] != null)
                    dataGridView1.Columns["id_user"].Visible = false;

                // Debug - PASTI muncul ini
                MessageBox.Show($"Load berhasil!\nJumlah baris: {dt.Rows.Count}\nKolom: {dt.Columns.Count}",
                                "Debug Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tidak ada data pengguna di database.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR saat load data:\n" + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace,
                                "Error Load Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL REFRESH ==================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDaftarPengguna();
        }

        // ================== TOMBOL HAPUS PENGGUNA ==================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih pengguna yang ingin dihapus.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = dataGridView1.SelectedRows[0].Cells["Username"].Value.ToString();
            int idUser = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);

            if (username.ToLower() == "admin")
            {
                MessageBox.Show("Tidak dapat menghapus akun Admin utama!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult konfirmasi = MessageBox.Show(
                $"Yakin ingin menghapus pengguna:\n\nUsername: {username} ?\n\nData ini tidak dapat dikembalikan.",
                "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                    string query = "DELETE FROM Pengguna WHERE id_user = @id_user";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id_user", idUser);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Pengguna berhasil dihapus.", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDaftarPengguna(); // Refresh
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus pengguna:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur Edit Pengguna akan dibuat selanjutnya.", "Info");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur Update Pengguna akan dibuat selanjutnya.", "Info");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bisa digunakan nanti jika ingin tambah tombol di dalam grid
        }
    }
}