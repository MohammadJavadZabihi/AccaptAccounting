using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Entities
{
    public class Users
    {
        public Users()
        {
            
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        [MaxLength(200)]
        public string RealFullName { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        [Required]
        [MaxLength(500)]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string VerifyCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int Role { get; set; }

        [Required]
        public DateTime ExpireAccessDate { get; set; }


        #region Realations

        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<InvoiceDetails> InvoiceDetails { get; set; }
        public IEnumerable<Pepole> Pepoles { get; set; }
        public IEnumerable<BankT> Banks { get; set; }
        public IEnumerable<Chek> Cheks { get; set; }
        public IEnumerable<Billan> Billans { get; set; }
        public IEnumerable<DebtorCreditor> DebtorCreditors { get; set; }
        public IEnumerable<Epmloyee> Employees { get; set; }
        public IEnumerable<EmployeeDeatails> EmployeeDeatails { get; set; }
        public IEnumerable<SallaryAndCosts> SallaryAndCosts { get; set; }
        public IEnumerable<ProviderServiceList> ProviderServiceLists { get; set; }
        public IEnumerable<ServiceProvider> ServiceProviders { get; set; }

        #endregion
    }
}
