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
    public class FindeProductServies : IFindeProductServies
    {
        private readonly AccaptFContext _context;
        public FindeProductServies(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<Product?> FindeProduct(string productName, string userId)
        {
            return await _context.products.FirstOrDefaultAsync(p => p.ProductName == productName && p.UserId == userId);
        }

        public async Task<Product?> FindeProduct(int productId, string userId)
        {
            return await _context.products.FirstOrDefaultAsync(p => p.ProductId == productId && p.UserId == userId);
        }

        public async Task<bool> IsExistProduct(ChekIsExistProduct chekIsExistProduct)
        {
            return await _context.products.AnyAsync(p => p.ProductName == chekIsExistProduct.ProductName && p.UserId == chekIsExistProduct.UserId);
        }
    }
}
