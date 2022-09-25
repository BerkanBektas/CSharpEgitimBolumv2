

using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsAppEntityFrameworkCodeFirst.Entities
{
    [Table("Urunler")] //Veritabanı tablosunun ismi ürünler olsun uruns olmasın 
    public class Urun
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public decimal Fiyat { get; set; }
        public int Stok { get; set; }
    }
}
