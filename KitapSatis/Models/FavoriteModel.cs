using System.Collections.Generic;

namespace KitapSatis.Models
{
    public class FavoriteModel
    {
        public int FavoriteId { get; set; }
        public List<FavoriteItemModel> FavoriteItems { get; set; }
    }
    public class FavoriteItemModel
    {
        public int FavoriteItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
