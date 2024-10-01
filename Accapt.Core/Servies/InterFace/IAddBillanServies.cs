using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IAddBillanServies
    {
        Task<BillanSummaryDTO?> AddBillan(string startBillan, string endBillan, string userId);

        Task<IEnumerable<InvoiceSummary>> GetIncomingForBillan(string userId, string currentDate, string endDate);
    }
}
