using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class ServiceProvider
    {
        public ServiceProvider()
        {
            
        }

        [Key]
        public int ServiceProviderId { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string ProviderName { get; set; }

        [Required]
        [MaxLength(250)]
        public string ProviderPassword { get; set; }

        [Required]
        [MaxLength(50)]
        public string IsCreditor { get; set; }

        [Required]
        [MaxLength(250)]
        public double AmountOfCreditor { get; set; }

        #region Realtions
        public IEnumerable<ProviderServiceList> ProviderServiceLists { get; set; }

        #endregion
    }
}
