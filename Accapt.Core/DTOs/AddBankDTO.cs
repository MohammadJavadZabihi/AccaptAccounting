using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class AddBankDTO
    {
        public string Id { get; set; }

        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankAddress { get; set; }
        public string BankNumber { get; set; }
        public bool HaseCheck { get; set; }
    }
}
