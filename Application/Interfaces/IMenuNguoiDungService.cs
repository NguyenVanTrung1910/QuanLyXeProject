using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Application.Interfaces
{
    public interface IMenuNguoiDungService
    {
        MenuNguoiDung GetById(int id);
        ResponeActionResult Insert(MenuNguoiDung item);
        ResponeActionResult Delete(int id);
        ResponeActionResult Update(MenuNguoiDung item);
        ResponeActionResult GetPaged(MenuNguoiDungQuery query);
        ResponeActionResult DisapprovedItem(int ID);
        ResponeActionResult ApprovedItem(int ID);
        MenuNguoiDung_Entity GetEntity(int Id);
        List<MenuNguoiDung> GetListByUserId(int userId);
    }
}
