using Domain.Entities;
using Domain.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        List<Product_Entity> XemDanhSach();
        List<Product_Entity> TestGetPaged(ProductQuery productQuery);

    }
}
