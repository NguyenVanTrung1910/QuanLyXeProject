using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class VaiTroService : IVaiTroService
    {
        private IVaiTroRepository _VaiTroRepository;
        private IQuyenSuDungRepository _QuyenSuDungRepository;
        private IMenuQuanTriRepository _MenuQuanTriRepository;
        private readonly ResponeActionResult _responeActionResult;
        public int TotalRecord = 0;

        public VaiTroService(ResponeActionResult responeActionResult,IVaiTroRepository VaiTroRepository, IQuyenSuDungRepository quyenSuDungRepository, IMenuQuanTriRepository menuQuanTriRepository) 
        {
            _VaiTroRepository = VaiTroRepository;
            _responeActionResult = responeActionResult;
            _QuyenSuDungRepository = quyenSuDungRepository;
            _MenuQuanTriRepository = menuQuanTriRepository;
        }

        public VaiTro GetById(int id)
        {
            return _VaiTroRepository.GetById(id);
        }
        public ResponeActionResult ApprovedItem(int id)
        {
            _VaiTroRepository.ApprovedItem(id);
            _responeActionResult.ex_message = "Duyệt thành công";
            return _responeActionResult;
        }
        public ResponeActionResult DisapprovedItem(int id)
        {
            _VaiTroRepository.DisapprovedItem(id);
            _responeActionResult.ex_message = "Hủy duyệt thành công";
            return _responeActionResult;
        }


        public ResponeActionResult GetPaged(VaiTroQuery searchQuery)
        {
           
            List<VaiTro_Entity> listTaiLieuThucTap = _VaiTroRepository.GetPaged(searchQuery);
            if (listTaiLieuThucTap == null)
            {
                _responeActionResult.Message($"Không tìm thấy bản ghi");
                return _responeActionResult;
            }
            _responeActionResult.data = listTaiLieuThucTap;
            _responeActionResult.draw = searchQuery.draw;
            _responeActionResult.recordsTotal = _VaiTroRepository.TotalRecord;
            _responeActionResult.recordsFiltered = _VaiTroRepository.TotalRecord;
            return _responeActionResult;
        }

        public ResponeActionResult Insert(VaiTro item)
        {
            item.FillDataForInsert(1);
            _VaiTroRepository.Insert(item);
            _responeActionResult.ex_message = "Thêm thành công";
            _responeActionResult.data = new VaiTro() { Id = item.Id };
            return _responeActionResult;
        }


        public ResponeActionResult Delete(int id)
        {
            _VaiTroRepository.DeleteById(id);
            _responeActionResult.ex_message = "Xóa thành công";
            return _responeActionResult;
        }

        public ResponeActionResult Update(VaiTro item)
        {
            item.FillDataForUpdate(1);
            _VaiTroRepository.Update(item);
            _responeActionResult.ex_message = "Sửa thành công";
            _responeActionResult.data = new VaiTro() { Id = item.Id };
            return _responeActionResult;
        }
        public VaiTro_Entity GetEntity(int Id)
        {
            return _VaiTroRepository.GetEntity(Id);
        }

        public ResponeActionResult GetVaiTroMenuByVaiTroId(int Id)
        {
            List<VaiTroMenu> oVaiTroMenu = _VaiTroRepository.GetVaiTroMenuByVaiTroId(Id);
            _responeActionResult.data = oVaiTroMenu;

            return _responeActionResult;
        }
        public ResponeActionResult GetVaiTroQuyenByVaiTroId(int Id)
        {
            List<VaiTroQuyen> oVaiTroQuyen = _VaiTroRepository.GetVaiTroQuyenByVaiTroId(Id);
            _responeActionResult.data = oVaiTroQuyen;
            return _responeActionResult;
        }
        public ResponeActionResult SavePhanQuyen([FromBody] PhanQuyenRequest request)
        {
            List<int> listIdMenu = request.ListIdMenu;
            List<int> listIdQuyen = request.ListIdQuyen;
            int idVaiTro = request.IdVaiTro;

            _VaiTroRepository.DeleteQuyenByVaiTroId(idVaiTro);

            _VaiTroRepository.DeleteMenuByVaiTroId(idVaiTro);

            for (int i = 0; i < listIdQuyen.Count; i++)
            {
                if (!_QuyenSuDungRepository.CheckHaveSubItem(listIdQuyen[i]))
                {
                    VaiTroQuyen vaiTro = new VaiTroQuyen
                    {
                        VaiTroId = idVaiTro,
                        QuyenSuDungId = listIdQuyen[i],
                    };
                    vaiTro.FillDataForInsert(request.NguoiDungId);
                    _VaiTroRepository.AddQuyenForVaiTro(vaiTro);
                }
            }
            for (int i = 0; i < listIdMenu.Count; i++)
            {
                if (!_MenuQuanTriRepository.CheckHaveSubItem(listIdMenu[i]))
                {
                    VaiTroMenu menu = new VaiTroMenu
                    {
                        VaiTroId = idVaiTro,
                        MenuQuanTriId = listIdMenu[i],
                    };
                    menu.FillDataForInsert(request.NguoiDungId);
                    _VaiTroRepository.AddMenuForVaiTro(menu);
                }
            }
            _responeActionResult.ex_message = "Phân quyền thành công";
            _responeActionResult.data = new VaiTro() { Id = idVaiTro };
            return _responeActionResult;
        }
        public List<ItemTreeView> GetTreeVaiTroTreeView()
        {
            return _VaiTroRepository.GetTreeVaiTroTreeView();
        }
    }
}
