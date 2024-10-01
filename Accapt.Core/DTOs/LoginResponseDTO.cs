using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public LoginStatusDTO LoginStatuce { get; set; }
    }
}
