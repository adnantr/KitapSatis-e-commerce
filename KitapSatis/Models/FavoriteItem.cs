using System.ComponentModel.DataAnnotations;

namespace KitapSatis.Models
{
    public class FavoriteItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int FavoriteId { get; set; }
        public Favorite Favorite { get; set; }
    }
}
