using System.ComponentModel.DataAnnotations;

namespace KitapSatis.Models
{
    public class Admin 
    {
        [Key]
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
