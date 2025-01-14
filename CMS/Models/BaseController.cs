using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories.SqlServer;
using Domain.Querys.Base;
using Infrastructure.Persitence.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CMS.Controllers
{
    public class BaseController : Controller
    {
        private readonly INguoiDungService _NguoiDungService;

        protected BaseController(INguoiDungService nguoiDungService)
        {
            _NguoiDungService = nguoiDungService;
        }

        public NguoiDung_Entity GetCurrentUser()
        {
            if (HttpContext.User?.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userID))
                {
                    return new NguoiDung_Entity();
                }

                var nguoiDung = _NguoiDungService.GetEntity(userID);
                if (nguoiDung == null || nguoiDung.ModerationStatus != ModerationStatus.Approved)
                {
                    return new NguoiDung_Entity();
                }
                if (nguoiDung != null)
                {
                    return new NguoiDung_Entity
                    {
                        Id = nguoiDung.Id,
                        TenNguoiDung = nguoiDung.TenNguoiDung,
                        Email = nguoiDung.Email,
                        DienThoai = nguoiDung.DienThoai
                    };
                }
            }
            return new NguoiDung_Entity();

        }
    }
}
