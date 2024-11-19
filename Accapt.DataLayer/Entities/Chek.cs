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
    public class Chek
    {
        public Chek()
        {
            
        }

        [Key]
        public int CheckId { get; set; }

        [Required]
        [MaxLength(300)]
        public string CheckNumber { get; set; }

        public string ChekcBankName { get; set; }

        [Required]
        public decimal ChekPrice { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        public DateTime CurrentDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string CurrentDateSearch { get; set; }

        [Required]
        [MaxLength(100)]
        public string DueDateSearch { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatuceOfPaid { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeOfChek { get; set; }

        #region Realations

        public BankT Bank { get; set; }

        #endregion
    }
}
