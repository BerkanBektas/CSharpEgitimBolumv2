using System.Data.Entity;
using WindowsFormsAppEntityFrameworkCodeFirst.Entities;

namespace WindowsFormsAppEntityFrameworkCodeFirst.Data
{
    public class DatabaseContext : DbContext // Oluşturduğumuz DatabaseContext sınıfına Entity framework ün  DbContext sınıfından miras alıyoruz
    {
        // Burada veritabanı tablolarını temsil edecek olan Dbset nesnelerimizi yazıyoruz
        public DbSet<Urun> Urunler { get; set; } // Entities klasörümüzdeki class larımız için
        public DbSet<Kategori> Kategoriler { get; set; } // Bu şekilde dbset ler tanımlamamız gerekli!
        // Dbset lerimizi yazdıktan sonra proje içerisindeki App.config isimli dosyaya entity framework ün kullanacağı veritabanını tanımlayan bir connection string kodu yazmamız gerekiyor!
       // public DbSet<Marka> Markalar { get; set; } // Bu db seti eklemezsek veritabanı işlemleri yapamayız
        // Bu aşamadan sonra projeye add new form diyerek markayonetim formu yapmalıyız
    }
}