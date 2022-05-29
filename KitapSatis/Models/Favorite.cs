using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitapSatis.Models
{
    public class Favorite
    {
        [Key]
        public int FavoriteId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public ICollection<FavoriteProduct> FavoriteProduct { get; set; }//çok çok
        public ICollection<FavoriteCustomer> FavoriteCustomer { get; set; }//çok çok

    }
}
