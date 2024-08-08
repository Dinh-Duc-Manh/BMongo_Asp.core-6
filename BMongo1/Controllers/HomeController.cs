using BMongo1.Models;
using BMongo1.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BMongo1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IRepositoryAccount repositoryaccount;
        IRepositoryCategory repositorycategory;
        IRepositoryCart repositorycart;
        IRepositoryProduct repositoryproduct;
        IRepositoryOrder repositoryorder;
        IRepositoryOrderDetail repositoryorderoetail;

        public HomeController(IRepositoryAccount repositoryaccount, IRepositoryCategory repositorycategory, IRepositoryCart repositorycart, IRepositoryProduct repositoryproduct, ILogger<HomeController> logger, IRepositoryOrder repositoryorder, IRepositoryOrderDetail repositoryorderoetail)
        {
            this.repositoryaccount = repositoryaccount;
            this.repositorycategory = repositorycategory;
            this.repositorycart = repositorycart;
            this.repositoryproduct = repositoryproduct;
            this.repositoryorder = repositoryorder;
            this.repositoryorderoetail = repositoryorderoetail;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
