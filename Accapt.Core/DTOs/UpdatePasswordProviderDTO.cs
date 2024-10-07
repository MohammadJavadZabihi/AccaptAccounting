using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class UpdatePasswordProviderDTO
    {
        public int providerId { get; set; }
        public string userId { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
