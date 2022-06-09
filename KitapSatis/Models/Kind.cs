using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitapSatis.Models
{
    public class Kind
    {
        [Key]
        public int KindId { get; set; }
        [Required(ErrorMessage = "Zorunlu bir alan")]
        public string KindName { get; set; }


        public ICollection<ProductKind> ProductKinds { get; set; }
    }
}
