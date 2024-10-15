using Accapt.DataLayer.Entities;

namespace Accapt.Core.DTOs
{
    public class ShowProviderServiceList
    {
        public IEnumerable<ProviderServiceList?> ProvidersList { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
