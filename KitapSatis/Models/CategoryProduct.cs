using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace KitapSatis.Models
{
    public class CategoryProduct
    {
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Category Category { get; set; }
        public Product Product { get; set; }
    }

}
