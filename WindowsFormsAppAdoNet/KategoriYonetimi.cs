using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppAdoNet
{
    public partial class KategoriYonetimi : Form
    {
        public KategoriYonetimi()
        {
            InitializeComponent();
        }

        KategoriDAL kategoriDAL = new KategoriDAL();
        private void dgvKategoriler_Load(object sender, EventArgs e)
        {

            dgvKategoriler.DataSource = kategoriDAL.GetAllDataTable();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                int sonuc = kategoriDAL.Add(new Kategori
                {
                    KategoriAdi = txtKategoriAdi.Text,
                    Durum = cbDurum.Checked
                });
                if (sonuc > 0)
                {
                    MessageBox.Show("Kayıt Başarılı!");
                    dgvKategoriler.DataSource = kategoriDAL.GetAllDataTable();
                }
                else MessageBox.Show("Kayıt Başarısız!");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void dgvKategoriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dgvKategoriler.CurrentRow.Cells[0].Value.ToString();

            var kategori = kategoriDAL.GetKategori(id);// kategoriDal içine yazdığımız get metoduna seçili satırdan aldığımız id değerini yolladık, o da bize bu id'ye ait kategori bilgilerini veritabanından çekip getirecek.

            txtKategoriAdi.Text = kategori.KategoriAdi;//Ön yüzündeki txtKategoriAdi adlı textbox'a veritabanından gelen kategorninin kategori adı bilgisini yükledik.

            cbDurum.Checked = kategori.Durum;//Aynı şekilde ön yüzdeki cbdurum isimli checkbox'ın değerini de kategoriden gelen geğere göre işaretledik.
            btnGuncelle.Enabled = true;
            btnSil.Enabled = true;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {

                var islemSonucu = kategoriDAL.Delete(dgvKategoriler.CurrentRow.Cells[0].Value.ToString());
                if (islemSonucu > 0)
                {
                    dgvKategoriler.DataSource = kategoriDAL.GetAllDataTable();
                    MessageBox.Show("Silme Başarılı");
                }
                else MessageBox.Show("Silme Başarısız");
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }
        }
        /*  private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int sonuc = kategoriDAL.Update(new Kategori
                {
                    KategoriAdi = txtKategoriAdi.Text,
                    Durum = cbDurum.Checked,
                    Id = (int)dgvKategoriler.CurrentRow.Cells[0].Value
                });
                if (sonuc > 0)
                {
                    dgvKategoriler.DataSource = kategoriDAL.GetAllDataTable();
                    MessageBox.Show("Kayıt Başarılı!");
                  
                }
                else MessageBox.Show("Kayıt Başarısız!");
            }
            catch (Exception)
{
    MessageBox.Show("Hata Oluştu!");
}
        */
           private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                Kategori kategori = new Kategori(); // Boş bir product nesnesi oluşturduk
                kategori.KategoriAdi = txtKategoriAdi.Text;
               kategori.Durum = Convert.ToBoolean(cbDurum.Checked);
                kategori.Id = Convert.ToInt32(dgvKategoriler.CurrentRow.Cells[0].Value);
               
                var islemSonucu = kategoriDAL.Update(kategori);
                if (islemSonucu > 0)
                {
                    dgvKategoriler.DataSource = kategoriDAL.GetAllDataTable(); // Data grid view da eklenen son kaydı da görebilmek için
                    MessageBox.Show("Kayıt Başarılı");
                }
                else MessageBox.Show("Güncelleme Başarısız!");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata");
            }
        }
    }
}
