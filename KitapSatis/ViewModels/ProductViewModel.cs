using KitapSatis.Models;
using System.Collections.Generic;

namespace KitapSatis.ViewModels
{
    public class ProductViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
