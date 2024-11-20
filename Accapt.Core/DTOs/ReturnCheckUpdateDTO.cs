using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class ReturnCheckUpdateDTO
    {
        public string Version { get; set; }
        public bool IsMandetory { get; set; }
        public string Note { get; set; }
        public string Downnload { get; set; }
    }
}
