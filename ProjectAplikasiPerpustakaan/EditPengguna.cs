using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class EditPengguna : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly int idUser;

        public EditPengguna(int idUser)
        {
            InitializeComponent();
            this.idUser = idUser;
        }

        private void EditPengguna_Load(object sender, EventArgs e)
        {
            AmbilDataPengguna();
        }

        // ================== AMBIL DATA DARI DB ==================
        private void AmbilDataPengguna()
        {
            string query = @"
                SELECT 
                    u.username,
                    ISNULL(p.nama_lengkap, u.nama_lengkap) AS nama_lengkap,
                    ISNULL(p.no_hp, u.no_hp)               AS no_hp,
                    ISNULL(p.email, u.email)               AS email,
                    p.perguruan,
                    p.nik
                FROM Pengguna u
                LEFT JOIN PENGUNJUNG p ON u.id_user = p.id_user
                WHERE u.id_user = @id_user";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id_user", idUser);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtUsername.Text = reader["username"].ToString();
                        txtNamaLengkap.Text = reader["nama_lengkap"].ToString();
                        txtNoHp.Text = reader["no_hp"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        txtPerguruan.Text = reader["perguruan"].ToString();
                        txtNIK.Text = reader["nik"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Data pengguna tidak ditemukan.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengambil data:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================== SIMPAN PERUBAHAN ==================
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string namaLengkap = txtNamaLengkap.Text.Trim();
            string noHp = txtNoHp.Text.Trim();
            string email = txtEmail.Text.Trim();
            string perguruan = txtPerguruan.Text.Trim();
            string nik = txtNIK.Text.Trim();

            // Validasi minimal
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username tidak boleh kosong.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            DialogResult konfirmasi = MessageBox.Show(
                $"Simpan perubahan data pengguna:\n\"{username}\"?",
                "Konfirmasi Simpan",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi != DialogResult.Yes) return;

            UpdatePengguna(username, namaLengkap, noHp, email, perguruan, nik);
        }

        private void UpdatePengguna(string username, string namaLengkap,
                                    string noHp, string email,
                                    string perguruan, string nik)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Update tabel Pengguna
                    string queryPengguna = @"
                        UPDATE Pengguna
                        SET username     = @username,
                            nama_lengkap = @nama_lengkap,
                            no_hp        = @no_hp,
                            email        = @email
                        WHERE id_user = @id_user";

                    using (SqlCommand cmd = new SqlCommand(queryPengguna, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@nama_lengkap", namaLengkap);
                        cmd.Parameters.AddWithValue("@no_hp", noHp);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@id_user", idUser);
                        cmd.ExecuteNonQuery();
                    }

                    // Cek apakah ada record di PENGUNJUNG
                    string cekQuery = "SELECT COUNT(*) FROM PENGUNJUNG WHERE id_user = @id_user";
                    int jumlah = 0;
                    using (SqlCommand cek = new SqlCommand(cekQuery, conn, transaction))
                    {
                        cek.Parameters.AddWithValue("@id_user", idUser);
                        jumlah = Convert.ToInt32(cek.ExecuteScalar());
                    }

                    if (jumlah > 0)
                    {
                        // Update tabel PENGUNJUNG
                        string queryPengunjung = @"
                            UPDATE PENGUNJUNG
                            SET nama_lengkap = @nama_lengkap,
                                no_hp        = @no_hp,
                                email        = @email,
                                perguruan    = @perguruan,
                                nik          = @nik
                            WHERE id_user = @id_user";

                        using (SqlCommand cmd2 = new SqlCommand(queryPengunjung, conn, transaction))
                        {
                            cmd2.Parameters.AddWithValue("@nama_lengkap", namaLengkap);
                            cmd2.Parameters.AddWithValue("@no_hp", noHp);
                            cmd2.Parameters.AddWithValue("@email", email);
                            cmd2.Parameters.AddWithValue("@perguruan", perguruan);
                            cmd2.Parameters.AddWithValue("@nik", nik);
                            cmd2.Parameters.AddWithValue("@id_user", idUser);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();

                    MessageBox.Show("Data pengguna berhasil diperbarui!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Gagal menyimpan perubahan:\n" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================== TOMBOL BATAL ==================
        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Event handler kosong
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtNamaLengkap_TextChanged(object sender, EventArgs e) { }
        private void txtNoHp_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtPerguruan_TextChanged(object sender, EventArgs e) { }
        private void txtNIK_TextChanged(object sender, EventArgs e) { }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Ambil nilai terkini dari form
            string username = txtUsername.Text.Trim();
            string namaLengkap = txtNamaLengkap.Text.Trim();
            string noHp = txtNoHp.Text.Trim();
            string email = txtEmail.Text.Trim();
            string perguruan = txtPerguruan.Text.Trim();
            string nik = txtNIK.Text.Trim();

            // Validasi
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username tidak boleh kosong.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(namaLengkap))
            {
                MessageBox.Show("Nama lengkap tidak boleh kosong.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamaLengkap.Focus();
                return;
            }

            DialogResult konfirmasi = MessageBox.Show(
                $"Perbarui data pengguna:\n\"{username}\"?",
                "Konfirmasi Update",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi != DialogResult.Yes) return;

            // Panggil method yang sudah ada
            UpdatePengguna(username, namaLengkap, noHp, email, perguruan, nik);
        }
    }
}