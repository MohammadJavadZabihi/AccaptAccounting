using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.DTOs
{
    public class ShowPepoleDTO
    {
        public IEnumerable<Pepole?> Pepoles { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
