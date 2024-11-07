using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class CreditorDTO
    {
        [Required]
        public int CreditorId { get; set; }
    }
}
