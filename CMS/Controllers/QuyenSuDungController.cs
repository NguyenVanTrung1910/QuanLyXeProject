using Application.Interfaces;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System.Text;

namespace CMS.Controllers
{
    public class QuyenSuDungController : BaseController
    {
        private readonly IQuyenSuDungService _QuyenSuDungService;
        public QuyenSuDungController(INguoiDungService nguoiDungService,IQuyenSuDungService QuyenSuDungService) : base(nguoiDungService)
        {
            _QuyenSuDungService = QuyenSuDungService;
        }
        public IActionResult Index()
        {
            return View();
        }
        //[HttpGet("/permissions.js")]
        //public IActionResult GetPermissionsJs()
        //{
        //    List<string> maQuyen = _PhanQuyenNguoiDungsRepository.GetListEntityByUserId(CurrentUser.Id).Select(a => a.QuyenSuDung.MaQuyen.ToString()).ToList();
        //    List<string> allMaQuyen = _QuyenSuDungsRepository.GetAllEntity().Select(a => a.MaQuyen.ToString()).ToList();
        //    //<button id="myButton" data-permission="001">Click me</button>
        //    StringBuilder listDataMission = new StringBuilder();
        //    for (int i = 0; i < allMaQuyen.Count; i++)
        //    {
        //        if (!maQuyen.Contains(allMaQuyen[i]))
        //        {
        //            listDataMission.Append($"$('[data-permission=\"{allMaQuyen[i]}\"]').hide();");
        //            listDataMission.Append($"$('[data-permission=\"{allMaQuyen[i]}\"]').removeClass(\"d-flex\");");
        //        }
        //    }
        //    string jsContent = @"
        //        function checkPermissions() {"
        //           + listDataMission.ToString() +
        //        "}";
        //    // Trả về nội dung JavaScript với loại nội dung application/javascript
        //    return Content(jsContent, "application/javascript");
        //}
        [HttpGet("/permissions.js")]
        public IActionResult GetPermissionsJs()
        {
            //List<string> maQuyen = _PhanQuyenNguoiDungsRepository.GetListEntityByUserId(CurrentUser.Id).Select(a => a.QuyenSuDung.MaQuyen.ToString()).ToList();
            //List<string> allMaQuyen = _QuyenSuDungsRepository.GetAllEntity().Select(a => a.MaQuyen.ToString()).ToList();
            ////<button id="myButton" data-permission="001">Click me</button>
            StringBuilder listDataMission = new StringBuilder();
            //for (int i = 0; i < allMaQuyen.Count; i++)
            //{
            //    if (!maQuyen.Contains(allMaQuyen[i]))
            //    {
            //        listDataMission.Append($"$('[data-permission=\"{allMaQuyen[i]}\"]').hide();");
            //        listDataMission.Append($"$('[data-permission=\"{allMaQuyen[i]}\"]').removeClass(\"d-flex\");");
            //    }
            //}
            string jsContent = @"
                function checkPermissions() {"
                   + listDataMission.ToString() +
                "}";
            // Trả về nội dung JavaScript với loại nội dung application/javascript
            return Content(jsContent, "application/javascript");
        }




        public IActionResult AddSubItem(int Id)
        {

            QuyenSuDung_Entity itemMenu = new QuyenSuDung_Entity();
            if (Id != 0)
            {
                var itemMenuParrent = _QuyenSuDungService.GetEntity(Id);
                itemMenu.QuyenCha = itemMenuParrent;
            }
            return View("Edit", itemMenu);
        }




        public IActionResult GetQuyenItems()
        {
            return Ok(_QuyenSuDungService.GetTreeQuyenTreeView());
        }

        //public IActionResult GetQuyenIdByVaiTroId(int id)
        //{
        //    var listQuyen = _VaiTroQuyensRepository.GetByVaiTroId(id).Select(a => a.QuyenSuDungId).ToList();
        //    return Ok(listQuyen);
        //}

        public IActionResult GetQuyenIdByVaiTroIdAndNguoiDungId(int idNguoiDung, string IdVaiTroString)
        {
            List<int> result = _QuyenSuDungService.GetQuyenIdByVaiTroIdAndNguoiDungId(idNguoiDung,IdVaiTroString);
            return Ok(result);
        }













        public IActionResult Edit(int Id)
        {

            QuyenSuDung_Entity itemMenu = new QuyenSuDung_Entity();
            QuyenSuDung_Entity itemMenuCha = new QuyenSuDung_Entity();
            if (Id != 0)
            {
                itemMenu = _QuyenSuDungService.GetEntity(Id);
                if (itemMenu.QuyenSuDungCha != null)
                {
                    itemMenuCha = _QuyenSuDungService.GetEntity((int)itemMenu.QuyenSuDungCha);
                    itemMenu.QuyenCha = itemMenuCha;
                }

                if (itemMenu == null) { itemMenu = new QuyenSuDung_Entity(); }
            }

            return View(itemMenu);
        }
        public IActionResult View(int Id)
        {
            QuyenSuDung_Entity itemEdit = _QuyenSuDungService.GetEntity(Id);
            return PartialView(itemEdit);
        }

        public IActionResult Update(QuyenSuDung oItem)
        {
            try
            {
                oItem.CurrentUserId = GetCurrentUser().Id;
                var responeActionResult = _QuyenSuDungService.Update(oItem);
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
                var responeActionResult =  _QuyenSuDungService.ApprovedItem(ID);
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
                var responeActionResult = _QuyenSuDungService.DisapprovedItem(ID);
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
                var responeActionResult = _QuyenSuDungService.Delete(Id);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error("Lỗi ở /TaiLieuThucTap/Delete", ex);
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }
        public IActionResult Insert(QuyenSuDung oItem)
        {
            try
            {
                oItem.CurrentUserId = GetCurrentUser().Id;
                var responeActionResult = _QuyenSuDungService.Insert(oItem);
                return Ok(responeActionResult);

            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Lỗi khi sửa");
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }

        public IActionResult GetPaged(QuyenSuDungQuery c_query)
        {

            try
            {
                var responeActionResult = _QuyenSuDungService.GetPaged(c_query);
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