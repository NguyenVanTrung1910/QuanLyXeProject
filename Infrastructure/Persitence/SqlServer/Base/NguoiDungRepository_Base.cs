using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Infrastructure.DependencyInjection;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class NguoiDungRepository : EFRepository<NguoiDung>
    {
        public int TotalRecord { get{ return Total; } }
        public int Total = 0;

        public NguoiDungRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public NguoiDung GetById(int Id)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.NguoiDungs.First(dk => dk.Id == Id);
            }
            catch { return new NguoiDung(); }
        }
        public void BulkInsert(List<NguoiDung> lstEntity, int packageSize = 1000, bool recreateContext = false)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            for (int i = 0; i < lstEntity.Count; i++)
            {
                context.NguoiDungs.Add(lstEntity[i]);
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


        public void Insert(NguoiDung item)
        {
            var context = (DBContext)UnitOfWork.Context;
            context.NguoiDungs.Add(item);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            NguoiDung entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
                this.Save();
            }
        }

        public void Update(NguoiDung item)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.NguoiDungs.Update(item);
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
        public NguoiDung_Entity GetEntity(int Id)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.NguoiDungs
                        where obj.Id == Id
                        select new NguoiDung_Entity
                        {
                            Id = obj.Id
                        };
            var entity = query.FirstOrDefault();
            if (entity == null) return new NguoiDung_Entity();
            return entity;
        }
    }
    
}
