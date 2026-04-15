using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class FormPengembalian : Form
    {
        private readonly string connectionString =
        "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly int idPeminjaman;
        private readonly string kodeBuku;
        private readonly string judulBuku;
        private readonly DateTime tanggalPinjam;
        private readonly DateTime tanggalJatuhTempo;
        private readonly string namaPengguna;  // ditambahkan (meski belum dipakai)

        // Constructor yang baru (sesuai dengan pemanggilan dari KembalikanBuku)
        public FormPengembalian(int idPeminjamanParam, string namaPenggunaParam,
            string kodeBukuParam, string judulBukuParam,
            DateTime tanggalPinjamParam, DateTime tanggalJatuhTempoParam)
        {
            InitializeComponent();

            idPeminjaman = idPeminjamanParam;
            namaPengguna = namaPenggunaParam;
            kodeBuku = kodeBukuParam;
            judulBuku = judulBukuParam;
            tanggalPinjam = tanggalPinjamParam;
            tanggalJatuhTempo = tanggalJatuhTempoParam;

            // idBuku bisa diambil nanti jika diperlukan
        }


        // ================== HITUNG DENDA OTOMATIS ==================
        private decimal HitungDendaOtomatis(string kondisi)
        {
            if (string.IsNullOrEmpty(kondisi)) return 0;

            switch (kondisi.ToLower())
            {
                case "rusak ringan": return 5000;
                case "rusak berat": return 20000;
                case "hilang": return 50000;
                case "baik":
                default: return 0;
            }
        }

        // ================== Event Kosong (Bisa dihapus jika tidak dipakai) ==================
        private void lblKodeBuku_Click(object sender, EventArgs e) { }
        private void lblJudulBuku_Click(object sender, EventArgs e) { }
        private void lblTanggalAjuan_Click(object sender, EventArgs e) { }
        private void cmbKondisiBuku_SelectedIndexChanged(object sender, EventArgs e) { }
        private void btnBatal_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Batalkan pengajuan pengembalian?", "Konfirmasi",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
        private void btnKembalikan_Click(object sender, EventArgs e)
        {
            if (cmbKondisiBuku.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih kondisi buku.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string kondisi = cmbKondisiBuku.SelectedItem.ToString();
            decimal denda = HitungDendaOtomatis(kondisi);

            DialogResult konfirm = MessageBox.Show(
                $"Ajukan pengembalian buku:\n\n" +
                $"Judul     : {judulBuku}\n" +
                $"Kode      : {kodeBuku}\n" +
                $"Kondisi   : {kondisi}\n" +
                $"Denda     : Rp {denda:N2}\n\n" +
                "Status akan menunggu verifikasi admin.\n\n" +
                "Lanjutkan?",
                "Konfirmasi Pengembalian",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO PENGEMBALIAN
                        (id_peminjaman, id_admin, tanggal_kembali, kondisi_buku, denda, status, catatan)
                        VALUES
                        (@id_peminjaman, NULL, GETDATE(), @kondisi, @denda, 'menunggu', @catatan);

                        UPDATE PEMINJAMAN
                        SET status = 'menunggu_kembali' 
                        WHERE id_peminjaman = @id_peminjaman;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);
                        cmd.Parameters.AddWithValue("@kondisi", kondisi);
                        cmd.Parameters.AddWithValue("@denda", denda);
                        cmd.Parameters.AddWithValue("@catatan", txtCatatan?.Text.Trim() ?? "");

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Pengembalian berhasil diajukan!\n" +
                                          "Silakan tunggu verifikasi dari admin.",
                                          "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengajukan pengembalian:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtCatatan_TextChanged(object sender, EventArgs e) { }
        private void lblPengembalian_Click(object sender, EventArgs e) { }

        private void FormPengembalian_Load_1(object sender, EventArgs e)
        {
            // Tampilkan informasi buku di Label
            lblKodeBuku.Text = kodeBuku ?? "-";
            lblJudulBuku.Text = judulBuku ?? "-";

            // ComboBox Kondisi Buku
            cmbKondisiBuku.Items.AddRange(new string[]
            {
                "baik",
                "rusak ringan",
                "rusak berat",
                "hilang"
            });
            cmbKondisiBuku.SelectedIndex = 0;
        }
    }
}