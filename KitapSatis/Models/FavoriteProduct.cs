using System.ComponentModel.DataAnnotations.Schema;
namespace KitapSatis.Models
{
    public class FavoriteProduct
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Favorite")]
        public int FavoriteId { get; set; }
        public Product Products { get; set; }
        public Favorite Favorites { get; set; }
    }
}
