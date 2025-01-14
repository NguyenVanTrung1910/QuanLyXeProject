using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Infrastructure.DependencyInjection;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class QuyenSuDungRepository : EFRepository<QuyenSuDung>
    {
        public int TotalRecord { get{ return Total; } }
        public int Total = 0;

        public QuyenSuDungRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public QuyenSuDung GetById(int Id)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.QuyenSuDungs.First(dk => dk.Id == Id);
            }
            catch { return new QuyenSuDung(); }
        }
        public void BulkInsert(List<QuyenSuDung> lstEntity, int packageSize = 1000, bool recreateContext = false)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            for (int i = 0; i < lstEntity.Count; i++)
            {
                context.QuyenSuDungs.Add(lstEntity[i]);
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


        public void Insert(QuyenSuDung item)
        {
            var context = (DBContext)UnitOfWork.Context;
            context.QuyenSuDungs.Add(item);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            QuyenSuDung entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
                this.Save();
            }
        }

        public void Update(QuyenSuDung item)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.QuyenSuDungs.Update(item);
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
        public QuyenSuDung_Entity GetEntity(int Id)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.QuyenSuDungs
                        where obj.Id == Id
                        select new QuyenSuDung_Entity
                        {
                            Id = obj.Id
                        };
            var entity = query.FirstOrDefault();
            if (entity == null) return new QuyenSuDung_Entity();
            return entity;
        }
    }
    
}
