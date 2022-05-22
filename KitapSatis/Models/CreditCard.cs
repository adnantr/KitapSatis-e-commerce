using System.ComponentModel.DataAnnotations;

namespace KitapSatis.Models
{
    public class CreditCard
    {
        [Key]
        public int CreditCardId { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardNo { get; set; }
        public string CardExpMo { get; set; }
        public string CardExpYr { get; set; }
        


        public Customer Customer { get; set; }//Bire bir
    }
}
