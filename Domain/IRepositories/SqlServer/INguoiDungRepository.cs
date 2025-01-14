using Domain.Entities;
using Domain.Models;
using Domain.Querys;

namespace Domain.IRepositories.SqlServer
{

    public interface INguoiDungRepository
    {
        int TotalRecord { get; }
        NguoiDung GetById(int id);
        void Insert(NguoiDung item);
        void DeleteById(int id);
        void Update(NguoiDung item);
        List<NguoiDung_Entity> GetPaged(NguoiDungQuery query);
        void DisapprovedItem(int ID);
        void ApprovedItem(int ID);
        NguoiDung_Entity GetEntity(int Id);
        NguoiDung CheckNguoiDungDangNhap(string oTenDangNhap, string pass);
        List<VaiTroNguoiDung> GetVaiTroNguoiDungByNguoiDungId(int Id);
    }

}
