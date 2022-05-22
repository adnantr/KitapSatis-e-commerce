using System.ComponentModel.DataAnnotations.Schema;

namespace KitapSatis.Models
{
    public class ProductKind
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Kind")]
        public int KindId { get; set; }

        public Product Product { get; set; }
        public Kind Kind { get; set; }
    }
}
