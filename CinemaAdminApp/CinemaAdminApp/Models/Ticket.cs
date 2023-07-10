using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShopAdminApplication.Models
{
    public class Ticket 
    {
        public Guid Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductImage { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public double Rating { get; set; }

        
    }
}
