using System.ComponentModel.DataAnnotations.Schema;

namespace KitapSatis.Models
{
    public class FavoriteCustomer
    {
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Favorite")]
        public int FavoriteId { get; set; }
        public Customer Customers { get; set; }
        public Favorite Favorites { get; set; }
    }
}
