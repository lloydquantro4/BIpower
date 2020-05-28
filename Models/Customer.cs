using System.ComponentModel.DataAnnotations;

namespace BIpower.Models
{
    public class Customer
    {        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Province { get; set; }

        public string Email {get;set;}
    }
}