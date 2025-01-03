using Domain.Entities;
using Domain.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.SqlServer
{
    public interface IProductRepository
    {
        List<Product_Entity> XemDanhSach();
        List<Product_Entity> GetPaged(ProductQuery productQuery);
       
    }
}
