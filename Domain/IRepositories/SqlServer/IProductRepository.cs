using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.SqlServer
{
    public interface IProductRepository
    {
        List<Product> XemDanhSach();
    }
}
