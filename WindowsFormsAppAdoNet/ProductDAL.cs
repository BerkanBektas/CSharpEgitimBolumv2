using System;
using System.Collections.Generic;
using System.Linq;
using System.Data; // Veritabanı işlemleri için gerekli
using System.Data.SqlClient; // Adonet kütüphaneleri

namespace WindowsFormsAppAdoNet
{
    public class ProductDAL
    {
        SqlConnection connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB; database=UrunYonetimiAdoNet; integrated security=true");// SqlConnection veritabanına bağlanmak için kullandığımız ado net sınıfıdır. Parametre olarak kendisine verilen bilgilerdeki veritabanına bağlanır.
        void ConnectionKontrol() // void = tek
        {
            if (connection.State == ConnectionState.Closed) // Eğer yukarda tanımladığımız veritabanı bağlantısı kapalıysa
            {
                connection.Open();// bağlantıyı aç
            }
        }
        public List<Product> GetAll() // Bu metodun geri dönüş değeri List<Product> yani ürün listesidir
        {
            ConnectionKontrol(); // metot çalıştığı anda bağlantıyı kontrol et
            List<Product> urunListesi = new List<Product>(); // geriye döndüreceğimiz List<Product> nesnesini oluşturduk

            SqlCommand command = new SqlCommand("select * from Products", connection); // SqlCommand sql komutlarını çalıştırabilmemizi sağlayan ado net sınıfı. Tırnaklar içerisine sql komutumuzu, sonraki parametrede de bu komutun çalıştırılacağı connection nesnesini belirtiyoruz.
            SqlDataReader reader = command.ExecuteReader(); // SqlDataReader Sql veri okuyucu sınıfıdır, bu sınıfa üstteki command nesnesini ExecuteReader metoduyla çalıştırmasını söyledik.

            while (reader.Read()) // reader db de okuyacak kayıt bulduğu sürece
            {
                Product product = new Product() // Döngü her döndüğünde içi boş yeni ürün oluşturuyoruz
                {
                    //Aşağıda veritabanından gelen verilerle ürün bilgilerini dolduruyoruz
                    Id = Convert.ToInt32(reader["Id"]),
                    UrunAdi = reader["UrunAdi"].ToString(),
                    StokMiktari = Convert.ToInt32(reader["StokMiktari"]),
                    UrunFiyati = Convert.ToDecimal(reader["UrunFiyati"])
                };
                urunListesi.Add(product); // İçi doldurulan product nesnesini yukarda oluşturduğumuz products listesine ekliyoruz.
            }
            reader.Close(); // Veri okuyucuyu kapat
            command.Dispose(); // sql komut nesnesini kapat
            connection.Close(); // veritabanı bağlantısını kapat
            return urunListesi;
        }
        public DataTable GetAllDataTable()
        {
            ConnectionKontrol(); // bağlantıyı kontrol et
            DataTable dt = new DataTable(); // Boş bir datatable nesnesi oluştur
            SqlCommand command = new SqlCommand("select * from Products", connection);
            SqlDataReader reader = command.ExecuteReader();
            dt.Load(reader); // dt tablosuna reader ile veritabanından okunan verileri yükle
            reader.Close(); // Veri okuyucuyu kapat
            command.Dispose(); // sql komut nesnesini kapat
            connection.Close();// veritabanı bağlantısını kapat
            return dt; // metodun çağırıldığı yere dt(data tablosunu) gönder.
        }
        public int Add(Product product)
        {
            ConnectionKontrol();
            SqlCommand command = new SqlCommand("Insert into Products (UrunAdi, UrunFiyati, StokMiktari) values (@UrunAdi, @UrunFiyati, @Stok)", connection); // Sql komutu olarak bu sefer insert komutu yazdık
            command.Parameters.AddWithValue("@UrunAdi", product.UrunAdi);
            command.Parameters.AddWithValue("@UrunFiyati", product.UrunFiyati);
            command.Parameters.AddWithValue("@Stok", product.StokMiktari);
            int islemSonucu = command.ExecuteNonQuery(); // ExecuteNonQuery metodu geriye veritabanında etkilenen kayıt sayısını döner
            command.Dispose(); // sql komut nesnesini kapat
            connection.Close();// veritabanı bağlantısını kapat
            return islemSonucu; // Metodumuz geriye int döndüğü için islemSonucu değişkenini geri gönüyoruz
        }
        public Product GetProduct(string id)
        {
            ConnectionKontrol();
            SqlCommand command = new SqlCommand("select * from Products where Id =  " + id, connection);
            SqlDataReader reader = command.ExecuteReader();
            Product product = new Product();

            while (reader.Read())
            {
                product.Id = Convert.ToInt32(reader["Id"]);
                product.UrunAdi = reader["UrunAdi"].ToString();
                product.StokMiktari = Convert.ToInt32(reader["StokMiktari"]);
                product.UrunFiyati = Convert.ToDecimal(reader["UrunFiyati"]);
            }
            reader.Close(); // Veri okuyucuyu kapat
            command.Dispose(); // sql komut nesnesini kapat
            connection.Close(); // veritabanı bağlantısını kapat
            return product;
        }
        public int Update(Product product)
        {
            ConnectionKontrol();
            SqlCommand command = new SqlCommand("Update Products set UrunAdi = @UrunAdi, UrunFiyati = @UrunFiyati , StokMiktari = @Stok where Id = @UrunId", connection); // Sql komutu olarak bu sefer Update komutu yazdık
            command.Parameters.AddWithValue("@UrunAdi", product.UrunAdi);
            command.Parameters.AddWithValue("@UrunFiyati", product.UrunFiyati);
            command.Parameters.AddWithValue("@Stok", product.StokMiktari);
            command.Parameters.AddWithValue("@UrunId", product.Id);
            int islemSonucu = command.ExecuteNonQuery(); // ExecuteNonQuery metodu geriye veritabanında etkilenen kayıt sayısını döner
            command.Dispose(); // sql komut nesnesini kapat
            connection.Close();// veritabanı bağlantısını kapat
            return islemSonucu; // Metodumuz geriye int döndüğü için islemSonucu değişkenini geri gönüyoruz
        }
        
        public int Delete(string id)
        {
            ConnectionKontrol();
            SqlCommand command = new SqlCommand("Delete from Products  where Id = @UrunId", connection); // Sql komutu olarak bu sefer Update komutu yazdık
            command.Parameters.AddWithValue("@UrunId",id); 
            int islemSonucu = command.ExecuteNonQuery(); // ExecuteNonQuery metodu geriye veritabanında etkilenen kayıt sayısını döner
            command.Dispose(); // sql komut nesnesini kapat
            connection.Close();// veritabanı bağlantısını kapat
            return islemSonucu; // Metodumuz geriye int döndüğü için islemSonucu değişkenini geri gönüyoruz
        }

    }
}
