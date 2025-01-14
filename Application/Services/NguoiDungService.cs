using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class NguoiDungService : INguoiDungService
    {
        private INguoiDungRepository _NguoiDungRepository;
        private IVaiTroRepository _VaiTroRepository;
        private IMenuNguoiDungRepository _MenuNguoiDungRepository;
        private IMenuQuanTriRepository _MenuQuanTriRepository;
        private IQuyenSuDungRepository _QuyenSuDungRepository;
        private readonly ResponeActionResult _responeActionResult;
        public int TotalRecord = 0;

        public NguoiDungService(ResponeActionResult responeActionResult,INguoiDungRepository NguoiDungRepository,
            IVaiTroRepository vaiTroRepository, IMenuQuanTriRepository menuQuanTriRepository, 
            IMenuNguoiDungRepository menuNguoiDungRepository, IQuyenSuDungRepository quyenSuDungRepository) 
        {
            _NguoiDungRepository = NguoiDungRepository;
            _VaiTroRepository = vaiTroRepository;
            _MenuQuanTriRepository = menuQuanTriRepository;
            _MenuNguoiDungRepository = menuNguoiDungRepository;
            _QuyenSuDungRepository = quyenSuDungRepository;
            _responeActionResult = responeActionResult;
        }

        public NguoiDung GetById(int id)
        {
            return _NguoiDungRepository.GetById(id);
        }
        public ResponeActionResult ApprovedItem(int id)
        {
            _NguoiDungRepository.ApprovedItem(id);
            _responeActionResult.ex_message = "Duyệt thành công";
            return _responeActionResult;
        }
        public ResponeActionResult DisapprovedItem(int id)
        {
            _NguoiDungRepository.DisapprovedItem(id);
            _responeActionResult.ex_message = "Hủy duyệt thành công";
            return _responeActionResult;
        }


        public ResponeActionResult GetPaged(NguoiDungQuery searchQuery)
        {
           
            List<NguoiDung_Entity> listTaiLieuThucTap = _NguoiDungRepository.GetPaged(searchQuery);
            if (listTaiLieuThucTap == null)
            {
                _responeActionResult.Message($"Không tìm thấy bản ghi");
                return _responeActionResult;
            }
            _responeActionResult.data = listTaiLieuThucTap;
            _responeActionResult.draw = searchQuery.draw;
            _responeActionResult.recordsTotal = _NguoiDungRepository.TotalRecord;
            _responeActionResult.recordsFiltered = _NguoiDungRepository.TotalRecord;
            return _responeActionResult;
        }

        public ResponeActionResult Insert(NguoiDung item)
        {
            item.FillDataForInsert(1);
            _NguoiDungRepository.Insert(item);
            _responeActionResult.ex_message = "Thêm thành công";
            _responeActionResult.data = new NguoiDung() { Id = item.Id };
            return _responeActionResult;
        }


        public ResponeActionResult Delete(int id)
        {
            _NguoiDungRepository.DeleteById(id);
            _responeActionResult.ex_message = "Xóa thành công";
            return _responeActionResult;
        }

        public ResponeActionResult Update(NguoiDung item)
        {
            item.FillDataForUpdate(1);
            _NguoiDungRepository.Update(item);
            _responeActionResult.ex_message = "Sửa thành công";
            _responeActionResult.data = new NguoiDung() { Id = item.Id };
            return _responeActionResult;
        }
        public NguoiDung_Entity GetEntity(int Id)
        {
            return _NguoiDungRepository.GetEntity(Id);
        }
        public NguoiDung CheckNguoiDungDangNhap(string oTenDangNhap, string pass)
        {
            return _NguoiDungRepository.CheckNguoiDungDangNhap(oTenDangNhap.Trim(), pass);
        }
        public ResponeActionResult GetVaiTroNguoiDungByNguoiDungId(int Id)
        {
            List<VaiTroNguoiDung> oVaiTroMenu = _NguoiDungRepository.GetVaiTroNguoiDungByNguoiDungId(Id);
            _responeActionResult.data = oVaiTroMenu;
            return _responeActionResult;
        }

        public ResponeActionResult SavePhanVaiTro([FromBody] PhanVaiTroRequest request)
        {
            List<int> listIdVaiTro = request.ListIdVaiTro != null ? request.ListIdVaiTro : [];
            List<int> listIdMenu = request.ListIdMenu != null ? request.ListIdMenu : []; ;
            List<int> listIdQuyen = request.ListIdQuyen != null ? request.ListIdQuyen : []; ;

            int idNguoiDung = request.IdNguoiDung;

            _VaiTroRepository.DeleteByUserId(idNguoiDung);
            _MenuNguoiDungRepository.DeleteByUserId(idNguoiDung);
            _QuyenSuDungRepository.DeleteByUserId(idNguoiDung);




            for (int i = 0; i < listIdVaiTro.Count; i++)
            {
                VaiTroNguoiDung vaiTroNguoiDung = new VaiTroNguoiDung
                {
                    UserId = idNguoiDung,
                    VaiTroId = listIdVaiTro[i],
                };
                vaiTroNguoiDung.FillDataForInsert(request.CurrentUserId);
                _VaiTroRepository.AddVaiTroNguoiDung(vaiTroNguoiDung);
            }
            for (int i = 0; i < listIdMenu.Count; i++)
            {
                if (!_MenuQuanTriRepository.CheckHaveSubItem(listIdMenu[i]))
                {
                    MenuNguoiDung menuNguoiDung = new MenuNguoiDung
                    {
                        UserId = idNguoiDung,
                        MenuId = listIdMenu[i],
                    };
                    menuNguoiDung.FillDataForInsert(request.CurrentUserId);
                    _MenuNguoiDungRepository.AddMenuNguoiDung(menuNguoiDung);
                }
            }
            for (int i = 0; i < listIdQuyen.Count; i++)
            {
                if (!_QuyenSuDungRepository.CheckHaveSubItem(listIdQuyen[i]))
                {
                    PhanQuyenNguoiDung phanQuyenNguoiDung = new PhanQuyenNguoiDung
                    {
                        UserId = idNguoiDung,
                        QuyenId = listIdQuyen[i],
                    };
                    phanQuyenNguoiDung.FillDataForInsert(request.CurrentUserId);
                    _QuyenSuDungRepository.AddPhanQuyenNguoiDung(phanQuyenNguoiDung);
                }

            }
            _responeActionResult.ex_message = "Phân vai trò thành công";
            _responeActionResult.data = new VaiTro() { Id = idNguoiDung };
            return _responeActionResult;
        }
    }
}
