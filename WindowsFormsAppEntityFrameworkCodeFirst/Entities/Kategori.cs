

using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsAppEntityFrameworkCodeFirst.Entities
{
    [Table("Kategoriler")]// Entity framework'ün veritabanı tablosunu kategoris ismi yerine kategoriler olarak oluşturması için bu attribute'u yazıyoruz
    public class Kategori
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public bool Durum { get; set; }

    }
}
