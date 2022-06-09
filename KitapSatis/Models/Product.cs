using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitapSatis.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Zorunlu bir alan")]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        [Required(ErrorMessage = "Zorunlu bir alan")]
        public int KindId { get; set; }
        [Required(ErrorMessage = "Zorunlu bir alan")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Zorunlu bir alan")]
        public double? Price { get; set; }
        public int? Stock { get; set; }
        public string Picture { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; } //Çok Çok İlişki
        public ICollection<ProductKind> ProductKinds { get; set; } //Çok Çok İlişki
        public ICollection<FavoriteProduct> FavoriteProduct { get; set; } //Çok Çok İlişki
        



    }
}
