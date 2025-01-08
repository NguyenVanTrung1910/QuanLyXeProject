using Domain.Entities;
using Domain.Models;
using Domain.Querys;

namespace Domain.IRepositories.SqlServer
{

    public interface IUserRepository
    {
        int TotalRecord { get; }
        User GetById(int id);
        void Insert(User item);
        void DeleteById(int id);
        void Update(User item);
        List<User_Entity> GetPaged(UserQuery query);
        void DisapprovedItem(int ID);
        void ApprovedItem(int ID);
        User_Entity GetEntity(int Id);

    }

}
