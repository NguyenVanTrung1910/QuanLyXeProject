using Application.Interfaces;
using CMS.Models;
using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class MenuQuanTriController : BaseController
    {
        private readonly IMenuQuanTriService _MenuQuanTriService;
        public MenuQuanTriController(INguoiDungService nguoiDungService,IMenuQuanTriService MenuQuanTriService) : base(nguoiDungService)
        {
            _MenuQuanTriService = MenuQuanTriService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetMenuItems()
        {
            return Ok(_MenuQuanTriService.GetTreeMenuTreeView());
        }
        public IActionResult AddSubItem(int Id)
        {
            MenuQuanTri_Entity itemMenu = new MenuQuanTri_Entity();
            if (Id != 0)
            {
                var itemMenuParrent = _MenuQuanTriService.GetEntity(Id);
                itemMenu.MenuCha = itemMenuParrent;
            }
            return View("Edit", itemMenu);
        }
        public IActionResult GetMenuIdByVaiTroIdAndNguoiDungId(int idNguoiDung, string IdVaiTroString)
        {
            List<int> result = _MenuQuanTriService.GetMenuIdByVaiTroIdAndNguoiDungId(idNguoiDung,IdVaiTroString);
            return Ok(result);
        }

        public IActionResult Edit(int Id)
        {
            MenuQuanTri_Entity itemEdit = new MenuQuanTri_Entity();
            if (Id != 0)
                itemEdit = _MenuQuanTriService.GetEntity(Id);
            return View(itemEdit);
        }
        public IActionResult View(int Id)
        {
            MenuQuanTri_Entity itemEdit = _MenuQuanTriService.GetEntity(Id);
            return PartialView(itemEdit);
        }

        public IActionResult Update(MenuQuanTri oItem)
        {
            try
            {
                oItem.CurrentUserId = GetCurrentUser().Id;
                var responeActionResult = _MenuQuanTriService.Update(oItem);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Lỗi khi sửa");
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }
        public IActionResult Approved(int Id)
        {
            try
            {
                var responeActionResult =  _MenuQuanTriService.ApprovedItem(Id);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error("Lỗi ở /TaiLieuThucTap/Approved", ex);
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }
        }

        public IActionResult Disapproval(int Id)
        {
            try
            {
                var responeActionResult = _MenuQuanTriService.DisapprovedItem(Id);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error("Lỗi ở /TaiLieuThucTap/Disapproval", ex);
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }
        public IActionResult Delete(int Id)
        {
            try
            {
                var responeActionResult = _MenuQuanTriService.Delete(Id);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error("Lỗi ở /TaiLieuThucTap/Delete", ex);
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }
        public IActionResult Insert(MenuQuanTri oItem)
        {
            try
            {
                oItem.CurrentUserId = GetCurrentUser().Id;
                var responeActionResult = _MenuQuanTriService.Insert(oItem);
                return Ok(responeActionResult);

            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Lỗi khi sửa");
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }

        public IActionResult GetPaged(MenuQuanTriQuery c_query)
        {

            try
            {
                var responeActionResult = _MenuQuanTriService.GetPaged(c_query);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Lỗi khi lấy danh sách");
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }
        }
    }
}