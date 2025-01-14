using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Domain.IRepositories.SqlServer
{

    public interface IVaiTroRepository
    {
        int TotalRecord { get; }
        VaiTro GetById(int id);
        void Insert(VaiTro item);
        void DeleteById(int id);
        void Update(VaiTro item);
        List<VaiTro_Entity> GetPaged(VaiTroQuery query);
        void DisapprovedItem(int ID);
        void ApprovedItem(int ID);
        VaiTro_Entity GetEntity(int Id);
        List<VaiTroMenu> GetVaiTroMenuByVaiTroId(int VaiTroId);
        List<VaiTroQuyen> GetVaiTroQuyenByVaiTroId(int Id);
        int DeleteQuyenByVaiTroId(int id);
        int DeleteMenuByVaiTroId(int VaiTroId);
        void AddQuyenForVaiTro(VaiTroQuyen vaiTroQuyen);
        void AddMenuForVaiTro(VaiTroMenu vaiTroMenu);
        List<VaiTroMenu> GetMenuByVaiTroId(int VaiTroId);
        List<ItemTreeView> GetTreeVaiTroTreeView();
        void DeleteByUserId(int userId);
        void AddVaiTroNguoiDung(VaiTroNguoiDung vaiTroNguoiDung);
    }

}
