using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Domain.IRepositories.SqlServer
{

    public interface IQuyenSuDungRepository
    {
        int TotalRecord { get; }
        QuyenSuDung GetById(int id);
        void Insert(QuyenSuDung item);
        void DeleteById(int id);
        void Update(QuyenSuDung item);
        List<QuyenSuDung_Entity> GetPaged(QuyenSuDungQuery query);
        void DisapprovedItem(int ID);
        void ApprovedItem(int ID);
        QuyenSuDung_Entity GetEntity(int Id);
        List<ItemTreeView> GetTreeQuyenTreeView();
        List<int> GetAllQuyenCha();
        bool CheckHaveSubItem(int Id);
        List<PhanQuyenNguoiDung> GetListQuyenByUserId(int userId);
        List<VaiTroQuyen> GetListQuyenByVaiTroId(int vaitroId);
        void DeleteByUserId(int userId);
        void AddPhanQuyenNguoiDung(PhanQuyenNguoiDung phanQuyenNguoiDung);

    }

}
