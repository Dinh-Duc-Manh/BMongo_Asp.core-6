using BMongo1.Controllers;
using BMongo1.Models;
using BMongo1.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMongo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersAdminController : Base1Controller
    {
        IRepositoryAccount repositoryaccount;
        IRepositoryCategory repositorycategory;
        IRepositoryCart repositorycart;
        IRepositoryProduct repositoryproduct;
        IRepositoryOrder repositoryorder;
        IRepositoryOrderDetail repositoryorderoetail;

        public OrdersAdminController(IRepositoryAccount repositoryaccount, IRepositoryCategory repositorycategory, IRepositoryCart repositorycart, IRepositoryProduct repositoryproduct, IRepositoryOrder repositoryorder, IRepositoryOrderDetail repositoryorderoetail)
        {
            this.repositoryaccount = repositoryaccount;
            this.repositorycategory = repositorycategory;
            this.repositorycart = repositorycart;
            this.repositoryproduct = repositoryproduct;
            this.repositoryorder = repositoryorder;
            this.repositoryorderoetail = repositoryorderoetail;
        }
        public IActionResult Index()
        {
            var order = repositoryorder.GetAll();
            return View(order);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Orders orders)
        {

            try
            {
                repositoryorder.Update(orders);
            }
            catch (Exception)
            {
                return NotFound();

            }
            return RedirectToAction(nameof(Index));

        }

    }
}
