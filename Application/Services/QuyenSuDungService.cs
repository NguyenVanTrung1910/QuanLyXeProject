using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Application.Services
{
    public class QuyenSuDungService : IQuyenSuDungService
    {
        private IQuyenSuDungRepository _QuyenSuDungRepository;
        private readonly ResponeActionResult _responeActionResult;
        public int TotalRecord = 0;

        public QuyenSuDungService(ResponeActionResult responeActionResult,IQuyenSuDungRepository QuyenSuDungRepository) 
        {
            _QuyenSuDungRepository = QuyenSuDungRepository;
            _responeActionResult = responeActionResult;
        }

        public QuyenSuDung GetById(int id)
        {
            return _QuyenSuDungRepository.GetById(id);
        }
        public ResponeActionResult ApprovedItem(int id)
        {
            _QuyenSuDungRepository.ApprovedItem(id);
            _responeActionResult.ex_message = "Duyệt thành công";
            return _responeActionResult;
        }
        public ResponeActionResult DisapprovedItem(int id)
        {
            _QuyenSuDungRepository.DisapprovedItem(id);
            _responeActionResult.ex_message = "Hủy duyệt thành công";
            return _responeActionResult;
        }


        public ResponeActionResult GetPaged(QuyenSuDungQuery searchQuery)
        {
           
            List<QuyenSuDung_Entity> listTaiLieuThucTap = _QuyenSuDungRepository.GetPaged(searchQuery);
            if (listTaiLieuThucTap == null)
            {
                _responeActionResult.Message($"Không tìm thấy bản ghi");
                return _responeActionResult;
            }
            _responeActionResult.data = listTaiLieuThucTap;
            _responeActionResult.draw = searchQuery.draw;
            _responeActionResult.recordsTotal = _QuyenSuDungRepository.TotalRecord;
            _responeActionResult.recordsFiltered = _QuyenSuDungRepository.TotalRecord;
            return _responeActionResult;
        }

        public ResponeActionResult Insert(QuyenSuDung item)
        {
            item.FillDataForInsert(item.CurrentUserId);
            _QuyenSuDungRepository.Insert(item);
            _responeActionResult.ex_message = "Thêm thành công";
            _responeActionResult.data = new QuyenSuDung() { Id = item.Id };
            return _responeActionResult;
        }


        public ResponeActionResult Delete(int id)
        {
            _QuyenSuDungRepository.DeleteById(id);
            _responeActionResult.ex_message = "Xóa thành công";
            return _responeActionResult;
        }

        public ResponeActionResult Update(QuyenSuDung item)
        {
            item.FillDataForUpdate(item.CurrentUserId);
            _QuyenSuDungRepository.Update(item);
            _responeActionResult.ex_message = "Sửa thành công";
            _responeActionResult.data = new QuyenSuDung() { Id = item.Id };
            return _responeActionResult;
        }
        public QuyenSuDung_Entity GetEntity(int Id)
        {
            return _QuyenSuDungRepository.GetEntity(Id);
        }
        public List<ItemTreeView> GetTreeQuyenTreeView()
        {
            return _QuyenSuDungRepository.GetTreeQuyenTreeView();
        }
        public List<int> GetAllQuyenCha()
        {
            return _QuyenSuDungRepository.GetAllQuyenCha();
        }
        public List<int> GetQuyenIdByVaiTroIdAndNguoiDungId(int idNguoiDung, string IdVaiTroString)
        {
            List<int> result = new List<int>();
            List<int> listIdVaiTro = IdVaiTroString != null ? IdVaiTroString.Split(',').Select(int.Parse).ToList() : [];
            if (_QuyenSuDungRepository.GetListQuyenByUserId(idNguoiDung) != null)
            {
                result = _QuyenSuDungRepository.GetListQuyenByUserId(idNguoiDung).Select(a => a.QuyenId).ToList();
            }


            for (int i = 0; i < listIdVaiTro.Count; i++)
            {
                var listMenu = _QuyenSuDungRepository.GetListQuyenByVaiTroId(listIdVaiTro[i]).Select(a => a.QuyenSuDungId).ToList();
                for (int j = 0; j < listMenu.Count; j++)
                {
                    result.Add(listMenu[j]);
                }

            }
            result = result.Distinct().ToList();
            return result;
        }


    }
}
