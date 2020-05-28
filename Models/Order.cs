using System;
using System.ComponentModel.DataAnnotations;
namespace BIpower.Models
{
    public class Order
    {
        public int id { get; set; }
        public Customer Customer { get; set;}

        [Required]

        public decimal Total { get; set; }

        public DateTime OrderPlaced { get; set; }

        public DateTime? OrderCompleted { get; set; }
    }
}