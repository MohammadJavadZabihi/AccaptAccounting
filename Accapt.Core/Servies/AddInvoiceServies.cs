﻿using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class AddInvoiceServies : IAddInvoiceServies
    {
        private readonly AccaptFContext _context;
        private readonly IProductServies _productServies;
        private readonly IFindPepolServies _findPepolServies;
        private readonly UserManager<IdentityUser> _userManager;
        public AddInvoiceServies(AccaptFContext context, 
            IProductServies productServies, 
            IFindPepolServies findPepolServies,
            UserManager<IdentityUser> userManager)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _productServies = productServies ?? throw new ArgumentException(nameof(productServies));
            _findPepolServies = findPepolServies ?? throw new ArgumentException(nameof(findPepolServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }
        public async Task<AddInvoicesDTO?> AddInvoice(AddInvoicesDTO invoice, string userId)
        {
            try
            {
                if (invoice == null)
                    throw new ArgumentNullException(nameof(invoice));

                Invoice addInvoice = new Invoice()
                {
                    AmountPaid = invoice.AmountPaid,
                    CreditorStatuce = invoice.CreditorStatuce,
                    DateOfSubmitInvoice = invoice.DateOfSubmitInvoice,
                    Description = invoice.Description,
                    Id = userId,
                    InvoiceName = invoice.InvoiceName,
                    TotalPrice = invoice.TotalPrice,
                    TypeOfInvoice = invoice.TypeOfInvoice,
                    NextDateVisit = invoice.NextDateVisit,
                };

                await _context.Invoices.AddAsync(addInvoice);
                await _context.SaveChangesAsync();

                bool isSell = false;
                if (addInvoice.TypeOfInvoice == "فاکتور فروش")
                {
                    isSell = true;
                }

                var user = await _userManager.FindByIdAsync(userId);

                foreach (var item in invoice.InvoiceDetails)
                {
                    InvoiceDetails invoiceDetails = new InvoiceDetails
                    {
                        Discount = (int)item.Discount,
                        Id = userId,
                        InvoiceId = addInvoice.InvoiceId,
                        Invoices = addInvoice,
                        ProductCount = (int)item.ProductCount,
                        ProductName = item.ProductName,
                        ProductPrice = (int)item.ProductPrice,
                        ProductTotalPrice = (int)item.ProductTotalPrice,
                    };

                    if (isSell)
                    {
                        var product = await _productServies.decrisesProductCount(invoiceDetails.ProductCount, invoiceDetails.ProductName
                                      ,userId);

                        if(!product)
                        {
                            throw new Exception("مغایرت در تعداد درخواستی محصول با تعداد محصول در انبار");
                        }
                    }
                    else
                    {
                        var product = await _productServies.AddProductCount(invoiceDetails.ProductCount, invoiceDetails.ProductName
                                      ,userId);

                        if(!product)
                        {
                            AddProductDTO addProductDTO = new AddProductDTO
                            {
                                CatrgoryId = 0,
                                Description = "ندارد",
                                Price = invoiceDetails.ProductPrice,
                                ProductCount = invoiceDetails.ProductCount,
                                Productname = invoiceDetails.ProductName,
                            };

                            var addProduc = await _productServies.AddProduct(addProductDTO, user.Id);
                        }
                    }

                    await _context.InvoiceDetails.AddAsync(invoiceDetails);
                }

                if (invoice.AmountPaid < invoice.TotalPrice)
                {
                    DebtorCreditor debtorCreditor = new DebtorCreditor();

                    var totalAmount = invoice.TotalPrice - invoice.AmountPaid;

                    debtorCreditor.DateOfPurchase = invoice.DateOfSubmitInvoice;
                    debtorCreditor.PriceOfDebtorCreditor = Convert.ToDouble(totalAmount);
                    debtorCreditor.CustomerName = invoice.PepolName;
                    debtorCreditor.DateOfPurchaseForShow = invoice.DateOfSubmitInvoice.ToShortDateString();
                    debtorCreditor.Desctiptions = "ندارد";
                    debtorCreditor.Statuce = "بدهکار";
                    debtorCreditor.UserId = userId;

                    await _context.DebtorCreditors.AddAsync(debtorCreditor);
                }
                else if (invoice.AmountPaid > invoice.TotalPrice)
                {
                    DebtorCreditor debtorCreditor = new DebtorCreditor();

                    var totalAmount = invoice.AmountPaid - invoice.TotalPrice;

                    debtorCreditor.DateOfPurchase = invoice.DateOfSubmitInvoice;
                    debtorCreditor.PriceOfDebtorCreditor = Convert.ToDouble(totalAmount);
                    debtorCreditor.CustomerName = invoice.PepolName;
                    debtorCreditor.DateOfPurchaseForShow = invoice.DateOfSubmitInvoice.ToShortDateString();
                    debtorCreditor.Desctiptions = "ندارد";
                    debtorCreditor.Statuce = "بستانکار";
                    debtorCreditor.UserId = userId;


                    await _context.DebtorCreditors.AddAsync(debtorCreditor);
                }
                await _context.SaveChangesAsync();

                await _context.SaveChangesAsync();



                return invoice;
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex.Message));
            }

        }
    }
}
