using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Infrastructure.DependencyInjection;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class VaiTroRepository : EFRepository<VaiTro>
    {
        public int TotalRecord { get{ return Total; } }
        public int Total = 0;

        public VaiTroRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public VaiTro GetById(int Id)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.VaiTros.First(dk => dk.Id == Id);
            }
            catch { return new VaiTro(); }
        }
        public void BulkInsert(List<VaiTro> lstEntity, int packageSize = 1000, bool recreateContext = false)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            for (int i = 0; i < lstEntity.Count; i++)
            {
                context.VaiTros.Add(lstEntity[i]);
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


        public void Insert(VaiTro item)
        {
            var context = (DBContext)UnitOfWork.Context;
            context.VaiTros.Add(item);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            VaiTro entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
                this.Save();
            }
        }

        public void Update(VaiTro item)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.VaiTros.Update(item);
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
        public VaiTro_Entity GetEntity(int Id)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.VaiTros
                        where obj.Id == Id
                        select new VaiTro_Entity
                        {
                            Id = obj.Id,
                            TenVaiTro = obj.TenVaiTro,
                            MoTa = obj.MoTa,

                            ThoiGianTao = obj.Created,
                            ThoiGiaSuaCuoi = obj.LastModified,
                            ID_NguoiTao = obj.CreatedBy,
                            ID_NguoiSua = obj.LastModifiedBy,
                        };
            var entity = query.FirstOrDefault();
            if (entity == null) return new VaiTro_Entity();
            return entity;
        }
    }
    
}
