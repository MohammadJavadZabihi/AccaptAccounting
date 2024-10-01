using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class SingleChekDTO
    {
        public int CheckId { get; set; }

        public string CheckNumber { get; set; }

        public string ChekcBankName { get; set; }

        public decimal ChekPrice { get; set; }

        public string CurrentDateSearch { get; set; }

        public string DueDateSearch { get; set; }

        public string StatuceOfPaid { get; set; }

        public string TypeOfChek { get; set; }
    }
}
