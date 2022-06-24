using System.ComponentModel.DataAnnotations;

namespace KitapSatis.Models
{
    public class Communication
    {
        [Key]
        public int CommunicationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }


    }
}
