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
        public int ProductId { get; set; }

        public Customer Customer { get; set; }//bire çok

    }
}
