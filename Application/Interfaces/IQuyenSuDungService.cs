using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Application.Interfaces
{
    public interface IQuyenSuDungService
    {
        QuyenSuDung GetById(int id);
        ResponeActionResult Insert(QuyenSuDung item);
        ResponeActionResult Delete(int id);
        ResponeActionResult Update(QuyenSuDung item);
        ResponeActionResult GetPaged(QuyenSuDungQuery query);
        ResponeActionResult DisapprovedItem(int ID);
        ResponeActionResult ApprovedItem(int ID);
        QuyenSuDung_Entity GetEntity(int Id);
        List<ItemTreeView> GetTreeQuyenTreeView();
        List<int> GetAllQuyenCha();
        List<int> GetQuyenIdByVaiTroIdAndNguoiDungId(int idNguoiDung, string IdVaiTroString);
    }
}
