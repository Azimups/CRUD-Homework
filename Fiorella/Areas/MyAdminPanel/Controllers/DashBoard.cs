using Microsoft.AspNetCore.Mvc;

namespace Fiorella.Areas.MyAdminPanel.Controllers
{
    [Area("MyAdminPanel")]
    public class DashBoard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}