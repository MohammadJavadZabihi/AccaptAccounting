using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class ChekUpdateDTO
    {
        public string ChekNumber { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal ChekPrice { get; set; }
        public bool IsPaide { get; set; }
    }
}
