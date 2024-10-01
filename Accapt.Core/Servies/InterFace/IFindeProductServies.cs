using Accapt.Core.DTOs;
using Accapt.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies.InterFace
{
    public interface IFindeProductServies
    {
        Task<Product?> FindeProduct(string productName, string userId);
        Task<Product?> FindeProduct(int productId, string userId);
        Task<bool> IsExistProduct(ChekIsExistProduct chekIsExistProduct);
    }
}
