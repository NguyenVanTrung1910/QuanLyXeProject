using Domain.Entities;
using Infrastructure.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.SqlServer
{
    public class ProductRepository : IProductRepository
    {   
        private readonly DBContext db;
        public ProductRepository(DBContext dBContext) {
            db = dBContext;
        }
        public List<Product> XemDanhSach()
        {
            return db.Product.ToList();
        }
    }
}
