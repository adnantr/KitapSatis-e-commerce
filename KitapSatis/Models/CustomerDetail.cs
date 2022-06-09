using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitapSatis.Models
{
    public class CustomerDetail
    {
        [Key]
        public int CustomerDetailId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Communication")]
        public int CommunicationId { get; set; }
        [ForeignKey("CreditCard")]
        public int CreditCardId { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryCountry { get; set; }


        public Customer Customer { get; set; }//Bire bir
        public CreditCard CreditCard { get; set; }//Bire bir
        public Communication Communication { get; set; } //Bire bir
        public ICollection<FavoriteCustomer> FavoriteCustomer { get; set; }//çok çok
    }
}
