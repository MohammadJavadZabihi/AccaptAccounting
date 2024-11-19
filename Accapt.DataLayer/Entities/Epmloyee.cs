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
    public class Epmloyee
    {
        public Epmloyee()
        {
            
        }

        [Key]
        public int EpmloyeeId { get; set; }

        [Required]
        [MaxLength(200)]
        public string EpmloyeeName { get; set; }

        public string UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string EmployeeRoll { get; set; }

        [Required]
        public DateTime DateOfRegister { get; set; }

        [Required]
        [MaxLength(200)]
        public string DateOfRegisterShow { get; set; }

        #region relations

        [JsonIgnore]
        public IEnumerable<EmployeeDeatails> EmployeeDeatails { get; set; }

        #endregion
    }
}
