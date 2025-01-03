using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Querys;
using Domain.Querys.Base;
using Infrastructure.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class ProductRepository : IProductRepository
    {   
        
        public List<Product_Entity> XemDanhSach()
        {
            var context =(DBContext)UnitOfWork.Context;
            return new List<Product_Entity>();
        }
        public List<Product_Entity> GetPaged(ProductQuery SearchOption)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.Product
                        //where (!SearchOption.isgetBylisID || SearchOption.lstIDGet.Any(id => id == obj.Id))
                        select new Product_Entity
                        {
                            Id = obj.Id
                        };
            return query.GetByGridRequest(SearchOption.oGridRequest, ref TotalRecord).ToList();
        }
    }
}
