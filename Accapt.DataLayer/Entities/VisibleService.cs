using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class VisibleService
    {
        public VisibleService()
        {
            
        }

        [Key]
        public int VisibleServiceId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public string Id { get; set; }

        [Required]
        [ForeignKey("ProviderServiceList")]
        public int ProviderWorkId { get; set; }

        [Required]
        [MaxLength(200)]
        public string SrviceName { get; set; }

        [Required]
        [MaxLength(800)]
        public string Address { get; set; }

        [Required]
        [MaxLength(200)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(800)]
        public string Descriptions { get; set; }

        [Required]
        [MaxLength(200)]
        public string Statuce { get; set; }

        [Required]
        [MaxLength(200)]
        public string DateOfService { get; set; }

        #region Realtions

        public Users User { get; set; }
        public ProviderServiceList ProviderServiceList { get; set; }

        #endregion

    }
}
