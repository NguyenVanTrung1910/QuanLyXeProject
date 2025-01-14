using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class VaiTroController : BaseController
    {
        private readonly IVaiTroService _VaiTroService;
        private readonly IMenuQuanTriService _MenuQuanTriService;
        private readonly IQuyenSuDungService _QuyenSuDungService;
        public VaiTroController(INguoiDungService nguoiDungService,IVaiTroService VaiTroService,IMenuQuanTriService menuQuanTriService, IQuyenSuDungService quyenSuDungService) : base(nguoiDungService)
        {
            _VaiTroService = VaiTroService;
            _MenuQuanTriService = menuQuanTriService;
            _QuyenSuDungService = quyenSuDungService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetVaiTroItems()
        {
            return Ok(_VaiTroService.GetTreeVaiTroTreeView());
        }
        public IActionResult PhanQuyen(int Id)
        {

            VaiTro_Entity oVaiTro = new VaiTro_Entity();
            if (Id != 0)
            {
                oVaiTro = _VaiTroService.GetEntity(Id);
                if (oVaiTro == null) { oVaiTro = new VaiTro_Entity(); }
            }
            var allMenuIds = _MenuQuanTriService.GetAllMenuQuanTriIds();
            ViewBag.AllMenuIds = allMenuIds;
            var allMenuChaQuyen = _QuyenSuDungService.GetAllQuyenCha();
            ViewBag.AllQuyenCha = allMenuChaQuyen;
            return PartialView(oVaiTro);
        }

        public IActionResult GetVaiTroMenuByVaiTroId(int Id)
        {
            var responeActionResult = _VaiTroService.GetVaiTroMenuByVaiTroId(Id);
            return Ok(responeActionResult);
        }
        public IActionResult GetVaiTroQuyenByVaiTroId(int Id)
        {
            var responeActionResult = _VaiTroService.GetVaiTroQuyenByVaiTroId(Id);
            return Ok(responeActionResult);
        }


        [HttpPost]
        public IActionResult SavePhanQuyen([FromBody] PhanQuyenRequest request)
        {
            request.NguoiDungId = GetCurrentUser().Id;
            var responeActionResult = _VaiTroService.SavePhanQuyen(request);
            return Ok(responeActionResult);
        }





        public IActionResult Edit(int Id)
        {
            VaiTro_Entity itemEdit = new VaiTro_Entity();
            if (Id != 0)
                itemEdit = _VaiTroService.GetEntity(Id);
            return View(itemEdit);
        }
        public IActionResult View(int Id)
        {
            VaiTro itemEdit = _VaiTroService.GetEntity(Id);
            return PartialView(itemEdit);
        }

        public IActionResult Update(VaiTro oItem)
        {
            try
            {
                var responeActionResult = _VaiTroService.Update(oItem);
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
                var responeActionResult =  _VaiTroService.ApprovedItem(ID);
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
                var responeActionResult = _VaiTroService.DisapprovedItem(ID);
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
                var responeActionResult = _VaiTroService.Delete(Id);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error("Lỗi ở /TaiLieuThucTap/Delete", ex);
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }
        public IActionResult Insert(VaiTro oItem)
        {
            try
            {
                var responeActionResult = _VaiTroService.Insert(oItem);
                return Ok(responeActionResult);

            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Lỗi khi sửa");
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }

        public IActionResult GetPaged(VaiTroQuery c_query)
        {

            try
            {
                var responeActionResult = _VaiTroService.GetPaged(c_query);
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