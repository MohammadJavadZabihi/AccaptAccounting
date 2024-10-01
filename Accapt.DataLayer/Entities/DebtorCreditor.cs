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
    public class DebtorCreditor
    {
        public DebtorCreditor()
        {
            
        }

        [Key]
        public int DebtorCreditorID { get; set; }

        [Required]
        [ForeignKey("Users")]
        public string UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CustomerName { get; set; }

        [Required]
        public double PriceOfDebtorCreditor { get; set; }

        [Required]
        [MaxLength(200)]
        public string Statuce { get; set; }

        [Required]
        public DateTime DateOfPurchase { get; set; }

        [Required]
        [MaxLength(200)]
        public string DateOfPurchaseForShow { get; set; }

        [MaxLength(800)]
        public string Desctiptions { get; set; }

        #region Relations

        [JsonIgnore]
        public Users User { get; set; }  

        #endregion
    }
}
