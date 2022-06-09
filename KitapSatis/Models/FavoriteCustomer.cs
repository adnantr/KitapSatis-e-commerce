using System.ComponentModel.DataAnnotations.Schema;

namespace KitapSatis.Models
{
    public class FavoriteCustomer
    {
        [ForeignKey("CustomerDetail")]
        public int CustomerDetailId { get; set; }
        [ForeignKey("Favorite")]
        public int FavoriteId { get; set; }
        public CustomerDetail CustomerDetails { get; set; }
        public Favorite Favorites { get; set; }
    }
}
