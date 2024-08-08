using BMongo1.Models;
using BMongo1.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMongo1.Controllers
{
    public class CartController : BaseController
    {
        IRepositoryAccount repositoryaccount;
        IRepositoryCategory repositorycategory;
        IRepositoryCart repositorycart;
        IRepositoryProduct repositoryproduct;

        public CartController(IRepositoryAccount repositoryaccount, IRepositoryCategory repositorycategory, IRepositoryCart repositorycart, IRepositoryProduct repositoryproduct)
        {
            this.repositoryaccount = repositoryaccount;
            this.repositorycategory = repositorycategory;
            this.repositorycart = repositorycart;
            this.repositoryproduct = repositoryproduct;
        }

        public async Task<IActionResult> Index()
        {

            var cart = repositorycart.GetForeignKey((int)HttpContext.Session.GetInt32("LoginId"));
            int c = 0;
            Int32 a = 0;
            foreach (var item in cart)
            {
                if (item.AccountId == HttpContext.Session.GetInt32("LoginId"))
                {
                    c++;
                    ViewData["Number_Pro"] = c;
                    a += (Int32)item.TotalPrice;
                    ViewData["Total_Cart"] = a.ToString("#,##0 $");
                    TempData["cart"] = "";
                }
            }
            if (cart == null)
            {
                TempData["cart"] = "123";
            }
            //var sem3DBContext = _context.Carts.Include(a => a.Account).Include(p => p.Product);
            return View(cart);
        }

        // POST: Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cart cart)
        {
            cart.TotalPrice *= cart.Quantity;

            if (cart.Quantity < 1)
            {
                cart.Quantity = 1;
            }
            int check = 0;
            var getCart = repositorycart.All();
            foreach (var c in getCart)
            {
                if (c.ProductId == cart.ProductId && c.AccountId == cart.AccountId)
                {
                    cart._id = c._id;
                    cart.Quantity += c.Quantity;
                    cart.TotalPrice = cart.Quantity * cart.TotalPrice;
                    repositorycart.Update(cart);
                    check++;
                }
                else
                {
                    continue;
                }
            }

            if (check == 0)
            {
                repositorycart.Insert(cart);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int CartId, Cart cart)
        {
            try
            {
                if (cart.Quantity < 1)
                {
                    cart.Quantity = 1;
                }

                cart.TotalPrice = cart.Quantity * cart.TotalPrice;
                cart._id = CartId;
                repositorycart.Update(cart);
            }
            catch (Exception)
            {

                return NotFound();
            }
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            repositorycart.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
