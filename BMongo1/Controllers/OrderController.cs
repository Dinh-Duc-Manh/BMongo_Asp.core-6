using BMongo1.Models;
using BMongo1.Models.BusinessModels;
using BMongo1.Models.ModelsViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMongo1.Controllers
{
    public class OrderController : Controller
    {
        IRepositoryAccount repositoryaccount;
        IRepositoryCategory repositorycategory;
        IRepositoryCart repositorycart;
        IRepositoryProduct repositoryproduct;
        IRepositoryOrder repositoryorder;
        IRepositoryOrderDetail repositoryorderoetail;

        public OrderController(IRepositoryAccount repositoryaccount, IRepositoryCategory repositorycategory, IRepositoryCart repositorycart, IRepositoryProduct repositoryproduct, IRepositoryOrder repositoryorder, IRepositoryOrderDetail repositoryorderoetail)
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

        public IActionResult Create()
        {
            var cart = repositorycart.GetForeignKey((int)HttpContext.Session.GetInt32("LoginId"));
            int c = 0;
            int a = 0;
            List<CartViewModel> list = new List<CartViewModel>();
            foreach (var item in cart)
            {
                if (item.AccountId == HttpContext.Session.GetInt32("LoginId"))
                {

                    c++;
                    ViewData["Number_Pro"] = c;
                    a += (int)item.TotalPrice;
                    ViewData["Total_Cart"] = a.ToString("#,##0 $");

                }
                list.Add(new CartViewModel() { _id = item._id, TotalPrice = item.TotalPrice, Quantity = item.Quantity, ProductName = item.ProductName, ProductImage = item.ProductImage, Price = item.Price });

            }
            ViewData["cart"] = list;

            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Orders orders)
        {
            if (ModelState.IsValid)
            {
                orders.OrderStatus = "Confirming";
                orders.AccountId = HttpContext.Session.GetInt32("LoginId");
                repositoryorder.Insert(orders);
                //var check = true;
                //do
                //{
                var getCart = repositorycart.GetForeignKey((int)HttpContext.Session.GetInt32("LoginId"));

                //if (pro != null)
                //{
                foreach (var item in getCart)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.TotalPrice = item.TotalPrice;
                    orderDetail.ProductId = item.ProductId;
                    orderDetail.OrdersId = repositoryorder.GetId();

                    repositoryorderoetail.Insert(orderDetail);

                }
                repositorycart.DeleteAll((int)orders.AccountId);
                return RedirectToAction(nameof(Index));
            }

            var cart = repositorycart.GetForeignKey((int)HttpContext.Session.GetInt32("LoginId"));
            int c = 0;
            int a = 0;
            List<CartViewModel> list = new List<CartViewModel>();
            foreach (var item in cart)
            {
                if (item.AccountId == HttpContext.Session.GetInt32("LoginId"))
                {

                    c++;
                    ViewData["Number_Pro"] = c;
                    a += (int)item.TotalPrice;
                    ViewData["Total_Cart"] = a.ToString("#,##0 $");

                }
                list.Add(new CartViewModel() { _id = item._id, TotalPrice = item.TotalPrice, Quantity = item.Quantity, ProductName = item.ProductName, ProductImage = item.ProductImage, Price = item.Price });

            }
            ViewData["cart"] = list;
            return View(orders);
        }

    }
}
