using Application.Interfaces;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CMS.Models;
using Domain.Entities;

namespace CMS.Controllers
{
    [Authorize]
    public class NguoiDungController : BaseController
    {
        private readonly INguoiDungService _NguoiDungService;
        private readonly IMenuQuanTriService _MenuQuanTriService;
        private readonly IQuyenSuDungService _QuyenSuDungService;
        public NguoiDungController(INguoiDungService NguoiDungService, IMenuQuanTriService menuQuanTriService, IQuyenSuDungService quyenSuDungService) : base(NguoiDungService)
        {
            _NguoiDungService = NguoiDungService;
            _MenuQuanTriService = menuQuanTriService;
            _QuyenSuDungService = quyenSuDungService;
        }
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.isQuanTriHT = GetCurrentUser().isQuanTriHT;
            return View();
        }
        public IActionResult PhanVaiTro(int Id)
        {

            NguoiDung_Entity oNguoiDung = new NguoiDung_Entity();
            if (Id != 0)
            {
                oNguoiDung = _NguoiDungService.GetEntity(Id);
                if (oNguoiDung == null) { oNguoiDung = new NguoiDung_Entity(); }
            }
            var allMenuIds = _MenuQuanTriService.GetAllMenuQuanTriIds();
            ViewBag.AllMenuIds = allMenuIds;
            var allMenuChaQuyen = _QuyenSuDungService.GetAllQuyenCha();
            ViewBag.AllQuyenCha = allMenuChaQuyen;
            return PartialView(oNguoiDung);
        }
        public IActionResult GetVaiTroNguoiDungByNguoiDungId(int Id)
        {
            var responeActionResult = _NguoiDungService.GetVaiTroNguoiDungByNguoiDungId(Id);
            return Ok(responeActionResult);
        }
        [HttpPost]
        public IActionResult SavePhanVaiTro([FromBody] PhanVaiTroRequest request)
        {
            request.CurrentUserId = GetCurrentUser().Id;
            var responeActionResult = _NguoiDungService.SavePhanVaiTro(request);
            return Ok(responeActionResult);
        }










        public IActionResult Edit(int Id)
        {
            NguoiDung_Entity itemEdit = new NguoiDung_Entity();
            if (Id != 0)
                itemEdit = _NguoiDungService.GetEntity(Id);
            return View(itemEdit);
        }
        public IActionResult View(int Id)
        {
            NguoiDung_Entity itemEdit = _NguoiDungService.GetEntity(Id);
            return PartialView(itemEdit);
        }

        public IActionResult Update(NguoiDung oItem)
        {
            try
            {
                var responeActionResult = _NguoiDungService.Update(oItem);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Lỗi khi sửa");
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }
        public IActionResult Approved(int ID)
        {
            try
            {
                var responeActionResult =  _NguoiDungService.ApprovedItem(ID);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error("Lỗi ở /TaiLieuThucTap/Approved", ex);
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }
        }

        public IActionResult Disapproval(int ID)
        {
            try
            {
                var responeActionResult = _NguoiDungService.DisapprovedItem(ID);
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
                var responeActionResult = _NguoiDungService.Delete(Id);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error("Lỗi ở /TaiLieuThucTap/Delete", ex);
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }
        public IActionResult Insert(NguoiDung oItem)
        {
            try
            {
                var responeActionResult = _NguoiDungService.Insert(oItem);
                return Ok(responeActionResult);

            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Lỗi khi sửa");
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }

        public IActionResult GetPaged(NguoiDungQuery c_query)
        {

            try
            {
                var responeActionResult = _NguoiDungService.GetPaged(c_query);
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