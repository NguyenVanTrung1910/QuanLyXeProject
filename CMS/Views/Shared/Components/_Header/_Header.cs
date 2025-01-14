using Application.Interfaces;
using CMS.BaseView;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Cl.Admin.Views.Shared.Components
{
    [ViewComponent]
    public class _Header : BaseViewComponent
    {
        private readonly IConfiguration _configuration;
        private INguoiDungService _NguoiDungService;
        public _Header(IHttpContextAccessor contextAccessor,
             IConfiguration configuration,
            INguoiDungService nguoiDungService) : base(contextAccessor)
        {
            _NguoiDungService = nguoiDungService;
            _configuration = configuration;
        }


        public IViewComponentResult Invoke()
        {
            // Lấy timestamp của ngày hiện tại
            DateTime currentDate = DateTime.Today;
            long ticks = currentDate.Ticks;


            byte[] bytes = Encoding.UTF8.GetBytes(ticks.ToString());
            byte[] hashBytes;

            using (MD5 md5 = MD5.Create())
            {
                hashBytes = md5.ComputeHash(bytes);
            }

            int hashValue = hashBytes[0];
            int tensDigit = Math.Abs(hashValue);

            ViewBag.LastTwoDigits = tensDigit;
            return View("_Header.cs",nguoiDung);

        }
    }
}
