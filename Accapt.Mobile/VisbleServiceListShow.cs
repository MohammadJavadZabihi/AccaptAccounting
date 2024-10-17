using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Mobile
{
    public class VisbleServiceListShow
    {
        public IEnumerable<VisibleService> VisibleService { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
