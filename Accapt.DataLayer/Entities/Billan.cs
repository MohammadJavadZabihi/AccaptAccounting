using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class Billan
    {
        public Billan()
        {
            
        }

        [Key]
        public int BillanId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public string Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string StartDateShow { get; set; }

        [Required]
        [MaxLength(100)]
        public string EndDateShow { get; set; }

        [Required] 
        public decimal PlusBillan { get; set; }

        [Required]
        public decimal NegtiveBillan { get; set; }

        [Required]
        public decimal TotoalBillan { get; set; }

        #region realtions

        public Users Users { get; set; }

        #endregion
    }
}
