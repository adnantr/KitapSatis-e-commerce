using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitapSatis.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Zorunlu bir alan")]
        public string CategoryName { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; } //Çok Çok İlişki

    }
}
