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
    public class SallaryAndCosts
    {
        public SallaryAndCosts()
        {
            
        }

        [Key]
        public int SallaryAndCostsId { get; set; }

        [ForeignKey("Users")]
        public string UserId { get; set; }

        [MaxLength(200)]
        [Required]
        public string SallaryAndCostsName { get; set; }

        [Required]
        public double PriceOfSallaryAndCosts { get; set; }

        [Required]
        public DateTime DateOfSubmit { get; set; }

        [Required, MaxLength(200)]
        public string DateOfSubmitForShow { get; set; }

        [MaxLength(1000)]
        public string Descriptions { get; set; }


        #region Realtions 

        [JsonIgnore]
        public Users User { get; set; }

        #endregion
    }
}
