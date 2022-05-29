using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitapSatis.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int KindId { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Picture { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; } //Çok Çok İlişki
        public ICollection<ProductKind> ProductKinds { get; set; } //Çok Çok İlişki
        public ICollection<FavoriteProduct> FavoriteProduct { get; set; } //Çok Çok İlişki
        public List<WeekProduct> WeekProduct { get; set; }



    }
}
