using System.ComponentModel.DataAnnotations;

namespace KitapSatis.Models
{
    public class RoleModel
    {
        [Required]
        public string Name { get; set; }
    }
}
