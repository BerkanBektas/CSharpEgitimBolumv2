using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppEntityFrameworkCodeFirst.Data;
using WindowsFormsAppEntityFrameworkCodeFirst.Entities;

namespace WindowsFormsAppEntityFrameworkCodeFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DatabaseContext context = new DatabaseContext();// Ef code first'ü kullanabilmek için DatabaseContext sınıfımızdaki bu şekilde bir nesne oluşturmalıyız 
        private void Form1_Load(object sender, EventArgs e)
        {
            dgvUrunler.DataSource = context.Urunler.ToList();//Context nesnemiz üzerindeki urunler isimli dbset üzerinden veritabanındaki kayıtları listeliyoruz 
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {          
            try
            {
                context.Urunler.Add(
                    new Urun
                    {
                       Adi = txtUrunAdi.Text,
                       Fiyat =Convert.ToDecimal(txtUrunFiyati.Text),
                       Stok = Convert.ToInt32(txtStokMiktari.Text)
                    }
                    );
                var sonuc = context.SaveChanges();
                if (sonuc > 0)
                {
                    dgvUrunler.DataSource = context.Urunler.ToList();
                    MessageBox.Show("Kayıt Başarılı");
                }

            }
            catch (Exception hata)
            {

                MessageBox.Show("Kayıt Başarısız"+hata.Message);
            }

        }

        private void dgvUrunler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenKayitId = Convert.ToInt32(dgvUrunler.CurrentRow.Cells[0].Value);
            var kayit = context.Urunler.Find(secilenKayitId);// Entity framework find metodu kendisine parametreyle gönderilen id ile eşleşen kaydı veritabanından getirir.

            txtUrunAdi.Text = kayit.Adi;
            txtStokMiktari.Text = kayit.Stok.ToString();
            txtUrunFiyati.Text = kayit.Fiyat.ToString();

            btnguncelle.Enabled = true;
            btnSil.Enabled = true;
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {

                int secilenKayitId = Convert.ToInt32(dgvUrunler.CurrentRow.Cells[0].Value);
                var kayit = context.Urunler.Find(secilenKayitId);

                kayit.Adi = txtUrunAdi.Text;

                kayit.Fiyat = Convert.ToDecimal(txtUrunFiyati.Text);

                kayit.Stok = Convert.ToInt32(txtStokMiktari.Text);

                var sonuc = context.SaveChanges();

                if (sonuc > 0)

                {
                    dgvUrunler.DataSource = context.Urunler.ToList();
                    MessageBox.Show("kayıt güncellendi");
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

                int secilenKayitId = Convert.ToInt32(dgvUrunler.CurrentRow.Cells[0].Value);

                Urun kayit = context.Urunler.Find(secilenKayitId);

                context.Urunler.Remove(kayit); // context üzerindeki products tablosundan kayit içindeki ürünü silinecek olarak işaretle!

                var sonuc = context.SaveChanges();//context üzerindeki değişikleri (Burada silme işlemine karşılık geliyor)veritabanına işle
                //Entity framework'de tracking denilen bir kavram var ve bu tracking ef context üzerindeki değişiklikleri izler, takip eder, savechanges'i çalıştırdığımzda db'ye işler. 

                if (sonuc > 0)// context.SaveChanges() metodu geriye veritabanından etkilenen kayıt sayısını int olarak bize döndürür. Sonuç değişkenine bu değeri atadık ve if ile bu değer 0dan büyük mü diye kontrol ettik. Eğer silme başarılı ise sonuç değeri 1 olacaktır 0 olacaktır 

                {
                    dgvUrunler.DataSource = context.Urunler.ToList();
                    MessageBox.Show("Kayıt Silindi");
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show("Kayıt silinemedi" + hata.Message);
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            dgvUrunler.DataSource = context.Urunler.Where(u => u.Adi.Contains(txtAra.Text)).ToList();
        }
    }
}
