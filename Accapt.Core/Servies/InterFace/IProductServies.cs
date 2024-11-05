using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IProductServies
    {
        Task<object?> AddProduct(AddProductDTO addProduct, string userId);
        Task<bool> DeletProduct(Product product);
        Task<bool> AddProductCount(int productCount, string productName, string userId);
        Task<bool> decrisesProductCount(int productCount, string productName, string userId);
        Task<IEnumerable<Product?>> GetProducts(int pageNumber = 1, int pageSize = 0, string filter = "", string userId = "");
        Task Save();
    }
}
