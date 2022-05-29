using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitapSatis.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; } //Çok Çok İlişki

    }
}
