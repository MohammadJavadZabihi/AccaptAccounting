using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class BankT
    {
        public BankT()
        {
            
        }

        [Key]
        public int BankId { get; set; }

        [Required]
        [MaxLength(200)]
        public string BankName { get; set; }

        [Required]
        [MaxLength(200)]
        public string BankBranch { get; set; }

        [MaxLength(800)]
        public string BankAddress { get; set; }

        [Required]
        [MaxLength(800)]
        public string BankNumber { get; set; }

        [Required]
        public bool HaseCheck { get; set; }

        [Required]
        public string Id { get; set; }

        #region Realations
        public IEnumerable<Chek> Cheks { get; set; }

        #endregion
    }
}
