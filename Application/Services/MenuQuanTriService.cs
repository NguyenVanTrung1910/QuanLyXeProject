using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;

namespace Application.Services
{
    public class MenuQuanTriService : IMenuQuanTriService
    {
        private IMenuQuanTriRepository _MenuQuanTriRepository;
        private IMenuNguoiDungRepository _MenuNguoiDungRepository;
        private IVaiTroRepository _VaiTroRepository;
        private readonly ResponeActionResult _responeActionResult;
        public int TotalRecord = 0;

        public MenuQuanTriService(ResponeActionResult responeActionResult,IMenuQuanTriRepository MenuQuanTriRepository, IMenuNguoiDungRepository menuNguoiDungRepository, IVaiTroRepository vaiTroRepository) 
        {
            _MenuQuanTriRepository = MenuQuanTriRepository;
            _responeActionResult = responeActionResult;
            _MenuNguoiDungRepository = menuNguoiDungRepository;
            _VaiTroRepository = vaiTroRepository;

        }

        public List<ItemTreeView> GetTreeMenuTreeView()
        {
            return _MenuQuanTriRepository.GetTreeMenuTreeView();
        }

        public List<TreeMenuQuanTri> GetTreeMenu()
        {
            return _MenuQuanTriRepository.GetTreeMenu();
        }
        public List<int> GetAllMenuQuanTriIds()
        {
            return _MenuQuanTriRepository.GetAllMenuQuanTriIds();
        }

        public List<int> GetMenuIdByVaiTroIdAndNguoiDungId(int idNguoiDung, string IdVaiTroString)
        {
            List<int> result = new List<int>();
            List<int> listIdVaiTro = IdVaiTroString != null ? IdVaiTroString.Split(',').Select(int.Parse).ToList() : [];
            if (_MenuNguoiDungRepository.GetListByUserId(idNguoiDung) != null)
            {
                result = _MenuNguoiDungRepository.GetListByUserId(idNguoiDung).Select(a => a.MenuId).ToList();
            }


            for (int i = 0; i < listIdVaiTro.Count; i++)
            {
                var listMenu = _VaiTroRepository.GetMenuByVaiTroId(listIdVaiTro[i]).Select(a => a.MenuQuanTriId).ToList();
                for (int j = 0; j < listMenu.Count; j++)
                {
                    result.Add(listMenu[j]);
                }

            }
            result = result.Distinct().ToList();
            return result;
        }















        public MenuQuanTri GetById(int id)
        {
            return _MenuQuanTriRepository.GetById(id);
        }
        public ResponeActionResult ApprovedItem(int id)
        {
            _MenuQuanTriRepository.ApprovedItem(id);
            _responeActionResult.ex_message = "Duyệt thành công";
            return _responeActionResult;
        }
        public ResponeActionResult DisapprovedItem(int id)
        {
            _MenuQuanTriRepository.DisapprovedItem(id);
            _responeActionResult.ex_message = "Hủy duyệt thành công";
            return _responeActionResult;
        }


        public ResponeActionResult GetPaged(MenuQuanTriQuery searchQuery)
        {
           
            List<MenuQuanTri_Entity> listTaiLieuThucTap = _MenuQuanTriRepository.GetPaged(searchQuery);
            if (listTaiLieuThucTap == null)
            {
                _responeActionResult.Message($"Không tìm thấy bản ghi");
                return _responeActionResult;
            }
            _responeActionResult.data = listTaiLieuThucTap;
            _responeActionResult.draw = searchQuery.draw;
            _responeActionResult.recordsTotal = _MenuQuanTriRepository.TotalRecord;
            _responeActionResult.recordsFiltered = _MenuQuanTriRepository.TotalRecord;
            return _responeActionResult;
        }

        public ResponeActionResult Insert(MenuQuanTri item)
        {
            item.ModerationStatus = ModerationStatus.Approved;
            item.FillDataForInsert(item.CurrentUserId);
            _MenuQuanTriRepository.Insert(item);
            _responeActionResult.ex_message = "Thêm thành công";
            _responeActionResult.data = new MenuQuanTri() { Id = item.Id };
            return _responeActionResult;
        }


        public ResponeActionResult Delete(int id)
        {
            _MenuQuanTriRepository.DeleteById(id);
            _responeActionResult.ex_message = "Xóa thành công";
            return _responeActionResult;
        }

        public ResponeActionResult Update(MenuQuanTri item)
        {
            item.FillDataForUpdate(item.CurrentUserId);
            _MenuQuanTriRepository.Update(item);
            _responeActionResult.ex_message = "Sửa thành công";
            _responeActionResult.data = new MenuQuanTri() { Id = item.Id };
            return _responeActionResult;
        }
        public MenuQuanTri_Entity GetEntity(int Id)
        {
            return _MenuQuanTriRepository.GetEntity(Id);
        }



    }
}
