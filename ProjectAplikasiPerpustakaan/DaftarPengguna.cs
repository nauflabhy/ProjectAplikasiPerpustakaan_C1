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