using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class AddBillanServies : IAddBillanServies
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        private readonly IFindInvoices _findInvoices;
        public AddBillanServies(AccaptFContext context,
            IFindUserServies findUserServies,
            IFindInvoices findInvoices)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
        }
        public async Task<BillanSummaryDTO?> AddBillan(string startBillan, string endBillan, string userId)
        {
            try
            {
                if (startBillan == null || endBillan == null)
                    throw new ArgumentNullException();

                var user = await _findUserServies.FindUserById(userId);

                if (user == null)
                    throw new Exception("Null user");

                var sellInvoices = await _context.Invoices.Where(i => i.DateOfSubmitInvoice >= Convert.ToDateTime(startBillan) && i.DateOfSubmitInvoice <= Convert.ToDateTime(endBillan)
                                && i.Id == userId && i.TypeOfInvoice == "فاکتور فروش").Select(i => new InvoiceForBillanSUmmaryDTO
                                {
                                    Date = i.DateOfSubmitInvoice.ToShortDateString(),
                                    Name = i.InvoiceName,
                                    Price = Convert.ToDouble(i.TotalPrice),
                                    AmountPaide = Convert.ToDouble(i.AmountPaid),

                                }).ToListAsync();

                var buyInvoices = await _context.Invoices.Where(i => i.DateOfSubmitInvoice >= Convert.ToDateTime(startBillan) && i.DateOfSubmitInvoice <= Convert.ToDateTime(endBillan)
                                  && i.Id == userId && i.TypeOfInvoice == "فاکتور خرید").
                                  Select(i => new InvoiceForBillanSUmmaryDTO
                                  {
                                      Price = Convert.ToDouble(i.TotalPrice),
                                      Date = i.DateOfSubmitInvoice.ToShortDateString(),
                                      Name = i.InvoiceName,
                                      AmountPaide = Convert.ToDouble(i.AmountPaid)

                                  }).ToListAsync();

                var sallaryAndCosts = await _context.SallaryAndCosts.Where(s => s.UserId == userId
                && s.DateOfSubmit >= Convert.ToDateTime(startBillan) && s.DateOfSubmit <= Convert.ToDateTime(endBillan))
                    .Select(i => new SallryAndCostForBillanSummary
                    {
                        Date = i.DateOfSubmitForShow,
                        Name = i.SallaryAndCostsName,
                        Price = i.PriceOfSallaryAndCosts

                    }).ToListAsync();

                double plusPrice = 0;
                double negetivPrice = 0;
                double totalPrice = 0;

                foreach (var item in sellInvoices)
                {
                    var price = item.Price;
                    if(price == item.AmountPaide)
                    {
                        plusPrice += price;
                    }
                    else
                    {
                        price -= item.AmountPaide;
                        plusPrice += price;
                    }
                }

                foreach (var item in buyInvoices)
                {
                    var price = item.Price;
                    negetivPrice += Convert.ToDouble(price);
                }

                foreach (var item in sallaryAndCosts)
                {
                    var price = item.Price;
                    negetivPrice += Convert.ToDouble(price);
                }

                totalPrice = plusPrice - negetivPrice;

                BillanSummaryDTO billanSummaryDTO = new BillanSummaryDTO
                {
                    NegetiveInvoices = buyInvoices,
                    NegetivePrice = negetivPrice,
                    TotalPrice = totalPrice,
                    PlusInvoices = sellInvoices,
                    PlusPrice = plusPrice,
                    SallaryAndCosts = sallaryAndCosts
                };

                Billan billan = new Billan
                {
                    EndDate = Convert.ToDateTime(endBillan),
                    EndDateShow = endBillan,
                    Id = userId,
                    NegtiveBillan = Convert.ToDecimal(negetivPrice),
                    PlusBillan = Convert.ToDecimal(plusPrice),
                    StartDate = Convert.ToDateTime(startBillan),
                    StartDateShow = startBillan,
                    TotoalBillan = Convert.ToDecimal(totalPrice)
                };

                await _context.Billans.AddAsync(billan);
                await _context.SaveChangesAsync();


                return billanSummaryDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<InvoiceSummary>> GetIncomingForBillan(string userId, string currentDate, string endDate)
        {
            if(userId == null)
                throw new ArgumentNullException("userId");

            var user = await _findUserServies.FindUserById(userId);

            if (user == null)
                throw new Exception("Access Denied");

            var invoiceInvoiming = await _context.Invoices.Where(i => i.DateOfSubmitInvoice >= Convert.ToDateTime(currentDate)
            && i.DateOfSubmitInvoice <= Convert.ToDateTime(endDate) && i.Id == userId && i.TypeOfInvoice == "فاکتور فروش").GroupBy(i => new
            {
                Month = i.DateOfSubmitInvoice.Month,
                Year = i.DateOfSubmitInvoice.Year,
                Day = i.DateOfSubmitInvoice.Day
            }).Select(g => new InvoiceSummary
            {
                DateOfSubmit = $"{g.Key.Year}/{g.Key.Month.ToString("D2")}/{g.Key.Day}",
                InvoicePrice = g.Sum(i => Convert.ToDouble(i.TotalPrice)),
            }).ToListAsync();

            return invoiceInvoiming;
        }
    }
}
