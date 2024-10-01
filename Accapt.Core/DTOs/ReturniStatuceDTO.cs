using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class ReturniStatuceDTO
    {
        public bool ISuucess { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; }
    }
}
