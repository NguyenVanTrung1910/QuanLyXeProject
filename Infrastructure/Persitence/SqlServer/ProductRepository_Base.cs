using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class ProductRepository : EFRepository<Product>
    {
        public int TotalRecord
        {
            get
            {
                return Total;
            }
        }
        public int Total = 0;

        public ProductRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public Product GetById(int Id)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.Product.First(dk => dk.Id == Id);
            }
            catch { return new Product(); }
        }
        public void BulkInsert(List<Product> lstEntity, int packageSize = 1000, bool recreateContext = false)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            for (int i = 0; i < lstEntity.Count; i++)
            {
                context.Product.Add(lstEntity[i]);
                if (i > 0 && i % packageSize == 0)
                {
                    context.SaveChanges();
                    if (recreateContext)
                    {
                        context.Dispose();
                        context = new DBContext();
                    }
                }
            }
            context.SaveChanges();
        }


        public void Insert(Product item)
        {
            var context = (DBContext)UnitOfWork.Context;
            context.Product.Add(item);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            Product entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
                this.Save();
            }

        }

        public void Update(Product item)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.Product.Update(item);
            context.SaveChanges();
            
        }


        public void DisapprovedItem(int Id)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            var item = this.GetById(Id);
            item.ModerationStatus = ModerationStatus.Pending;
            context.SaveChanges();
        }

        public void ApprovedItem(int Id)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            var item = this.GetById(Id);
            item.ModerationStatus = ModerationStatus.Approved;
            context.SaveChanges();
        }
        public Product_Entity GetEntity(int Id)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.Product
                        where obj.Id == Id
                        select new Product_Entity
                        {
                            Id = obj.Id,
                            Name = obj.Name,
                            CreatedByText = "trung",
                            LastModifiedByText = "11/11/2000"
                        };
            var entity = query.FirstOrDefault();
            if (entity == null) return new Product_Entity();
            return entity;
        }
    }
}

