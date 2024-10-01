using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class DebtorCreditorsService : IDebtorCreditorsService
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        public DebtorCreditorsService(AccaptFContext context,
            IFindUserServies findUserServies)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
        }
        public async Task<AddDebtorCreditorsDTO?> AddDebtorCreditors(AddDebtorCreditorsDTO addDebtorCreditorsDTO)
        {
            var user = await _findUserServies.FindUserById(addDebtorCreditorsDTO.UserId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            DebtorCreditor addDebtorCreditor = new DebtorCreditor
            {
                CustomerName = addDebtorCreditorsDTO.CustomerName,
                DateOfPurchase = addDebtorCreditorsDTO.DateOfPurchase,
                DateOfPurchaseForShow = addDebtorCreditorsDTO.DateOfPurchaseForShow,
                Desctiptions = addDebtorCreditorsDTO.Desctiptions,
                PriceOfDebtorCreditor = addDebtorCreditorsDTO.PriceOfDebtorCreditor,
                Statuce = addDebtorCreditorsDTO.Statuce,
                UserId = addDebtorCreditorsDTO.UserId,
            };

            await _context.DebtorCreditors.AddAsync(addDebtorCreditor);
            await _context.SaveChangesAsync();

            return addDebtorCreditorsDTO;
        }

        public async Task<bool> DeletCreditors(CreditorDTO creditorDTO)
        {
            var debtorCreditors = await _context.DebtorCreditors.FirstOrDefaultAsync(d => d.UserId == creditorDTO.UserId && d.DebtorCreditorID == creditorDTO.CreditorId);

            if (debtorCreditors == null)
                return false;

            _context.DebtorCreditors.Remove(debtorCreditors);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditeCreditors(EditeCreditoDTO creditorDTO)
        {
            var debtorCreditors = await _context.DebtorCreditors.FirstOrDefaultAsync(d => d.UserId == creditorDTO.UserId && d.DebtorCreditorID == creditorDTO.CreditorId);

            if (debtorCreditors == null)
                return false;

            debtorCreditors.CustomerName = creditorDTO.CustomerName;
            debtorCreditors.PriceOfDebtorCreditor = creditorDTO.Price;
            debtorCreditors.Desctiptions = creditorDTO.Descriptions;
            debtorCreditors.Statuce = creditorDTO.Statuce;

            _context.DebtorCreditors.Update(debtorCreditors);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<DebtorCreditor?>> GetAllDebtorCreditors(int pageNumber = 1, int pageSize = 0, string filter = "", string userId = "")
        {
            if (userId == null)
            {
                return null;
            }

            var user = await _findUserServies.FindUserById(userId);

            if (user == null)
            {
                return null;
            }

            if (pageSize == 0)
                pageSize = 8;

            IQueryable<DebtorCreditor> result = _context.DebtorCreditors.AsNoTracking();

            result = result.Where(d => d.UserId == user.Id);

            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(d => d.CustomerName == filter || d.Statuce == filter || d.DateOfPurchaseForShow == filter);

                if (result.Count() <= 0)
                {
                    return null;
                }

                return result;
            }

            var skip = (pageNumber - 1 ) * pageSize;

            return await result.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<DebtorCreditor?> GetSingle(CreditorDTO creditorDTO)
        {
            return await _context.DebtorCreditors.FirstOrDefaultAsync(d => d.UserId == creditorDTO.UserId && d.DebtorCreditorID == creditorDTO.CreditorId);
        }

        public async Task<bool> IsPay(CreditorDTO creditorDTO)
        {
            var debtorCreditors = await _context.DebtorCreditors.FirstOrDefaultAsync(d => d.UserId == creditorDTO.UserId && d.DebtorCreditorID == creditorDTO.CreditorId);

            if (debtorCreditors == null)
                return false;

            debtorCreditors.Statuce = "پرداخت شده";

            return true;
        }
    }
}
