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

        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetupInputRestrictions()
        {
            // Username → Huruf + Angka (tanpa spasi & simbol)
            txtUsername.KeyPress += AllowOnlyAlphanumericNoSpace;

            // Nama Lengkap → Huruf, Angka, Spasi
            txtNamaLengkap.KeyPress += AllowAlphanumericWithSpace;

            // NIK → Hanya Angka
            txtNIK.KeyPress += AllowOnlyNumbers;

            // No HP → Hanya Angka
            txtNoHp.KeyPress += AllowOnlyNumbers;

            // Perguruan → Huruf, Angka, Spasi
            txtPerguruan.KeyPress += AllowAlphanumericWithSpace;

            // Email → Huruf, Angka, @ . _ -
            txtEmail.KeyPress += AllowEmailCharacters;

            // Batasi panjang maksimal
            txtUsername.MaxLength = 50;
            txtNamaLengkap.MaxLength = 100;
            txtNIK.MaxLength = 20;
            txtNoHp.MaxLength = 15;
            txtEmail.MaxLength = 80;
            txtPerguruan.MaxLength = 100;
        }

        private void AllowOnlyAlphanumericNoSpace(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void AllowAlphanumericWithSpace(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '\b')
                e.Handled = true;
        }

        private void AllowOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void AllowEmailCharacters(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                "@._-".IndexOf(e.KeyChar) == -1 &&
                e.KeyChar != '\b')
                e.Handled = true;
        }

        private void EditPengguna_Load_1(object sender, EventArgs e)
        {
            AmbilDataPengguna();
            SetupInputRestrictions();
        }
    }
}