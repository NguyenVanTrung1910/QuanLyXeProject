using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Application.Interfaces
{
    public interface IUserService
    {
        User GetById(int id);
        ResponeActionResult Insert(User item);
        ResponeActionResult Delete(int id);
        ResponeActionResult Update(User item);
        ResponeActionResult GetPaged(UserQuery query);
        ResponeActionResult DisapprovedItem(int ID);
        ResponeActionResult ApprovedItem(int ID);
        User_Entity GetEntity(int Id);
    }
}
