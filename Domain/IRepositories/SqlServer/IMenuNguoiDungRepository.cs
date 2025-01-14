using Domain.Entities;
using Domain.Models;
using Domain.Querys;

namespace Domain.IRepositories.SqlServer
{

    public interface IMenuNguoiDungRepository
    {
        int TotalRecord { get; }
        MenuNguoiDung GetById(int id);
        void Insert(MenuNguoiDung item);
        void DeleteById(int id);
        void Update(MenuNguoiDung item);
        List<MenuNguoiDung_Entity> GetPaged(MenuNguoiDungQuery query);
        void DisapprovedItem(int ID);
        void ApprovedItem(int ID);
        MenuNguoiDung_Entity GetEntity(int Id);
        List<MenuNguoiDung> GetListByUserId(int userId);
        void DeleteByUserId(int userId);
        void AddMenuNguoiDung(MenuNguoiDung menuNguoiDung);
    }

}
