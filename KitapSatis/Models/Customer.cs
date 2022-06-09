using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitapSatis.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [PersonalData]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}")]
        [PersonalData]
        public string Email { get; set; }
        public string FirtName { get; set; }
        public string LastName { get; set; }
    }
}
