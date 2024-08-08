using BMongo1.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc;

namespace BMongo1.Controllers
{
    public class OrderDetailsController : Controller
    {
        IRepositoryAccount repositoryaccount;
        IRepositoryCategory repositorycategory;
        IRepositoryCart repositorycart;
        IRepositoryProduct repositoryproduct;
        IRepositoryOrder repositoryorder;
        IRepositoryOrderDetail repositoryorderoetail;

        public OrderDetailsController(IRepositoryAccount repositoryaccount, IRepositoryCategory repositorycategory, IRepositoryCart repositorycart, IRepositoryProduct repositoryproduct, IRepositoryOrder repositoryorder, IRepositoryOrderDetail repositoryorderoetail)
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
            return View(ODT);
        }
    }
}
