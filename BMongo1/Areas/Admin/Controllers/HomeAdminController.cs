using BMongo1.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BMongo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class HomeAdminController : Base1Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
