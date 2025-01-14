using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Domain.IRepositories.SqlServer
{

    public interface IMenuQuanTriRepository
    {
        int TotalRecord { get; }
        MenuQuanTri GetById(int id);
        void Insert(MenuQuanTri item);
        void DeleteById(int id);
        void Update(MenuQuanTri item);
        List<MenuQuanTri_Entity> GetPaged(MenuQuanTriQuery query);
        void DisapprovedItem(int ID);
        void ApprovedItem(int ID);
        MenuQuanTri_Entity GetEntity(int Id);
        List<ItemTreeView> GetTreeMenuTreeView();
        List<TreeMenuQuanTri> GetTreeMenu();
        List<int> GetAllMenuQuanTriIds();
        bool CheckHaveSubItem(int Id);
    }

}
