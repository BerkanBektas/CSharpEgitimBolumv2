using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppEntityFrameworkDbFirst
{
    public partial class KategoriYonetimi : Form
    {
        public KategoriYonetimi()
        {
            InitializeComponent();
        }

        UrunYonetimiAdoNetEntities context = new UrunYonetimiAdoNetEntities();
        
        private void KategoriYonetimi_Load(object sender, EventArgs e)
        {
            dgvKategoriler.DataSource = context.Kategoriler.ToList();
        }

        private void dgvKategoriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenKayitId = Convert.ToInt32(dgvKategoriler.CurrentRow.Cells[0].Value);
            var kayit = context.Kategoriler.Find(secilenKayitId);
           
            txtKategoriAdi.Text = kayit.KategoriAdi.ToString();
            
            cbDurum.Checked = Convert.ToBoolean(kayit.Durum);
            

            btnGuncelle.Enabled = true;
            btnSil.Enabled = true;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKategoriAdi.Text))
            {
                MessageBox.Show("Kategori Adı Boş Geçilemez");
                return;
            }
            try
            {

                context.Kategoriler.Add(new Kategoriler
                {
                    Durum = cbDurum.Checked,
                    KategoriAdi = txtKategoriAdi.Text,
                });

                var sonuc = context.SaveChanges();
                if (sonuc>0)
                {
                    dgvKategoriler.DataSource = context.Kategoriler.ToList();
                    MessageBox.Show("Kayıt Başarılı");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Hata oluştu");
            }
           
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int secilenKayitId = Convert.ToInt32(dgvKategoriler.CurrentRow.Cells[0].Value);
                var kayit = context.Kategoriler.Find(secilenKayitId);

                kayit.Durum = cbDurum.Checked;
                kayit.KategoriAdi = txtKategoriAdi.Text;
                var sonuc = context.SaveChanges();

                if (sonuc > 0)

                {
                    dgvKategoriler.DataSource = context.Kategoriler.ToList();
                    MessageBox.Show("Kayıt güncellendi");
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show("kayıt güncellenemedi" + hata.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                int secilenKayitId = Convert.ToInt32(dgvKategoriler.CurrentRow.Cells[0].Value);
               
                var kayit = context.Kategoriler.Find(secilenKayitId);
               
                context.Kategoriler.Remove(kayit);

                var sonuc = context.SaveChanges();

                if (sonuc > 0)

                {
                    dgvKategoriler.DataSource = context.Kategoriler.ToList();
                    MessageBox.Show("Kayıt Silindi");
                }

            }
            catch (Exception hata)
            {

                MessageBox.Show("kayıt Silinemedi" + hata.Message);
            }

        }
    }
}
