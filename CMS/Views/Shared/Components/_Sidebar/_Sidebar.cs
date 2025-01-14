
using Application.Interfaces;
using CMS.BaseView;
using Domain.Models;
using Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Cl.Admin.Views.Shared.Components._Sidebar
{

    [ViewComponent]
    public class _Sidebar : BaseViewComponent
    {
        private DBContext _dBContext;
        private IMenuQuanTriService _MenuQuanTriService;
        private IMenuNguoiDungService _MenuNguoiDungService;

        public _Sidebar(IHttpContextAccessor contextAccessor, DBContext dBContext, IMenuNguoiDungService menuNguoiDungService, IMenuQuanTriService menuQuanTriService) : base(contextAccessor)
        {
            _dBContext = dBContext;
            _MenuNguoiDungService = menuNguoiDungService;
            _MenuQuanTriService = menuQuanTriService;
        }

        public IViewComponentResult Invoke()
        {
            var onguoiDung = nguoiDung;
            List<int> ListMenuDuocPhan = _MenuNguoiDungService.GetListByUserId(onguoiDung.Id).Select(a => a.MenuId).ToList();
            List<TreeMenuQuanTri> listMenuQuanTri = _MenuQuanTriService.GetTreeMenu();
            List<TreeMenuQuanTri> filteredListMenuQuanTri = FilterMenus(listMenuQuanTri, ListMenuDuocPhan);
            //string menuSezilize = JsonConvert.SerializeObject(filteredListMenuQuanTri);
            return View("_Sidebar.cs", filteredListMenuQuanTri);

        }


        private List<TreeMenuQuanTri> FilterMenus(List<TreeMenuQuanTri> menus, List<int> allowedMenuIds)
        {
            List<TreeMenuQuanTri> filteredMenus = new List<TreeMenuQuanTri>();

            foreach (var menu in menus)
            {
                // Kiểm tra xem menu hiện tại có trong danh sách cho phép hay không
                if (allowedMenuIds.Contains(menu.Id))
                {
                    // Nếu menu có trong danh sách cho phép, kiểm tra các con của nó
                    if (menu.children != null && menu.children.Any())
                    {
                        menu.children = FilterMenus(menu.children, allowedMenuIds);
                    }
                    // Thêm menu hiện tại vào danh sách kết quả
                    filteredMenus.Add(menu);
                }
                else if (menu.children != null && menu.children.Any())
                {
                    // Nếu menu hiện tại không có trong danh sách cho phép nhưng có con
                    // Kiểm tra các con của nó
                    var filteredChildren = FilterMenus(menu.children, allowedMenuIds);
                    if (filteredChildren.Any())
                    {
                        // Nếu có bất kỳ con nào trong danh sách cho phép, thêm menu hiện tại vào danh sách kết quả
                        menu.children = filteredChildren;
                        filteredMenus.Add(menu);
                    }
                }
            }

            return filteredMenus;
        }

    }
}
