using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class Product
    {
        public Product()
        {
            
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int ProductCount { get; set; }

        [Required]
        [MaxLength(800)]
        public string Description { get; set; }
    }
}
