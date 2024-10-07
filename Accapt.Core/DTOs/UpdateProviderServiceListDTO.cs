using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class UpdateProviderServiceListDTO
    {
        public string Address { get; set; }
        public string UserId { get; set; }
        public string ServiceName { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
        public string IsDone { get; set; }
    }
}
