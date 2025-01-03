using Domain.Entities;
using Domain.Models;
using Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class ProductRepository : EFRepository<Product>
    {
        public int TotalRecord = 0;
        
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
            catch { return null; }
        }

        public Product SaveReturnToObject(Product obj)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                context.Product.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace + ex.Source);
            }
            return obj;
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

        public int DeleteById(int Id)
        {
            try
            {
                Product entity = this.GetById(Id);
                if (entity != null)
                    this.Delete(entity);
                return 1;
            }
            catch { return 0; }
        }

        public List<string> DeleteByIds(string ItemId, string returnField = "TEN")
        {
            string[] arrItemID = ItemId.Split(',');
            List<string> lstObjectID = new List<string>();
            List<string> lstObjectName = new List<string>();
            DBContext context = (DBContext)UnitOfWork.Context;
            foreach (string Id in arrItemID)
            {
                try
                {
                    string deleteCommand = string.Format("DELETE FROM MenuNguoiDungs WHERE Id = '{0}'", Id);
                    context.Database.ExecuteSqlRaw(deleteCommand);
                }
                catch (Exception)
                {
                    lstObjectID.Add(Id);
                }
            }
            return lstObjectID;
        }

        public void DeleteByWhereClause(string whereClause)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            string deleteCommand = string.Format("DELETE FROM {0} WHERE {1}", "MenuNguoiDungs", whereClause);
            context.Database.ExecuteSqlRaw(deleteCommand);
        }
    }
}

