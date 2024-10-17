using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Accapt.Mobile
{
    public class VisibleService
    {
        public VisibleService()
        {

        }

        public int VisibleServiceId { get; set; }

        public string Id { get; set; }

        public int ProviderWorkId { get; set; }

        public string SrviceName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Descriptions { get; set; }

        public string Statuce { get; set; }

        public string DateOfService { get; set; }
    }
}