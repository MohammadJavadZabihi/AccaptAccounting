using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class AddEmployeeDTO
    {
        [Required]
        [MaxLength(200)]
        public string EpmloyeeName { get; set; }

        [Required]
        [MaxLength(200)]
        public string EmployeeRoll { get; set; }

        [Required]
        [MaxLength(200)]
        public string DateOfRegister{ get; set; }

        [Required]
        [MaxLength(200)]
        public string EmployeeDeatailsName { get; set; }

        [Required]
        [MaxLength(200)]
        public int EmployeeDeatailsCount { get; set; }

        public double PriceOfEmployeDeatails { get; set; }
    }
}
