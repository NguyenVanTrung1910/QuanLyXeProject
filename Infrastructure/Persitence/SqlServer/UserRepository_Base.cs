using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Infrastructure.DependencyInjection;

namespace Infrastructure.Persitence.SqlServer
{
    public partial class UserRepository : EFRepository<User>
    {
        public int TotalRecord { get{ return Total; } }
        public int Total = 0;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public User GetById(int Id)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return context.User.First(dk => dk.Id == Id);
            }
            catch { return new User(); }
        }
        public void BulkInsert(List<User> lstEntity, int packageSize = 1000, bool recreateContext = false)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            for (int i = 0; i < lstEntity.Count; i++)
            {
                context.User.Add(lstEntity[i]);
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


        public void Insert(User item)
        {
            var context = (DBContext)UnitOfWork.Context;
            context.User.Add(item);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            User entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
                this.Save();
            }
        }

        public void Update(User item)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            context.User.Update(item);
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
        public User_Entity GetEntity(int Id)
        {
            var context = (DBContext)UnitOfWork.Context;
            var query = from obj in context.User
                        where obj.Id == Id
                        select new User_Entity
                        {
                            Id = obj.Id
                        };
            var entity = query.FirstOrDefault();
            if (entity == null) return new User_Entity();
            return entity;
        }
    }
    
}
