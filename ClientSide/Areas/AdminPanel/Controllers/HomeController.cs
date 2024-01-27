using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;

namespace ClientSide.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [PermissionCheck(1)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
