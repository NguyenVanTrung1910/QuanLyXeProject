using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IVaiTroService
    {
        VaiTro GetById(int id);
        ResponeActionResult Insert(VaiTro item);
        ResponeActionResult Delete(int id);
        ResponeActionResult Update(VaiTro item);
        ResponeActionResult GetPaged(VaiTroQuery query);
        ResponeActionResult DisapprovedItem(int ID);
        ResponeActionResult ApprovedItem(int ID);
        VaiTro_Entity GetEntity(int Id);
        ResponeActionResult GetVaiTroMenuByVaiTroId(int Id);
        ResponeActionResult GetVaiTroQuyenByVaiTroId(int Id);
        ResponeActionResult SavePhanQuyen([FromBody] PhanQuyenRequest request);
        List<ItemTreeView> GetTreeVaiTroTreeView();
    }
}
