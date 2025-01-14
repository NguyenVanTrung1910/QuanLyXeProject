using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Application.Services
{
    public class MenuNguoiDungService : IMenuNguoiDungService
    {
        private IMenuNguoiDungRepository _MenuNguoiDungRepository;
        private readonly ResponeActionResult _responeActionResult;
        public int TotalRecord = 0;

        public MenuNguoiDungService(ResponeActionResult responeActionResult,IMenuNguoiDungRepository MenuNguoiDungRepository) 
        {
            _MenuNguoiDungRepository = MenuNguoiDungRepository;
            _responeActionResult = responeActionResult;
        }

        public MenuNguoiDung GetById(int id)
        {
            return _MenuNguoiDungRepository.GetById(id);
        }
        public ResponeActionResult ApprovedItem(int id)
        {
            _MenuNguoiDungRepository.ApprovedItem(id);
            _responeActionResult.ex_message = "Duyệt thành công";
            return _responeActionResult;
        }
        public ResponeActionResult DisapprovedItem(int id)
        {
            _MenuNguoiDungRepository.DisapprovedItem(id);
            _responeActionResult.ex_message = "Hủy duyệt thành công";
            return _responeActionResult;
        }


        public ResponeActionResult GetPaged(MenuNguoiDungQuery searchQuery)
        {
           
            List<MenuNguoiDung_Entity> listTaiLieuThucTap = _MenuNguoiDungRepository.GetPaged(searchQuery);
            if (listTaiLieuThucTap == null)
            {
                _responeActionResult.Message($"Không tìm thấy bản ghi");
                return _responeActionResult;
            }
            _responeActionResult.data = listTaiLieuThucTap;
            _responeActionResult.draw = searchQuery.draw;
            _responeActionResult.recordsTotal = _MenuNguoiDungRepository.TotalRecord;
            _responeActionResult.recordsFiltered = _MenuNguoiDungRepository.TotalRecord;
            return _responeActionResult;
        }

        public ResponeActionResult Insert(MenuNguoiDung item)
        {
            item.FillDataForInsert(1);
            _MenuNguoiDungRepository.Insert(item);
            _responeActionResult.ex_message = "Thêm thành công";
            _responeActionResult.data = new MenuNguoiDung() { Id = item.Id };
            return _responeActionResult;
        }


        public ResponeActionResult Delete(int id)
        {
            _MenuNguoiDungRepository.DeleteById(id);
            _responeActionResult.ex_message = "Xóa thành công";
            return _responeActionResult;
        }

        public ResponeActionResult Update(MenuNguoiDung item)
        {
            item.FillDataForUpdate(1);
            _MenuNguoiDungRepository.Update(item);
            _responeActionResult.ex_message = "Sửa thành công";
            _responeActionResult.data = new MenuNguoiDung() { Id = item.Id };
            return _responeActionResult;
        }
        public MenuNguoiDung_Entity GetEntity(int Id)
        {
            return _MenuNguoiDungRepository.GetEntity(Id);
        }

        public List<MenuNguoiDung> GetListByUserId(int userId)
        {
            return _MenuNguoiDungRepository.GetListByUserId(userId);
        }

    }
}
