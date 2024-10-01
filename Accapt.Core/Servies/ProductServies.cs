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
    public class ProductServies : IProductServies
    {
        private readonly AccaptFContext _context;
        private readonly IFindUserServies _findUserServies;
        private readonly IFindeProductServies _findeProductServies;
        public ProductServies(AccaptFContext context,
            IFindUserServies findUserServies,
            IFindeProductServies findeProductServies)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _findUserServies = findUserServies ?? throw new ArgumentException(nameof(findUserServies));
            _findeProductServies = findeProductServies;

        }
        public async Task<object?> AddProduct(AddProductDTO addProduct)
        {
            if (addProduct == null)
                return "Null Exeption";

            try
            {
                var user = await _findUserServies.FindUserByUserName(addProduct.UserName);

                if (user == null)
                    return "Cannot find the Main User";

                Product product = new Product
                {
                    ProductName = addProduct.Productname,
                    Description = addProduct.Description,
                    Price = addProduct.Price,
                    ProductCount = addProduct.ProductCount,
                    UserId = user.Id,
                    Users = user
                };

                await _context.AddAsync(product);
                await _context.SaveChangesAsync();




                return product;
            }
            catch (Exception ex)
            {
                return "Error Message is : " + ex.Message;
            }
        }

        public async Task<bool> AddProductCount(int productCount, string productName, string userId)
        {
            if (productCount <= 0)
                throw new ArgumentException("تعداد نا متعبر");

            var product = await _findeProductServies.FindeProduct(productName, userId);

            if (product == null)
                return false;

            product.ProductCount += productCount;

            _context.products.Update(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> decrisesProductCount(int productCount, string productName, string userId)
        {
            if (productCount <= 0)
                throw new ArgumentException("تعداد نا متعبر");

            var product = await _findeProductServies.FindeProduct(productName, userId);

            if (product == null)
                return false;

            product.ProductCount -= productCount;

            if (product.ProductCount < 0)
                return false;

            _context.products.Update(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletProduct(Product product)
        {
            if (product == null)
                return false;

            try
            {
                _context.products.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Product?>> GetProducts(int pageNumber = 1, int pageSize = 0,
            string filter = "", string userId = "")
        {
            try
            {
                var user = await _findUserServies.FindUserById(userId);

                if (user == null)
                    return null;

                if (pageSize == 0)
                    pageSize = 8;

                IQueryable<Product> products = _context.products.AsNoTracking();

                products = products.Where(p => p.UserId == userId);

                if (!string.IsNullOrEmpty(filter))
                {
                    products = products.Where(p => p.ProductName.Contains(filter));

                    if (products.Count() == 0)
                        return null;

                    return products;
                }

                var skip = (pageNumber - 1) * pageSize;

                return await products.Skip(skip).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
