using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface INguoiDungService
    {
        NguoiDung GetById(int id);
        ResponeActionResult Insert(NguoiDung item);
        ResponeActionResult Delete(int id);
        ResponeActionResult Update(NguoiDung item);
        ResponeActionResult GetPaged(NguoiDungQuery query);
        ResponeActionResult DisapprovedItem(int ID);
        ResponeActionResult ApprovedItem(int ID);
        NguoiDung_Entity GetEntity(int Id);
        NguoiDung CheckNguoiDungDangNhap(string oTenDangNhap, string pass);
        ResponeActionResult GetVaiTroNguoiDungByNguoiDungId(int Id);
        ResponeActionResult SavePhanVaiTro([FromBody] PhanVaiTroRequest request);
    }
}
