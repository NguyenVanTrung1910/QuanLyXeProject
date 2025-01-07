using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories.SqlServer
{
    public interface IProductRepository
    {
        int TotalRecord { get; }
        Product GetById(int id);
        void Insert(Product item);
        void DeleteById(int id);
        void Update(Product item);
        List<Product_Entity> GetPaged(ProductQuery query);
        void DisapprovedItem(int ID);
        void ApprovedItem(int ID);
        Product_Entity GetEntity(int Id);

    }
}
