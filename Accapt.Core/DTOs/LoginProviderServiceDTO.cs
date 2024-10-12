using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class LoginProviderServiceDTO
    {
        public string UserId { get; set; }
        public int ServiceProviderId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
