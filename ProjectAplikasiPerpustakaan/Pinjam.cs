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
    public partial class Pinjam : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly int idBuku;
        private readonly string kodeBuku;
        private readonly string judulBuku;
        private readonly int? idUser;

        // Constructor dengan parameter (dipanggil dari form CariBuku)
        public Pinjam(int idBuku, string kodeBuku, string judulBuku, int? idUser = null)
        {
            InitializeComponent();
            this.idBuku = idBuku;
            this.kodeBuku = kodeBuku;
            this.judulBuku = judulBuku;
            this.idUser = idUser;
        }

        private void Pinjam_Load(object sender, EventArgs e)
        {
            // Tampilkan data buku di Label
            lblKodeBuku.Text = kodeBuku ?? "Kode tidak tersedia";
            lblJudulBuku.Text = judulBuku ?? "Judul tidak tersedia";

            // ============== BATASI INPUT PADA SETIAP TEXTBOX ==============
            txtNIK.KeyPress += AllowOnlyNumbers_KeyPress;
            txtNIK.MaxLength = 20;

            txtNamaLengkap.KeyPress += AllowOnlyAlphanumeric_KeyPress;
            txtNamaLengkap.MaxLength = 100;

            txtNoHp.KeyPress += AllowOnlyNumbers_KeyPress;
            txtNoHp.MaxLength = 15;

            txtEmail.KeyPress += AllowEmailCharacters_KeyPress;
            txtEmail.MaxLength = 80;

            txtPerguruan.KeyPress += AllowOnlyAlphanumeric_KeyPress;
            txtPerguruan.MaxLength = 100;
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ================== PEMBATAS INPUT ==================
        private void AllowOnlyAlphanumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void AllowOnlyNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void AllowEmailCharacters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                "@._-".IndexOf(e.KeyChar) == -1 &&
                e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void btnAjukanPeminjaman_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNIK.Text) ||
                string.IsNullOrWhiteSpace(txtNamaLengkap.Text))
            {
                MessageBox.Show("NIK dan Nama Lengkap wajib diisi!",
                    "Validasi Gagal",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_AjukanPeminjamanLengkap", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_user",
                            (object)this.idUser ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@nik", txtNIK.Text.Trim());
                        cmd.Parameters.AddWithValue("@nama_lengkap", txtNamaLengkap.Text.Trim());
                        cmd.Parameters.AddWithValue("@no_hp", txtNoHp.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@perguruan", txtPerguruan.Text.Trim());
                        cmd.Parameters.AddWithValue("@id_buku", this.idBuku);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            "✅ Peminjaman berhasil diajukan!\nStatus: Menunggu Persetujuan Admin.",
                            "Berhasil",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Terjadi kesalahan:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}