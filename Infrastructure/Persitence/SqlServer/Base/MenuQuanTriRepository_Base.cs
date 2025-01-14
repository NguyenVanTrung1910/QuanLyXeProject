using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Infrastructure.DependencyInjection;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class MenuQuanTriRepository : EFRepository<MenuQuanTri>
    {
        public int TotalRecord { get{ return Total; } }
        public int Total = 0;

        public MenuQuanTriRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public MenuQuanTri GetById(int Id)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.MenuQuanTris.First(dk => dk.Id == Id);
            }
            catch { return new MenuQuanTri(); }
        }
        public void BulkInsert(List<MenuQuanTri> lstEntity, int packageSize = 1000, bool recreateContext = false)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            for (int i = 0; i < lstEntity.Count; i++)
            {
                context.MenuQuanTris.Add(lstEntity[i]);
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


        public void Insert(MenuQuanTri item)
        {
            var context = (DBContext)UnitOfWork.Context;
            context.MenuQuanTris.Add(item);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            MenuQuanTri entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
                this.Save();
            }
        }

        public void Update(MenuQuanTri item)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.MenuQuanTris.Update(item);
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
    }
    
}
