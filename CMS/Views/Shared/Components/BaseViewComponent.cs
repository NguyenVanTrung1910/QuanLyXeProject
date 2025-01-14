using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CMS.BaseView
{
    public abstract class BaseViewComponent : ViewComponent
    {
        protected NguoiDung_Entity nguoiDung;
        private readonly IHttpContextAccessor _contextAccessor;
        public BaseViewComponent(IHttpContextAccessor contextAccessor)
        {
            nguoiDung = new NguoiDung_Entity();
            _contextAccessor = contextAccessor;

            var jsonUserData = _contextAccessor.HttpContext.Session.GetString("dataUser");
            if (!string.IsNullOrEmpty(jsonUserData))
            {
                nguoiDung = JsonConvert.DeserializeObject<NguoiDung_Entity>(jsonUserData);
            }
        }

    }
}
