
using System;
using System.ComponentModel.DataAnnotations;

namespace KitapSatis.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
