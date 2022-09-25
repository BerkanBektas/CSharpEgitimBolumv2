using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppEntityFrameworkDbFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Entity Framework
        /*
         * Entity Framework ORM(Object Relational Mapping) araçlarından biridir. Veritabanı CRUD işlemlerini Sql sorgusu yazmadan Linq dili ile hazır metotları kullanarak yapabilmemizi sağlar.
         * * Entity Framework ile 4 farklı yöntem kullanarak proje geliştirebilirsiniz
         * 
         * Model First(Model oluşturup bu modele göre db oluşturarak)
         * Database First (Var olan Veritabanını Kullanma)
         * Code First(Önce Entity class'larımızı oluşturup sonraveritabanı oluşturarak)
         * Code First(Var olan Veritabanını kullanma Entitiy class'larını oluşturarak)
        */
        //Entity Framework projelere dahili olarak ado.net gibi gelmez!
        //Sonradan projeye sağ tıklayıp açılan menüden nuget package manager'i açıp buradan browse menüsüne tıklayıp arama çubuğundan entity framework yazarak paketi bulup install diyerek açılan onay penceresinde I accept butonuna basarak  yüklememiz gerekir 

        UrunYonetimiAdoNetEntities context = new UrunYonetimiAdoNetEntities();//Enitty framework ile veritabanı crud işlemlerini yapabilmek için bu sınıftan bir nesne tanımlıyoruz.
        private void Form1_Load(object sender, EventArgs e)
        {
            dgvUrunler.DataSource = context.Products.ToList();// EF ile contex nesnesi üzerindeki Products dbset'ine ulaşıp veritabanındaki ürünleri listeledik 
        }

        private void Ekle_Click(object sender, EventArgs e)
        {
            try
            {
                context.Products.Add(new Products
                {
                    StokMiktari = Convert.ToInt32(txtStokMiktari.Text),
                    UrunAdi = txtUrunAdi.Text,
                    UrunFiyati = Convert.ToDecimal(txtUrunFiyati.Text)
                });// Yukarıda db set üzerine yeni bir ürün kaydı ekledik
                context.SaveChanges();//Burada ise context üzerinde yapılan bu değişikliği veritabanına kaydettik 
                dgvUrunler.DataSource = context.Products.ToList();
                MessageBox.Show("Kayıt başarılı");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu");

            }
        }

        private void dgvUrunler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenKayitId = Convert.ToInt32(dgvUrunler.CurrentRow.Cells[0].Value);
            var kayit = context.Products.Find(secilenKayitId);// Entity framework find metodu kendisine parametreyle gönderilen id ile eşleşen kaydı veritabanından getirir.

            txtUrunAdi.Text = kayit.UrunAdi;
            txtStokMiktari.Text = kayit.StokMiktari.ToString();
            txtUrunFiyati.Text = kayit.UrunFiyati.ToString();

            btnguncelle.Enabled = true;
            btnSil.Enabled = true;
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {

                int secilenKayitId = Convert.ToInt32(dgvUrunler.CurrentRow.Cells[0].Value);
                var kayit = context.Products.Find(secilenKayitId);

                kayit.UrunAdi = txtUrunAdi.Text;

                kayit.UrunFiyati = Convert.ToDecimal(txtUrunFiyati.Text);

                kayit.StokMiktari = Convert.ToInt32(txtStokMiktari.Text);

                var sonuc = context.SaveChanges();

                if (sonuc > 0)

                {
                    dgvUrunler.DataSource = context.Products.ToList();
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

                Products kayit = context.Products.Find(secilenKayitId);

                context.Products.Remove(kayit); // context üzerindeki products tablosundan kayit içindeki ürünü silinecek olarak işaretle!

                var sonuc = context.SaveChanges();//context üzerindeki değişikleri (Burada silme işlemine karşılık geliyor)veritabanına işle
                //Entity framework'de tracking denilen bir kavram var ve bu tracking ef context üzerindeki değişiklikleri izler, takip eder, savechanges'i çalıştırdığımzda db'ye işler. 

                if (sonuc > 0)// context.SaveChanges() metodu geriye veritabanından etkilenen kayıt sayısını int olarak bize döndürür. Sonuç değişkenine bu değeri atadık ve if ile bu değer 0dan büyük mü diye kontrol ettik. Eğer silme başarılı ise sonuç değeri 1 olacaktır 0 olacaktır 

                {
                    dgvUrunler.DataSource = context.Products.ToList();
                    MessageBox.Show("Kayıt Silindi");
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show("Kayıt silinemedi" + hata.Message);
            }

        }
    }
}
