using BMongo1.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BMongo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderDetailsAdminController : Controller
    {
        IRepositoryAccount repositoryaccount;
        IRepositoryCategory repositorycategory;
        IRepositoryCart repositorycart;
        IRepositoryProduct repositoryproduct;
        IRepositoryOrder repositoryorder;
        IRepositoryOrderDetail repositoryorderoetail;

        public OrderDetailsAdminController(IRepositoryAccount repositoryaccount, IRepositoryCategory repositorycategory, IRepositoryCart repositorycart, IRepositoryProduct repositoryproduct, IRepositoryOrder repositoryorder, IRepositoryOrderDetail repositoryorderoetail)
        {
            this.repositoryaccount = repositoryaccount;
            this.repositorycategory = repositorycategory;
            this.repositorycart = repositorycart;
            this.repositoryproduct = repositoryproduct;
            this.repositoryorder = repositoryorder;
            this.repositoryorderoetail = repositoryorderoetail;
        }
        public IActionResult Index(int id)
        {
            var ODT = repositoryorderoetail.GetForeignKey(id);
            foreach (var o in ODT)
            {
                ViewData["Name"] = o.ReceiverName;
            }
            return View(ODT);
        }
    }
}
