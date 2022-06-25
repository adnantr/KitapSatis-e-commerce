using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitapSatis.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<FavoriteItem> FavoriteItems { get; set; }
    }
}
