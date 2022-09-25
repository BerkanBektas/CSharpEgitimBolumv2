using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppEntityFrameworkCodeFirst.Data;
using WindowsFormsAppEntityFrameworkCodeFirst.Entities;

namespace WindowsFormsAppEntityFrameworkCodeFirst
{

    public partial class KategoriYonetimi : Form
    {
        public KategoriYonetimi()
        {
            InitializeComponent();
        }
        DatabaseContext context = new DatabaseContext();
        private void KategoriYonetimi_Load(object sender, EventArgs e)
        {
            dgvKategoriler.DataSource = context.Kategoriler.ToList();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtKategoriAdi.Text))
            {
                MessageBox.Show("Kategori Adı Boş Geçilemez!");
                return;
            }
            try
            {
                Kategori kategori = new Kategori()
                {
                    Adi = txtKategoriAdi.Text,
                    Durum = cbDurum.Checked
                };

                context.Kategoriler.Add(kategori);

                context.SaveChanges();

                dgvKategoriler.DataSource = context.Kategoriler.ToList();

                MessageBox.Show("Kayıt Başarılı!");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }

        }

        private void dgvKategoriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtKategoriAdi.Text = dgvKategoriler.CurrentRow.Cells[1].Value.ToString();
                cbDurum.Checked = Convert.ToBoolean(dgvKategoriler.CurrentRow.Cells[2].Value);

                btnGuncelle.Enabled = true;
                btnSil.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvKategoriler.CurrentRow.Cells[0].Value);
                Kategori kategori = context.Kategoriler.FirstOrDefault(k => k.Id == id); // FirstOrDefault metodu kendisine gönderilen soruya ait kaydı veritabanından bulur
                kategori.Adi = txtKategoriAdi.Text;
                kategori.Durum = cbDurum.Checked;

                context.SaveChanges();

                dgvKategoriler.DataSource = context.Kategoriler.ToList();

                MessageBox.Show("Kayıt Başarılı!");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvKategoriler.CurrentRow.Cells[0].Value);
                Kategori kategori = context.Kategoriler.SingleOrDefault(k => k.Id == id); // SingleOrDefault metodu veritabanındaki 1 kaydı getirmek için kullanılır, eğer şarta uyan 1 den fazla kayıt bulursa hata verir!
                context.Kategoriler.Remove(kategori);
                var islemSonucu = context.SaveChanges();
                if (islemSonucu > 0)
                {
                        dgvKategoriler.DataSource = context.Kategoriler.ToList();
                    MessageBox.Show("Kayıt Silindi!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu tekrar deneyiz!");
            }

        }

     
    }
}
