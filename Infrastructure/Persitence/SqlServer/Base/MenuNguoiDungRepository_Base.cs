using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Infrastructure.DependencyInjection;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class MenuNguoiDungRepository : EFRepository<MenuNguoiDung>
    {
        public int TotalRecord { get{ return Total; } }
        public int Total = 0;

        public MenuNguoiDungRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public MenuNguoiDung GetById(int Id)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.MenuNguoiDungs.First(dk => dk.Id == Id);
            }
            catch { return new MenuNguoiDung(); }
        }
        public void BulkInsert(List<MenuNguoiDung> lstEntity, int packageSize = 1000, bool recreateContext = false)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            for (int i = 0; i < lstEntity.Count; i++)
            {
                context.MenuNguoiDungs.Add(lstEntity[i]);
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


        public void Insert(MenuNguoiDung item)
        {
            var context = (DBContext)UnitOfWork.Context;
            context.MenuNguoiDungs.Add(item);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            MenuNguoiDung entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
                this.Save();
            }
        }

        public void Update(MenuNguoiDung item)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.MenuNguoiDungs.Update(item);
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
        public MenuNguoiDung_Entity GetEntity(int Id)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.MenuNguoiDungs
                        where obj.Id == Id
                        select new MenuNguoiDung_Entity
                        {
                            Id = obj.Id
                        };
            var entity = query.FirstOrDefault();
            if (entity == null) return new MenuNguoiDung_Entity();
            return entity;
        }
    }
    
}
