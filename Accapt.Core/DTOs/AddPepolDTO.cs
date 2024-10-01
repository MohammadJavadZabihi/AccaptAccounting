using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class AddPepolDTO
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string PepoName { get; set; }

        [Required]
        [MaxLength(200)]
        public string PepoCode { get; set; }

        [Required]
        [MaxLength(250)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string PepoType { get; set; }
    }
}
