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
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        public string FirtName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("Communication")]
        public int CommunicationId { get; set; }
        [ForeignKey("CreditCard")]
        public int CreditCardId { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryCountry { get; set; }

        public CreditCard CreditCard { get; set; } //Bire bir
        public Communication Communication { get; set; } //Bire bir
        public List<Favorite> Favorite { get; set; } //Bire çok
        //public Favorite Favorite { get; set; }
        //public Order Order { get; set; }
    }
}
