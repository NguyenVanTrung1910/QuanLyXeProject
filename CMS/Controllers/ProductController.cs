using Application.Interfaces;
using Domain.Models;
using Domain.Querys;
using Domain.Querys.Base;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Security.Policy;
using System.Web.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CMS.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(int Id)
        {
            Product itemEdit = new Product();
            if (Id != 0)
                itemEdit = _productService.GetById(Id);
            return View(itemEdit);
        }
        public IActionResult View(int Id)
        {
            Product itemEdit = _productService.GetEntity(Id);
            return PartialView(itemEdit);
        }

        public IActionResult Update(Product oItem)
        {
            try
            {
                var responeActionResult = _productService.Update(oItem);
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
                var responeActionResult =  _productService.ApprovedItem(ID);
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
                var responeActionResult = _productService.DisapprovedItem(ID);
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
                var responeActionResult = _productService.Delete(Id);
                return Ok(responeActionResult);
            }
            catch (Exception ex)
            {
                //Log.Error("Lỗi ở /TaiLieuThucTap/Delete", ex);
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }
        public IActionResult Insert(Product oItem)
        {
            try
            {
                var responeActionResult = _productService.Insert(oItem);
                return Ok(responeActionResult);

            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Lỗi khi sửa");
                return StatusCode(500, "Đã có lỗi xảy ra. Vui lòng thử lại sau.");
            }


        }

        public IActionResult GetPaged(ProductQuery c_query)
        {

            try
            {
                var responeActionResult = _productService.GetPaged(c_query);
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
