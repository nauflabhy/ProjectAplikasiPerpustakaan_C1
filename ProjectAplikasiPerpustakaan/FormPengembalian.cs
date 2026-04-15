using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class FormPengembalian : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        // Data yang diterima dari form sebelumnya
        private readonly int idPeminjaman;
        private readonly string kodeBuku;
        private readonly string judulBuku;
        private readonly DateTime tanggalPinjam;
        private readonly DateTime tanggalJatuhTempo;
        private readonly string namaPengguna;

        // Constructor
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


        // ================== BUTTON BATAL ==================
        private void btnBatal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Batalkan pengajuan pengembalian buku?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // ================== BUTTON KEMBALIKAN ==================
        private void btnKembalikan_Click(object sender, EventArgs e)
        {
        
        }

        // ================== EVENT HANDLER LAIN (jika diperlukan) ==================
        private void cmbKondisiBuku_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnKembalikan_Click_1(object sender, EventArgs e)
        {
            if (cmbKondisiBuku.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih kondisi buku terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string kondisi = cmbKondisiBuku.SelectedItem.ToString();
            decimal denda = HitungDendaOtomatis(kondisi);
            string catatan = txtCatatan.Text.Trim();

            // Konfirmasi
            DialogResult konfirm = MessageBox.Show(
                $"Konfirmasi pengembalian buku?\n\n" +
                $"Judul     : {judulBuku}\n" +
                $"Kode      : {kodeBuku}\n" +
                $"Kondisi   : {kondisi}\n" +
                $"Denda     : Rp {denda:N0}\n" +
                $"Catatan   : {catatan}\n\n" +
                "Pengembalian akan langsung diselesaikan.\nLanjutkan?",
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
                        (@id_peminjaman, NULL, GETDATE(), @kondisi, @denda, 'diverifikasi', @catatan);

                        UPDATE PEMINJAMAN 
                        SET status = 'selesai'
                        WHERE id_peminjaman = @id_peminjaman;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);
                        cmd.Parameters.AddWithValue("@kondisi", kondisi);
                        cmd.Parameters.AddWithValue("@denda", denda);
                        cmd.Parameters.AddWithValue("@catatan", string.IsNullOrEmpty(catatan) ? (object)DBNull.Value : catatan);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Pengembalian buku berhasil dilakukan!\n" +
                              "Status peminjaman telah diubah menjadi 'selesai'.",
                              "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat melakukan pengembalian:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBatal_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Batalkan pengajuan pengembalian buku?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void FormPengembalian_Load(object sender, EventArgs e)
        {
            lblKodeBuku.Text = kodeBuku ?? "-";
            lblJudulBuku.Text = judulBuku ?? "-";
            lblTanggalAjuan.Text = DateTime.Now.ToString("dd MMMM yyyy");
            lblTanggalPengembalian.Text = DateTime.Now.ToString("dd MMMM yyyy");

            cmbKondisiBuku.Items.Clear();
            cmbKondisiBuku.Items.AddRange(new string[]
            {
                "baik", "rusak ringan", "rusak berat", "hilang"
            });
            cmbKondisiBuku.SelectedIndex = 0;
        
        }

        private void txtCatatan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}