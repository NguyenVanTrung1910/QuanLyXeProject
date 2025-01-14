using Microsoft.AspNetCore.Mvc;

namespace Cl.Admin.Views.Shared.Components._Footer
{
    [ViewComponent]
    public class _Footer : ViewComponent
    {

        public _Footer()
        {

        }

        public IViewComponentResult Invoke()
        {

            return View("_Footer.cs");

        }
    }
}
