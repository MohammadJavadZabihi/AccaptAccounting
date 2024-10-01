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
    public class EmployeeDeatails
    {
        public EmployeeDeatails()
        {
            
        }

        [Key]
        public int EmployeeDeatailsId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("Epmloyee")]
        [Required]
        public int EpmloyeeId { get; set; }

        [Required]
        [MaxLength(200)]
        public string EmployeeDeatailsName { get; set; }

        [Required]
        [MaxLength(200)]
        public int EmployeeDeatailsCount { get; set; }

        public double PriceOfEmployeDeatails { get; set; }

        #region Realtion

        [JsonIgnore]
        public Epmloyee Epmloyee { get; set; }

        #endregion
    }
}
