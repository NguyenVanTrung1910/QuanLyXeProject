using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Application.Interfaces
{
    public interface IMenuQuanTriService
    {
        MenuQuanTri GetById(int id);
        ResponeActionResult Insert(MenuQuanTri item);
        ResponeActionResult Delete(int id);
        ResponeActionResult Update(MenuQuanTri item);
        ResponeActionResult GetPaged(MenuQuanTriQuery query);
        ResponeActionResult DisapprovedItem(int ID);
        ResponeActionResult ApprovedItem(int ID);
        MenuQuanTri_Entity GetEntity(int Id);
        List<ItemTreeView> GetTreeMenuTreeView();
        List<TreeMenuQuanTri> GetTreeMenu();
        List<int> GetAllMenuQuanTriIds();
        List<int> GetMenuIdByVaiTroIdAndNguoiDungId(int idNguoiDung, string IdVaiTroString);
    }
}
